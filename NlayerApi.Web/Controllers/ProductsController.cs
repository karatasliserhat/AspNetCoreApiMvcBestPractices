using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NlayerApi.Core.DTOs;
using NlayerApi.Core.IServices;
using NlayerApi.Core.Models;
using NlayerApi.Web.Filters;
using NlayerApi.Web.Services;

namespace NlayerApi.Web.Controllers
{
    public class ProductsController : Controller
    {

        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;
        public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService)
        {

            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        }

        public async Task GetCategory()
        {
            var categories = await _categoryApiService.GetAllCategoryAsync();
            ViewBag.categories = new SelectList(categories, "Id", "Name");
        }
        public async Task GetCategory(int categoryId)
        {
            var categories = await _categoryApiService.GetAllCategoryAsync();
            ViewBag.categories = new SelectList(categories, "Id", "Name", categoryId);
        }
        public async Task<IActionResult> Index()
        {
            var customResponseDto = await _productApiService.GetProductWithCategoryAsync();

            return View(customResponseDto);

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
                await _productApiService.SaveAsync(productCreateDto);
                return RedirectToAction(nameof(Index));
            }
            await GetCategory();
            return View(productCreateDto);
        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var data = await _productApiService.ProductGetByIdAsync(id);

            await GetCategory(data.CategoryId);
            return View(data);

        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.UpdateAsync(productUpdateDto);
                return RedirectToAction(nameof(Index));
            }
            await GetCategory(productUpdateDto.CategoryId);
            return View(productUpdateDto);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _productApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
