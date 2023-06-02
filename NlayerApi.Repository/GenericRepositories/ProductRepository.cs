using Microsoft.EntityFrameworkCore;
using NlayerApi.Core.IRepositories;
using NlayerApi.Core.Models;
using NlayerApi.Repository.Context;

namespace NlayerApi.Repository.GenericRepositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductWithCategoryAsync()
        {
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
