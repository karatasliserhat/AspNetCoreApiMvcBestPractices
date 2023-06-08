using Microsoft.AspNetCore.Mvc;
using NlayerApi.Core.DTOs;
using NlayerApi.Core.IServices;
using NlayerApi.Core.Models;
using NlayerApi.RestFull.Helper;

namespace NlayerApi.RestFull.Controllers
{

    public class CateogoriesWithDtoConroller : CustomBaseController
    {
        private readonly IServiceWithDto<Category, CategoryDto> _categoryWithDto;

        public CateogoriesWithDtoConroller(IServiceWithDto<Category, CategoryDto> categoryWithDto)
        {
            _categoryWithDto = categoryWithDto;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CreateActionResult(await _categoryWithDto.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryDto category)
        {
            return CreateActionResult(await _categoryWithDto.AddAsync(category));
        }
    }
}
