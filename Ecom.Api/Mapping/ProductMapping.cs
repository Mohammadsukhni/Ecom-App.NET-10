using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities.Product;

namespace Ecom.Api.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<AddProductDto, Product>()
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ReverseMap();

        }
    }
}
