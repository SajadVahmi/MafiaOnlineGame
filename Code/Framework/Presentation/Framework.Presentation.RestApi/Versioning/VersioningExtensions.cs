using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Presentation.RestApi.Versioning;

public static class VersioningExtensions
{
    public static IServiceCollection AddApiVersioning(this IServiceCollection services, IConfigurationManager configuration)
    {
        var versioningOptions = configuration.GetSection("ApiVersioning").Get<VersioningOptions>();

        ArgumentNullException.ThrowIfNull(versioningOptions);
        ArgumentNullException.ThrowIfNull(versioningOptions.AssumeDefaultVersionWhenUnspecified);
        ArgumentNullException.ThrowIfNull(versioningOptions.DefaultApiVersion?.Major);
        ArgumentNullException.ThrowIfNull(versioningOptions.DefaultApiVersion?.Minor);
        ArgumentNullException.ThrowIfNull(versioningOptions.GroupNameFormat);
        ArgumentNullException.ThrowIfNull(versioningOptions.SubstituteApiVersionInUrl);


        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = versioningOptions.AssumeDefaultVersionWhenUnspecified.Value;

            options.DefaultApiVersion = new Asp.Versioning.ApiVersion(versioningOptions.DefaultApiVersion.Major.Value, versioningOptions.DefaultApiVersion.Minor.Value);

        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = versioningOptions.GroupNameFormat;

            options.SubstituteApiVersionInUrl = versioningOptions.SubstituteApiVersionInUrl.Value;
        });

        return services;
    }
}