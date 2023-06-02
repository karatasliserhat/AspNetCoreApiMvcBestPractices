using Microsoft.AspNetCore.Diagnostics;
using NlayerApi.Core.DTOs;
using NlayerApi.Service.Exceptions;
using System.Text.Json;

namespace NlayerApi.RestFull.CustomException
{
    public static class CustomExceptionHandler
    {
        public static void CustomExceptionError(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(opts =>
            {
                opts.Run(async context => {

                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;

                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                
                });
            });
        }
    }
}
