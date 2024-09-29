using Framework.Presentation.RestApi.Extensions;
using Framework.Presentation.RestApi.Filters;
using Games.Application.PlayerAggregate.CommandHandlers;
using Games.Application.PlayerAggregate.Commands;
using Games.Domain.Contracts.DomainEvents.PlayerAggregate;
using Games.Domain.PlayerAggregate.Data;
using Games.Persistence.EventStore;
using Microsoft.OpenApi.Models;

namespace Games.ServiceHost;

public static class ServiceConfiguration
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {


        builder.Services.AddControllers();

        builder.Services
            .AddHttpContextServices()
            .AddCoreServices()
            .AddRouting(options => options.LowercaseUrls = true)
            .AddEndpointsApiExplorer()
            .AddDomainServices()
            .AddCommandHandlers(typeof(PlayerCommandHandler).Assembly)
            .AddEventSourceRepositories("esdb://localhost:2113?tls=false&tlsVerifyCert=false", typeof(PlayerRegistered).Assembly);

        builder.Services.AddScoped<IPlayerRepository,PlayerRepository>();
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

        return builder.Build();


    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {

        app.UseSwagger();

        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

}