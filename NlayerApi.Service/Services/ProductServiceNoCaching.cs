using AutoMapper;
using NlayerApi.Core.DTOs;
using NlayerApi.Core.IRepositories;
using NlayerApi.Core.IServices;
using NlayerApi.Core.Models;
using NlayerApi.Core.UnitOfWork;

namespace NlayerApi.Service.Services
{
    public class ProductServiceNoCaching : Service<Product>, IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductServiceNoCaching(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory()
        {
            var products = await _productRepository.GetProductWithCategoryAsync();
            var productData = _mapper.Map<List<ProductWithCategoryDto>>(products.ToList());
            
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(productData,200);
        }
    }
}
