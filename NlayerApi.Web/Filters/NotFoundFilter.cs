using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NlayerApi.Core.DTOs;
using NlayerApi.Core.IServices;
using NlayerApi.Core.Models;

namespace NlayerApi.Web.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseModel
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idval = context.ActionArguments.Values.FirstOrDefault();
            if (idval == null)
            {
                await next.Invoke();
                return;
            }
            var id = (int)idval;
            var data = await _service.AnyAsync(x => x.Id == id);
            if (data)
            {
                await next.Invoke();
                return;
            }
            var errorViewModel = new ErrorViewModel();
            errorViewModel.Errors.Add($"{typeof(T).Name}({id}) not found");
            context.Result = new RedirectToActionResult("Error", "Home", errorViewModel);
        }
    }
}
