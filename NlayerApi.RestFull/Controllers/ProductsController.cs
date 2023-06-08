using AutoMapper;
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
        private readonly IProductServiceWithDto _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductServiceWithDto productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return CreateActionResult(await _productService.GetAllAsync());
        }
        [ServiceFilter(typeof(NotFoundActionFilter<Product>))]
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            return CreateActionResult(await _productService.GetByIdAsync(productId));
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductCreateDto productCreateDto)
        {

            return CreateActionResult(await _productService.AddAsync(productCreateDto));
        }
        [HttpDelete("{productId}")]

        public async Task<IActionResult> Remove(int productId)
        {

            return CreateActionResult(await _productService.RemoveAsync(productId));
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto updateDto)
        {
            return CreateActionResult(await _productService.UpdateAsync(updateDto));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory()
        {
            return CreateActionResult(await _productService.GetProductWithCategoryAsync());
        }
        [HttpGet("Any({id}")]

        public async Task<IActionResult> Any(int id)
        {
            return CreateActionResult(await _productService.AnyAsync(x => x.Id == id));
        }

        [HttpPost("/SaveAll")]
        public async Task<IActionResult> Add(List<ProductCreateDto> productCreateDtos)
        {
            return CreateActionResult(await _productService.AddRangeAsync(productCreateDtos));
        }
        [HttpDelete("/Remove")]
        public async Task<IActionResult> Remove(List<int> ids)
        {
            return CreateActionResult(await _productService.RemoveRangeAsync(ids));
        }
    }
}
