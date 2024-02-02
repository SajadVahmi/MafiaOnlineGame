using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Framework.Presentation.RestApi.Swagger;

public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, SwaggerOptions swaggerOptions) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options )
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, swaggerOptions));
        }
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description,SwaggerOptions swaggerOptions)
    {
        var info = new OpenApiInfo()
        {
            Title = swaggerOptions.Title,
            Version = description.ApiVersion.ToString(),
            Description = swaggerOptions.Description,
            Contact = new OpenApiContact { Name = swaggerOptions.Contact?.Name, Email = swaggerOptions.Contact?.Email},
        };

        if (description.IsDeprecated)
        {
            info.Description += swaggerOptions.IsDeprecatedApiDescription;
        }

        return info;
    }

   
}