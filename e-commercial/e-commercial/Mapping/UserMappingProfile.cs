using AutoMapper;
using e_commercial.DTOs.Request.User;
using e_commercial.Models;

namespace e_commercial.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() 
        {

            //UserCreateDTO
            CreateMap<UserCreateDTO, User>().ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                                            .ForMember(dest => dest.Userpassword, opt => opt.MapFrom(src => src.Userpassword))
                                            .ForMember(dest => dest.UserShownname, opt => opt.MapFrom(src => src.UserShownname))
                                            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.UserEmail))
                                            .ForMember(dest => dest.UserAddress, opt => opt.MapFrom(src => src.UserAddress))
                                            .ForMember(dest => dest.UserWard, opt => opt.MapFrom(src => src.UserWard))
                                            .ForMember(dest => dest.UserDistrict, opt => opt.MapFrom(src => src.UserDistrict))
                                            .ForMember(dest => dest.UserPhone, opt => opt.MapFrom(src => src.UserPhone));

           
        }
    }
}
