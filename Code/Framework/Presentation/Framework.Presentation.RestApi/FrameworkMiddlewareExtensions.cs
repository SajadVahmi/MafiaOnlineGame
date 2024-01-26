using Microsoft.AspNetCore.Builder;

namespace Framework.Presentation.RestApi;

public static class FrameworkMiddlewareExtensions
{
    public static IApplicationBuilder UseFrameworkGlobalExceptionHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}