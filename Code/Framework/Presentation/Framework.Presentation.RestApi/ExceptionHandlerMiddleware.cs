using Framework.Core.ApplicationServices.Exceptions;
using Framework.Core.Contracts;
using Framework.Core.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Framework.Presentation.RestApi;

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
            case NotFoundException notFoundException:
                await WriteResponse(context, ApiError.Instantiate(notFoundException), HttpStatusCode.NotFound);
                break;
            case ConflictException conflictException:
                await WriteResponse(context, ApiError.Instantiate(conflictException), HttpStatusCode.Conflict);
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
