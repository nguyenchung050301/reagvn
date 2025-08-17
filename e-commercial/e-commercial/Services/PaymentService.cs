using e_commercial.Constants;
using e_commercial.DTOs.Request.Payment;
using e_commercial.Exceptions;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;

namespace e_commercial.Services
{
    public class PaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository; 
        public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public Payment GetByID(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new BadValidationException("Payment ID cannot be empty.");
            }
            return _paymentRepository.GetByID(id);
        }

        public void PayOrder(PaymentCreateDTO paymentDTO, Guid orderId, Guid userId)
        {
            var existingOrder = _orderRepository.GetByID(orderId);
            if (existingOrder.OrderStatus != OrderStatusEnum.Pending.ToString())
            {
                throw new BadValidationException("Order is not allowed to pay.");
            }
            if (orderId == Guid.Empty)
            {
                throw new BadValidationException("Order ID cannot be empty.");
            }
            if (userId == Guid.Empty)
            {
                throw new BadValidationException("User ID cannot be empty.");
            }
            var payment = new Payment
            {
                PaymentId = Guid.NewGuid().ToString(),
                PaymentPrice = paymentDTO.PaymentPrice,
                PaymentType = paymentDTO.PaymentType.ToString(),
                OrderId = orderId.ToString(),
                UserId = userId.ToString(),
                CreatedAt = DateTime.UtcNow
            };

            var existing = _orderRepository.GetByID(orderId);
            if (paymentDTO.PaymentPrice != existing.TotalAmount)
            {
                throw new BadValidationException("Total Paid is not matched Total Amount.");
            }
            
            _paymentRepository.Add(payment);
        }
    }
}
