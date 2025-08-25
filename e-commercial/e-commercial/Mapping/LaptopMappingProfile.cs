using AutoMapper;
using e_commercial.DTOs.Request.Laptop;
using e_commercial.DTOs.Response.Laptop;
using e_commercial.Models;
using System.Text.Json;


namespace e_commercial.Mapping
{
    public class LaptopMappingProfile : Profile
    {
        public LaptopMappingProfile()
        {
            //LaptopDetailDTO
            CreateMap<Laptop, LaptopDetailDTO>().ForMember(dest => dest.LaptopName, opt => opt.MapFrom(src => src.LaptopName))
                                                .ForMember(dest => dest.LaptopPrice, opt => opt.MapFrom(src => src.Price))
                                                .ForMember(dest => dest.LaptopImage, opt => opt.MapFrom(src => src.LaptopImage))
                                                .ForMember(dest => dest.LaptopDescription, opt => opt.MapFrom(src => src.LaptopDescription))
                                                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.ManufacturerName))
                                                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                                                .ForMember(dest => dest.LaptopSize, opt => opt.MapFrom(src => src.LaptopSize))
                                                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.StockQuantity))
                                                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
                            
            //LaptopItemDTO
            CreateMap<Laptop, LaptopItemDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.LaptopId))
                                                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.LaptopName));

            //LaptopUpdateDTO
            CreateMap<LaptopUpdateDTO, Laptop>().ForMember(dest => dest.LaptopName, opt => opt.MapFrom(src => src.LaptopName))
                                                .ForMember(d => d.LaptopImage, opt => opt.Ignore()) //ignore: skip mapping, use when dest variable
                                                                                                    //and source variable is not the same
                                                    .AfterMap((src, dest) => //after mapping completely
                                                    {
                                                        // nếu muốn null -> null; rỗng -> "[]"
                                                        if (src.LaptopImage == null)
                                                            dest.LaptopImage = null;
                                                        else
                                                            dest.LaptopImage = JsonSerializer.Serialize(src.LaptopImage);

                                                    })
                                                .ForMember(dest => dest.LaptopSize, opt => opt.Ignore())
                                                    .AfterMap((src, dest) =>
                                                    {
                                                        if (src.LaptopSize != null)
                                                            dest.LaptopSize = int.Parse(src.LaptopSize.ToString().Substring("Inch".Length).Trim());
                                                    })
                                                .ForMember(dest => dest.LaptopDescription, opt => opt.MapFrom(src => src.LaptopDescription))
                                                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.StockQuantity))
                                                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                                                .ForMember(dest => dest.ManufacturerId, opt => opt.MapFrom(src => src.ManufacturerId))
                                                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

            //LaptopCreateDTO
            CreateMap<LaptopCreateDTO, Laptop>().ForMember(dest => dest.LaptopName, opt => opt.MapFrom(src => src.LaptopName))
                                                  .ForMember(d => d.LaptopImage, opt => opt.Ignore()) //ignore: skip mapping, use when dest variable
                                                                                                        //and source variable is not the same
                                                    .AfterMap((src, dest) => //after mapping completely
                                                    {
                                                        // nếu muốn null -> null; rỗng -> "[]"
                                                        if (src.LaptopImage == null)
                                                            dest.LaptopImage = null;
                                                        else
                                                            dest.LaptopImage = JsonSerializer.Serialize(src.LaptopImage);

                                                    })
                                                  .ForMember(dest => dest.LaptopSize, opt => opt.Ignore())
                                                    .AfterMap((src, dest) =>
                                                    {
                                                        if (src.LaptopSize != null)
                                                            dest.LaptopSize = int.Parse(src.LaptopSize.ToString().Substring("Inch".Length).Trim());
                                                    })
                                                .ForMember(dest => dest.LaptopDescription, opt => opt.MapFrom(src => src.LaptopDescription))
                                                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.StockQuantity))
                                                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                                                .ForMember(dest => dest.ManufacturerId, opt => opt.MapFrom(src => src.ManufacturerId))
                                                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
        }
    }
}
