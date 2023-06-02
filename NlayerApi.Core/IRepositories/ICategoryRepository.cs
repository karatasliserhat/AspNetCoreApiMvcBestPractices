using NlayerApi.Core.Models;

namespace NlayerApi.Core.IRepositories
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Task<Category> GetByIdCategoryWithProduct(int id); 
    }
}
