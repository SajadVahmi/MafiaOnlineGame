using Framework.Core.Contracts;
using Framework.Presentation.AspNetCore.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Framework.Presentation.AspNetCore.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddHttpContextServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.TryAddSingleton<IAuthenticatedUser, AspNetCoreAuthenticatedUser>();
    }
}
