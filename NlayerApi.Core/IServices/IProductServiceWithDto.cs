using NlayerApi.Core.DTOs;
using NlayerApi.Core.Models;

namespace NlayerApi.Core.IServices
{
    public interface IProductServiceWithDto:IServiceWithDto<Product,ProductDto>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategoryAsync();

        Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto productUpdateDto);
        Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto productCreateDto);
        Task<CustomResponseDto<IEnumerable<ProductDto>>> AddRangeAsync(IEnumerable<ProductCreateDto> productCreateDtos);

    }
}
