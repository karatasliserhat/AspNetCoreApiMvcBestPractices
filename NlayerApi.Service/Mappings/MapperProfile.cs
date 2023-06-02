using AutoMapper;
using NlayerApi.Core.DTOs;
using NlayerApi.Core.Models;

namespace NlayerApi.Service.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductWithCategoryDto>();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Category,GetByCategoryWithProductDto>();
        }
    }
}
