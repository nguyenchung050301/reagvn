using AutoMapper;
using e_commercial.DTOs.Request.Order;
using e_commercial.DTOs.Response.Order;
using e_commercial.Models;

namespace e_commercial.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile() 
        {
            //OrderDetailItemDTO.CartItemDTO
            CreateMap<Orderdetail, OrderDetailItemDTO.CartItemDTO>().ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                                                                    .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
                                                                    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));


            //OrderDetailItemDTO
            CreateMap<Order, OrderDetailItemDTO>().ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.UserAddress))
                                                  .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.User.UserDistrict))
                                                  .ForMember(dest => dest.Ward, opt => opt.MapFrom(src => src.User.UserWard))
                                                  .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.Orderdetails));

            //OrderCreateDTO
            CreateMap<OrderCreateDTO, Order>().ForMember(dest => dest.Orderdetails, opt => opt.MapFrom(src => src.CartItems))
                                              ;
        }
    }
}
