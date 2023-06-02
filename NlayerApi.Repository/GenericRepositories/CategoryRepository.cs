using Microsoft.EntityFrameworkCore;
using NlayerApi.Core.IRepositories;
using NlayerApi.Core.Models;
using NlayerApi.Repository.Context;

namespace NlayerApi.Repository.GenericRepositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetByIdCategoryWithProduct(int id)
        {
            return await _context.Set<Category>().Include(x=> x.Products).SingleOrDefaultAsync(x=> x.Id == id);
        }
    }
}
