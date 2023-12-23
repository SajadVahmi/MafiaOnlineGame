using FluentValidation;
using Framework.Configuration.Loaders;
using Framework.Configuration.Scrutor;
using Framework.JsonSerializer.NewtonSoft;
using Framework.Mapping.AutoMapper;
using Framework.Presentation.AspNetCore.Extensions;
using Framework.Presentation.RestApi;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Players.Config;
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

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,

                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),

                        TokenUrl = new Uri("https://localhost:5001/connect/authorize"),

                        Scopes = new Dictionary<string, string>
                             {
                                 {"players","For access to players api"}
                             }
                    }
                },

                In = ParameterLocation.Header,

            });

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


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Players API V1");

                options.OAuthClientId("playersswaggerapiui");

                options.OAuthAppName("Players Swagger API UI");

            });
        }

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers().RequireAuthorization("players_api_access");

        return app;
    }
}
