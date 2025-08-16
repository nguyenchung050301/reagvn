using e_commercial.Constants;
using e_commercial.DTOs.Request.Order;
using e_commercial.DTOs.Request.Pagination;
using e_commercial.DTOs.Response.Order;
using e_commercial.DTOs.Response.Pagination;
using e_commercial.Exceptions;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;

using System;
using System.Net;
using System.Numerics;

namespace e_commercial.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IKeyboardRepository _keyboardRepository;

        private IEnumerable<(string, ProductTypeEnum, float, int)> validatedProducts;
        public OrderService(IOrderRepository orderRepository, IKeyboardRepository keyboardRepository,
            IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _keyboardRepository = keyboardRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public PaginationResponseDTO<OrderItemDTO>  GetPagination(OrderPaginationRequestDTO requestDTO)
        {
            var items = _orderRepository.GetPagination(requestDTO.PageNumber, requestDTO.PageSize, requestDTO.orderStatus);
            int count = items.Item2;
            return new PaginationResponseDTO<OrderItemDTO>
            {
                PageSize = requestDTO.PageSize,
                CurrentPage = requestDTO.PageNumber,
                TotalCount = count,
                TotalPages = (int)Math.Ceiling((double)count / requestDTO.PageSize),
                Items = items.Item1.Select(order => new OrderItemDTO
                {
                    Id = order.OrderId,
                    OrderStatus = Enum.Parse<OrderStatusEnum>(order.OrderStatus),
                    // Map other properties as needed
                }).ToList()
            };
        }

        public void AprroveOrder(Guid orderId)
        {
            var existing = _orderRepository.GetByID(orderId); Console.WriteLine(existing.ToString());
            var products = _orderDetailRepository.GetProductsByOrderID(orderId);
            if (existing == null)
            {
                throw new BadValidationException("Order not found.");
            }
            if (existing.OrderStatus != OrderStatusEnum.Pending.ToString())
            {
                throw new BadValidationException("Order cannot be approved because it is not in pending status.");
            }

            existing.OrderStatus = OrderStatusEnum.Approved.ToString();

            //Stock handling
            foreach (var product in products)
            {
                if (product.Quantity <= 0)
                {
                    throw new BadValidationException($"Product {product.ProductId} quantity must be greater than zero.");
                }
                StockHandle(Enum.Parse<ProductTypeEnum>(product.ProductType), Guid.Parse(product.ProductId), product.Quantity);
            }


            _orderRepository.Update(existing);
        }

        public void CancelOrderByUser(Guid orderId, Guid CancelById)
        {
            var existing = _orderRepository.GetByID(orderId);
            if (existing == null)
            {
                throw new BadValidationException("Order not found.");
            }
            if (existing.OrderStatus != OrderStatusEnum.Pending.ToString())
            {
                throw new BadValidationException("Order cannot be cancelled because it is not in pending status.");
            }
            existing.OrderStatus = OrderStatusEnum.CancelledByUser.ToString();
            existing.CancelBy = CancelById.ToString();
            _orderRepository.Update(existing);
        }

        public void CancelOrderByAdmin(Guid orderId, Guid cancelById)
        {
            var existing = _orderRepository.GetByID(orderId);
            if (existing == null)
            {
                throw new BadValidationException("Order not found.");
            }
            existing.OrderStatus = OrderStatusEnum.CancelledByAdmin.ToString();
            existing.CancelBy = cancelById.ToString();
            _orderRepository.Update(existing);
        }
        /// <summary>
        /// B1: Khi request toi -> validate data: product type, so luong cart item tu request co = so luong trong DB ko,
        /// B2: Tao cac order detail voi moi product trong orderCreateDTO
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <param name="userID"></param>
        /// <exception cref="BadValidationException"></exception>
        public void CreateOrder(OrderCreateDTO orderDTO, User user)
        {
            if (orderDTO == null)
            {
                throw new BadValidationException("Order data cannot be null");
            }
            string orderId = Guid.NewGuid().ToString();

            //Order Details
            Dictionary<ProductTypeEnum, IEnumerable<OrderCreateDTO.CartItemDTO>> dicts =
                 orderDTO.CartItems
               .GroupBy(item => item.ProductType)
               .ToDictionary(g => g.Key, g => g.AsEnumerable());
            var order = new Order
            {
                OrderId = orderId,

                OrderStatus = OrderStatusEnum.Pending.ToString(),
                CreatedAt = DateTime.UtcNow,
                UserId = user.UserId,
                // Map other properties as needed
            };
            _orderRepository.Add(order);


     
           
            foreach (var item in dicts)
            {
                //Tuple: // (string, ProductTypeEnum, float, int) -> productId, productType ,price, quantity
                validatedProducts = ValidateProducts(item.Key, item.Value);
                order.TotalAmount = (int)validatedProducts.Sum(p => p.Item3 * p.Item4);
            }

           // IEnumerable<Orderdetail> orderDetails = new List<Orderdetail>();
            foreach (var item in validatedProducts)
            {
                var orderDetail = new Orderdetail();
                orderDetail.OrderId = orderId;
                if (user.UserId == null)
                {
                    orderDetail.District = orderDTO.District;
                    orderDetail.Ward = orderDTO.Ward;
                    orderDetail.Address = orderDTO.Address;
                    orderDetail.Phone = orderDTO.Phone;
                }
                else
                {
                    orderDetail.District = user.UserDistrict;
                    orderDetail.Ward = user.UserWard;
                    orderDetail.Address = user.UserAddress;
                    orderDetail.Phone = user.UserPhone;
                }
                FillIntoOrderDetail(ref orderDetail, item.Item1, item.Item2, item.Item3, item.Item4);
                _orderDetailRepository.Add(orderDetail);
            }




        }

        /*Cach 1: 
           Dictionary<string, IEnumerable<OrderCreateDTO.CartItemDTO>> dicts =
                  orderDTO.CartItems
                .GroupBy(item => item.ProductType)
                .ToDictionary(g => g.Key, g => g.AsEnumerable()); 
        */
        /*Cach 2:
        public Dictionary<string, IEnumerable<OrderCreateDTO.CartItemDTO>> MapDict(IEnumerable<OrderCreateDTO.CartItemDTO> cartItem)
        {
            Dictionary<string, IEnumerable<OrderCreateDTO.CartItemDTO>> results = new();

            foreach (var item in cartItem)
            {
                if (!results.ContainsKey(item.ProductType))
                {
                    results[item.ProductType] = new List<OrderCreateDTO.CartItemDTO>();
                    
                }
                results[item.ProductType].Append(item);

            }
            return results;
        }*/
        private IEnumerable<(string, ProductTypeEnum, float, int)> ValidateProducts(ProductTypeEnum type, IEnumerable<OrderCreateDTO.CartItemDTO> cartItems)
        {
            switch (type)
            {
                case ProductTypeEnum.Keyboard:
                    {
                        var foundProducts = _keyboardRepository.FindInIDs(cartItems.Select(item => item.ProductId)).ToList();
                        if (cartItems.Count() != foundProducts.Count())
                        {
                            throw new BadValidationException("Some keyboard IDs are invalid.");
                        }


                        return foundProducts.Select(item => (item.KeyboardId, ProductTypeEnum.Keyboard, (float)item.Price,
                                cartItems.FirstOrDefault(p => p.ProductId == item.KeyboardId).Quantity)).ToList();

                    }
                case ProductTypeEnum.Laptop:
                    // Handle laptop specific logic
                    return new List<(string, ProductTypeEnum, float, int)>();
                default:
                    throw new ArgumentException("Invalid product type.");
            }
        }
        private void StockHandle(ProductTypeEnum type, Guid id, int quantity)
        {
            switch (type)
            {
                case ProductTypeEnum.Keyboard:
                    {
                        var item = _keyboardRepository.GetByID(id);
                        if (item.StockQuantity < quantity)
                        {
                            throw new BadValidationException($"Keyboard {id} is out of stock.");
                        }
                        item.StockQuantity -= quantity;
                        break;
                    }
                case ProductTypeEnum.Laptop:
                    break;
            }
        }
        private void FillIntoOrderDetail(ref Orderdetail orderDetail, string productID, ProductTypeEnum productType,
            float price, int quantity)
        {
            orderDetail.OrderDetailId = Guid.NewGuid().ToString();
            //     orderDetail.OrderId = orderDetail.OrderId;
            orderDetail.ProductId = productID;
            orderDetail.ProductType = productType.ToString();
            orderDetail.UnitPrice = price;
            orderDetail.Quantity = quantity;
        }
    }
}