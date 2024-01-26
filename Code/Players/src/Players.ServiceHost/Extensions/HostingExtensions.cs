using FluentValidation;
using FluentValidation.AspNetCore;
using Framework.Configuration.Loaders;
using Framework.Configuration.Scrutor;
using Framework.JsonSerializer.NewtonSoft;
using Framework.Mapping.AutoMapper;
using Framework.Presentation.AspNetCore.Extensions;
using Framework.Presentation.RestApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Players.Config;
using Players.Contracts.Resources;
using Players.Mapping.PlayerAggregate;
using Players.RestApi.V1.PlayerAggregate.Validations.Register;

namespace Players.ServiceHost.Extensions;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("players_api_access", policy => policy.RequireClaim("scope", "players"));
        });

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Players API", Version = "v1" });

            var scheme = new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),

                        TokenUrl = new Uri("https://localhost:5001/connect/token"),

                        Scopes = new Dictionary<string, string>{

                            {"players","For access to players api"}
                        }
                    }
                },
                Type = SecuritySchemeType.OAuth2
            };

            options.AddSecurityDefinition("OAuth", scheme);

            options.OperationFilter<AuthorizeCheckOperationFilter>();
        });

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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Players API V1");

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
