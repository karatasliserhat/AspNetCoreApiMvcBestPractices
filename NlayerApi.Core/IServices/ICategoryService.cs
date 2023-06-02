using NlayerApi.Core.DTOs;
using NlayerApi.Core.Models;

namespace NlayerApi.Core.IServices
{
    public interface ICategoryService:IService<Category>
    {
        Task<CustomResponseDto<GetByCategoryWithProductDto>> GetGetByCategoryWithProduct(int id);
    }
}
