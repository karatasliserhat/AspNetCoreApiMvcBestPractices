using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using NlayerApi.Core.DTOs;
using NlayerApi.Core.IServices;
using NlayerApi.Core.Models;

namespace NlayerApi.RestFull.Filters
{
    public class NotFoundActionFilter<T> : IAsyncActionFilter where T : BaseModel
    {
        private readonly IService<T> _service;

        public NotFoundActionFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var idVal = context.ActionArguments.Values.FirstOrDefault();
            if (idVal == null)
            {
                await next.Invoke();
                return;
            }
            var id = (int)idVal;
            var productData = await _service.AnyAsync(x => x.Id == id);

            if (productData)
            {
                await next.Invoke();
                return;
            }
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}' '({id}) not found"));
        }
    }
}
