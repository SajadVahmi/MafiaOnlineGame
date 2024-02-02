using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Framework.Presentation.RestApi.Swagger;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfigurationManager configuration)
    {
        var swaggerOptions = configuration.GetSection("Swagger").Get<SwaggerOptions>();

        ArgumentNullException.ThrowIfNull(swaggerOptions);
        ArgumentNullException.ThrowIfNull(swaggerOptions.SecurityScheme?.AuthorizationUrl);
        ArgumentNullException.ThrowIfNull(swaggerOptions.SecurityScheme?.TokenUrl);


        services.AddSingleton(swaggerOptions);
       
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen(options =>
        {

            var scheme = new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = swaggerOptions.SecurityScheme.Name,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(swaggerOptions.SecurityScheme.AuthorizationUrl),

                        TokenUrl = new Uri(swaggerOptions.SecurityScheme.TokenUrl),

                        Scopes = swaggerOptions.SecurityScheme.Scopes
                    }
                },
                Type = SecuritySchemeType.OAuth2
            };

            options.AddSecurityDefinition("OAuth", scheme);

            options.OperationFilter<AuthorizeCheckOperationFilter>();
        });

        return services;
    }

}