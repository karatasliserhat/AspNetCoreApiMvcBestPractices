using Microsoft.EntityFrameworkCore;
using NlayerApi.Core.IRepositories;
using NlayerApi.Repository.Context;
using System.Linq.Expressions;

namespace NlayerApi.Repository.GenericRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private DbSet<T> _dbset { get; set; }
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        protected readonly DbSet<T> _dbSet;
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<IEnumerable<T>> GetAll()
        {
            return (IQueryable<IEnumerable<T>>)_dbSet.AsNoTracking().AsQueryable().ToList();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
