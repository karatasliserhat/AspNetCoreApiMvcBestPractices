using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NlayerApi.Core.IServices;
using NlayerApi.RestFull.Helper;
using System.Runtime.CompilerServices;

namespace NlayerApi.RestFull.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetByCategoryWithProduct(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetGetByCategoryWithProduct(categoryId));
        }
    }
}
