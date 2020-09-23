using Quick.Common.Dtos;
using Quick.Common.Helpers;
using Quick.Core.Runtime.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Quick.Web.Framework.Handlers
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is MyException)
                        {
                            await context.Response.WriteAsync(JsonHelper.SerializeObject(new ApiOutput
                            {
                                Status = context.Response.StatusCode,
                                Success = false,
                                Message = ((MyException)contextFeature.Error).ErrorMessage
                            }));
                        }
                        else
                        {
                            await context.Response.WriteAsync(JsonHelper.SerializeObject(new ApiOutput
                            {
                                Status = context.Response.StatusCode,
                                Success = false,
                                Message = contextFeature.Error.Message
                            }));
                        }
                    }
                });
            });
        }

    }
}
