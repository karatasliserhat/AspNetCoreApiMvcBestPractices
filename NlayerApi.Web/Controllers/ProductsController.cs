using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NlayerApi.Core.DTOs;
using NlayerApi.Core.IServices;
using NlayerApi.Core.Models;
using NlayerApi.Repository.Configurations;
using NlayerApi.Web.Filters;

namespace NlayerApi.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper, ICategoryService categoryService)
        {
            _productService = productService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task GetCategory()
        {
            var categories = await _categoryService.GetAllAsync();
            var categorydata = _mapper.Map<List<CategoryDto>>(categories);
            ViewBag.categories = new SelectList(categorydata, "Id", "Name");
        }
        public async Task GetCategory(int categoryId)
        {
            var categories = await _categoryService.GetAllAsync();
            var categorydata = _mapper.Map<List<CategoryDto>>(categories);
            ViewBag.categories = new SelectList(categorydata, "Id", "Name", categoryId);
        }
        public async Task<IActionResult> Index()
        {
            var customResponseDto = await _productService.GetProductWithCategory();

            return View(customResponseDto.Data);

        }

        public async Task<IActionResult> Save()
        {
            await GetCategory();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto productCreateDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(_mapper.Map<Product>(productCreateDto));
                return RedirectToAction(nameof(Index));
            }
            await GetCategory();
            return View(productCreateDto);
        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var data = _mapper.Map<ProductUpdateDto>(await _productService.GetByIdAsync(id));
            
            await GetCategory(data.CategoryId);
            return View(data);

        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(_mapper.Map<Product>(productUpdateDto));
                return RedirectToAction(nameof(Index));
            }
            await GetCategory(productUpdateDto.CategoryId);
            return View(productUpdateDto);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data =await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(data);
            return RedirectToAction(nameof(Index));
        }
    }
}
