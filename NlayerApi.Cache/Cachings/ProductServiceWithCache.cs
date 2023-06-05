using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NlayerApi.Core.DTOs;
using NlayerApi.Core.IRepositories;
using NlayerApi.Core.IServices;
using NlayerApi.Core.Models;
using NlayerApi.Core.UnitOfWork;
using NlayerApi.Service.Exceptions;
using System.Linq.Expressions;

namespace NlayerApi.Cache.Cachings
{
    public class ProductServiceWithCache : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private const string CacheProductKey = "productCache";
        public ProductServiceWithCache(IMapper mapper, IMemoryCache memoryCache, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;
            if (!_memoryCache.TryGetValue(CacheProductKey, out _))
            {
                _memoryCache.Set(CacheProductKey, _repository.GetProductWithCategoryAsync().Result);
            }

        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await GetListProductCache();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await GetListProductCache();
            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            return Task.FromResult(_memoryCache.Get<List<Product>>(CacheProductKey).Any(expression.Compile()));
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var data = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);
            if (data == null)
                throw new NoContentException($"{typeof(Product)}({id}) not found");
            return Task.FromResult(data);
        }

        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory()
        {
            var data = _memoryCache.Get<List<Product>>(CacheProductKey);
            var dataMapper = _mapper.Map<List<ProductWithCategoryDto>>(data);
            return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.Success(dataMapper, 200));
        }

        public async Task RemoveAsync(Product entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await GetListProductCache();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await GetListProductCache();
        }

        public async Task UpdateAsync(Product entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await GetListProductCache();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task GetListProductCache()
        {
            _memoryCache.Set(CacheProductKey, await _repository.GetProductWithCategoryAsync());
        }
    }
}
