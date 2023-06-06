using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NlayerApi.Core.DTOs;
using NlayerApi.Core.IServices;
using NlayerApi.Core.Models;
using NlayerApi.Repository.Configurations;

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
    }
}
