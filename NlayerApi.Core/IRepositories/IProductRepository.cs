using NlayerApi.Core.DTOs;
using NlayerApi.Core.Models;

namespace NlayerApi.Core.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductWithCategoryAsync();
    }
}
