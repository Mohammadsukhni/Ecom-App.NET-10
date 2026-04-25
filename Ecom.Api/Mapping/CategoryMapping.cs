using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities.Product;

namespace Ecom.Api.Mapping
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        { 
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<UpdateCategoryDto,Category>().ReverseMap();

        }
    }
}
