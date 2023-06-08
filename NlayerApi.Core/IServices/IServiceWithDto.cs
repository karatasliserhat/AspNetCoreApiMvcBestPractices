using NlayerApi.Core.DTOs;
using NlayerApi.Core.Models;
using System.Linq.Expressions;

namespace NlayerApi.Core.IServices
{
    public interface IServiceWithDto<Entity, Dto> where Entity : BaseModel where Dto : class
    {
        Task<CustomResponseDto<IEnumerable<Dto>>> GetAllAsync();
        Task<CustomResponseDto<Dto>> GetByIdAsync(int id);
        Task<CustomResponseDto<Dto>> AddAsync(Dto Dto);
        Task<CustomResponseDto<IEnumerable<Dto>>> AdRangeAsync(IEnumerable<Dto> Dtos);

        Task<CustomResponseDto<IEnumerable<Dto>>> WhereAsync(Expression<Func<Entity, bool>> expression);
        Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<Entity, bool>> expression);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(Dto Dto);
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);
        Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids);

    }
}
