using FluentValidation;
using Framework.Configuration.Loaders;
using Framework.Configuration.Scrutor;
using Framework.JsonSerializer.NewtonSoft;
using Framework.Mapping.AutoMapper;
using Framework.Presentation.AspNetCore.Extensions;
using Framework.Presentation.RestApi;
using Framework.Presentation.RestApi.Swagger;
using Framework.Presentation.RestApi.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Players.Config;
using Players.Mapping.PlayerAggregate;
using Players.RestApi.V1.PlayerAggregate.Validations.Register;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Players.ServiceHost.Extensions;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {


        builder.Services.AddControllers();

        builder.Services.AddApiVersioning(builder.Configuration);

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("players_api_access", policy => policy.RequireClaim("scope", "players"));
        });
        
        

        builder.Services.AddSwagger(builder.Configuration);

        builder.Services.AddHttpContextServices();

        builder.Services.AddValidatorsFromAssemblyContaining<PlayerRegistrationRequestValidator>();

        FrameworkModuleBuilder.Setup()
            .WithIocModule(new ScrutorModule(builder.Services))
            .WithModule(new PlayersModule(builder.Configuration, builder.Services))
            .WithModule(new AutoMapperModule(typeof(PlayerMappings).Assembly))
            .WithModule(new NewtonSoftSerializerModule());




        builder.Services.AddAuthentication("Bearer")
             .AddJwtBearer("Bearer", options =>
             {
                 options.Authority = "https://localhost:5001";

                 options.MapInboundClaims = false;

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateAudience = false
                 };


             });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseFrameworkGlobalExceptionHandlerMiddleware();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                var descriptions = app.DescribeApiVersions();

                foreach (var description in descriptions)
                {
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }

                options.OAuthAppName("Players Swagger API");

                options.OAuthClientId("players-api-swagger");

                options.OAuthScopes("profile", "openid", "players");

                options.OAuthUsePkce();

            });
        }

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers().RequireAuthorization("players_api_access");

        return app;
    }
}
