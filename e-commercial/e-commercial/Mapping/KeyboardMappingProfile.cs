using AutoMapper;
using e_commercial.DTOs.Request.Keyboard;
using e_commercial.DTOs.Response.Keyboard;
using e_commercial.Models;
using System.Text.Json;

namespace e_commercial.Mapping
{
    public class KeyboardMappingProfile : Profile
    {
        public KeyboardMappingProfile() 
        {
            //KeyboardDetailDTO
            CreateMap<Keyboard, KeyboardDetailDTO>().ForMember(dest => dest.KeyboardName, opt => opt.MapFrom(src => src.KeyboardName))
                                                    .ForMember(dest => dest.KeyboardImage, opt => opt.MapFrom(src => src.KeyboardImage))
                                                    .ForMember(dest => dest.KeyboardSwitch, opt => opt.MapFrom(src => src.KeyboardSwitch))
                                                    .ForMember(dest => dest.KeyboardDescription, opt => opt.MapFrom(src => src.KeyboardDescription))
                                                    .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.ManufacturerName))
                                                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                                                    .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.StockQuantity))
                                                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            //KeyboardCreateDTO
            CreateMap<KeyboardCreateDTO, Keyboard>().ForMember(dest => dest.KeyboardName, opt => opt.MapFrom(src => src.KeyboardName))
                                                    .ForMember(dest => dest.KeyboardDescription, opt => opt.MapFrom(src => src.KeyboardDescription))
                                                    .ForMember(dest => dest.KeyboardImage, opt => opt.Ignore()).AfterMap((src, dest) =>
                                                    {
                                                        if (src.KeyboardImage == null)
                                                            dest.KeyboardImage = null;
                                                        else
                                                            dest.KeyboardImage = JsonSerializer.Serialize(src.KeyboardImage);
                                                    })
                                                    .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.StockQuantity))
                                                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                                                    .ForMember(dest => dest.ManufacturerId, opt => opt.MapFrom(src => src.ManufacturerId))
                                                    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
                       
            //KeyboardUpdateDTO
            CreateMap<KeyboardUpdateDTO, Keyboard>().ForMember(dest => dest.KeyboardName, opt => opt.MapFrom(src => src.KeyboardName))
                                                    .ForMember(dest => dest.KeyboardDescription, opt => opt.MapFrom(src => src.KeyboardDescription))
                                                    .ForMember(dest => dest.KeyboardImage, opt => opt.Ignore()).AfterMap((src, dest) =>
                                                    {
                                                        if (src.KeyboardImage == null)
                                                            dest.KeyboardImage = null;
                                                        else
                                                            dest.KeyboardImage = JsonSerializer.Serialize(src.KeyboardImage);
                                                    })
                                                    .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.StockQuantity))
                                                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                                                    .ForMember(dest => dest.ManufacturerId, opt => opt.MapFrom(src => src.ManufacturerId))
                                                    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
        }
    }
}
