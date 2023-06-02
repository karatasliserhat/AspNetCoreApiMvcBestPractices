using NlayerApi.Core.DTOs;
using NlayerApi.Core.Models;

namespace NlayerApi.Core.IServices
{
    public interface IProductService:IService<Product>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory();
    }
}
