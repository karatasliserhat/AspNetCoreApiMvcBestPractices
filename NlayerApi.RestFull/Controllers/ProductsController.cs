using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NlayerApi.Core.DTOs;
using NlayerApi.Core.IServices;
using NlayerApi.Core.Models;
using NlayerApi.RestFull.Filters;
using NlayerApi.RestFull.Helper;

namespace NlayerApi.RestFull.Controllers
{
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var product = await _productService.GetAllAsync();
            var responseData = _mapper.Map<List<ProductDto>>(product.ToList());

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(responseData, 200));
        }
        [ServiceFilter(typeof(NotFoundActionFilter<Product>))]
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _productService.GetByIdAsync(productId);
            var responseData = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(responseData, 200));
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductCreateDto productCreateDto)
        {
            var productDto = _mapper.Map<Product>(productCreateDto);
            await _productService.AddAsync(productDto);
            return CreateActionResult(CustomResponseDto<ProductCreateDto>.Success(productCreateDto, 200));
        }
        [HttpDelete("{productId}")]

        public async Task<IActionResult> Remove(int productId)
        {
            var productData = await _productService.GetByIdAsync(productId);
            await _productService.RemoveAsync(productData);

            return CreateActionResult(CustomResponseDto<Product>.Success(204));
        }
        [HttpPut]
        public async Task<IActionResult> Add(ProductUpdateDto updateDto)
        {
            var responseDto = _mapper.Map<Product>(updateDto);
            await _productService.UpdateAsync(responseDto);
            return CreateActionResult(CustomResponseDto<ProductUpdateDto>.Success(200));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory()
        {
            return CreateActionResult(await _productService.GetProductWithCategory());
        }
    }
}
