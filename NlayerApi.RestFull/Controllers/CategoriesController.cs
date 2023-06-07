using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NlayerApi.Core.DTOs;
using NlayerApi.Core.IServices;
using NlayerApi.RestFull.Helper;

namespace NlayerApi.RestFull.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoryAsync()
        {
            var data = await _categoryService.GetAllAsync();
            var categoryData = _mapper.Map<List<CategoryDto>>(data.ToList());
            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(categoryData, 200));
        }

        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetByCategoryWithProduct(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetGetByCategoryWithProduct(categoryId));
        }
    }
}
