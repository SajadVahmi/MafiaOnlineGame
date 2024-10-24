﻿using Framework.Presentation.AspNet.Extensions;
using Framework.Presentation.RestApi.Filters;
using Framework.Presentation.RestApi.Middlewares;
using Games.Application.PlayerAggregate.Commands.RegisterPlayer;
using Games.Contract.PlayerAggregate.DomainEvents;
using Games.Persistence.EventStore;
using Games.Query._Shared;
using Games.Query.PlayerAggregate.Queries.ViewProfile;
using Games.Query.PlayerAggregate.Services;
using Games.RestApi.PlayerAggregate.Mappers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Games.ServiceHost;

public static class ServiceConfiguration
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var domainEventsAssemblies = new[] { typeof(PlayerRegistered).Assembly };
        var domainServicesAssemblies= new[] { typeof(PlayerDuplicationRegistrationDetector).Assembly };
        var commandHandlersAssemblies = new[] { typeof(RegisterPlayerCommandHandler).Assembly };
        var queryHandlersAssemblies = new[] { typeof(ViewProfileQueryHandler).Assembly };
        var repositoriesAssemblies = new[] { typeof(PlayerRepository).Assembly };
        var autoMappersConfigsAssemblies = new[] { typeof(PlayerMappingProfile).Assembly };

        var eventStoreConnectionString = builder.Configuration.GetSection("Persistence:Command").Get<string?>();
        ArgumentException.ThrowIfNullOrEmpty(eventStoreConnectionString);

        var sqlServerConnectionString = builder.Configuration.GetSection("Persistence:Query").Get<string?>();
        ArgumentException.ThrowIfNullOrEmpty(sqlServerConnectionString);

        builder.Services.AddControllers();

        
        
        builder.Services
            .AddHttpContextServices()
            .AddCoreServices()
            .AddRouting(options => options.LowercaseUrls = true)
            .AddEndpointsApiExplorer()
            .AddDomainServices(domainServicesAssemblies)
            .AddCommandHandlers(commandHandlersAssemblies)
            .AddQueryHandlers(queryHandlersAssemblies)
            .AddRepositories(repositoriesAssemblies)
            .AddNewtonSoft()
            .AddAutoMapperConfigs(autoMappersConfigsAssemblies)
            .AddEventSourceDatabase(eventStoreConnectionString,domainEventsAssemblies)
            .AddQueryDbContext(sqlServerConnectionString);



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

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("players_api_access", policy => policy.RequireClaim("scope", "players"));
        });
        
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

        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Players API V1");

            options.OAuthAppName("Players Swagger API");

            options.OAuthClientId("players-api-swagger");

            options.OAuthScopes("profile", "openid", "players");

            options.OAuthUsePkce();

        });

        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

}