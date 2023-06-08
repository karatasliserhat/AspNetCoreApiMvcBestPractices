using AutoMapper;
using Microsoft.AspNetCore.Http;
using NlayerApi.Core.DTOs;
using NlayerApi.Core.IRepositories;
using NlayerApi.Core.IServices;
using NlayerApi.Core.Models;
using NlayerApi.Core.UnitOfWork;

namespace NlayerApi.Service.Services
{
    public class ProductServiceWithDto : ServiceWithDto<Product, ProductDto>, IProductServiceWithDto

    {
        private readonly IProductRepository _productRepository;
        public ProductServiceWithDto(IGenericRepository<Product> genericRepository, IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository) : base(genericRepository, mapper, unitOfWork)
        {
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto productCreateDto)
        {
            var entity = _mapper.Map<Product>(productCreateDto);
            await _productRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            var data = _mapper.Map<ProductDto>(entity);
            return CustomResponseDto<ProductDto>.Success(data, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<IEnumerable<ProductDto>>> AddRangeAsync(IEnumerable<ProductCreateDto> productCreateDtos)
        {
            var entities = _mapper.Map<IEnumerable<Product>>(productCreateDtos);
            await _productRepository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            var dtos = _mapper.Map<IEnumerable<ProductDto>>(entities);
            return CustomResponseDto<IEnumerable<ProductDto>>.Success(dtos, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategoryAsync()
        {
            var data = await _productRepository.GetProductWithCategoryAsync();
            var dataMap = _mapper.Map<List<ProductWithCategoryDto>>(data);

            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(dataMap, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var entity = _mapper.Map<Product>(productUpdateDto);
            _productRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}
