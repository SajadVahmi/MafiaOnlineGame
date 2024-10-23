using System.Net;
using Framework.Core.Domain.Exceptions;
using Framework.Core.ServiceContracts;
using Framework.Presentation.RestApi.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Framework.Presentation.RestApi.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next, IJsonSerializerAdapter jsonSerializer)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            await HandleException(context, exception);

        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        switch (exception)
        {
            case BusinessException businessException:
                await WriteResponse(context, ApiError.Instantiate(businessException), HttpStatusCode.BadRequest);
                break;
            default:
                await WriteResponse(context, ApiError.Instantiate("Internal-500", "An internal server error has occurred, please contact support"), HttpStatusCode.InternalServerError);
                break;
        }
    }

    private async Task WriteResponse(HttpContext context, ApiError error, HttpStatusCode responseCode)
    {
        context.Response.StatusCode = (int)responseCode;

        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(jsonSerializer.Serialize(error)!);
    }

}

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseFrameworkGlobalExceptionHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}