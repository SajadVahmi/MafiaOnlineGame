using FluentValidation;
using FluentValidation.AspNetCore;
using IDP.Administration.Api.Users.Mappers;
using IDP.Administration.Api.Users.Validations;
using IDP.Administration.Services.Users.Services;
using IDP.Shared.IdentityStore.DbContexts;
using IDP.Shared.IdentityStore.Models;
using IDP.Shared.IdentityStore.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IDP.Administration.ServiceHost;

public static class HostingExtensions
{
    public static WebApplicationBuilder ConfigureConfiguration(this WebApplicationBuilder builder)
    {
        var appSettingsPath = $"appsettings.{builder.Environment.EnvironmentName}.json";

        builder.Configuration
            .AddJsonFile(path: appSettingsPath, optional: true, reloadOnChange: true).AddEnvironmentVariables();

        return builder;
    }

    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IUserServices, UserServices>();

        builder.Services.AddAutoMapper(typeof(UserMapper), typeof(Services.Users.Mappers.UserMapper));

        builder.Services.AddFluentValidationAutoValidation();

        builder.Services.AddValidatorsFromAssemblyContaining<CreateUserRequestBodyValidator>();

        ConfigureDatabase(builder);

        ConfigureIdentity(builder);

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }


    private static void ConfigureDatabase(WebApplicationBuilder builder)
    {
        var identityStoreOptions = builder.Configuration.GetSection("IdentityStore").Get<IdentityStoreOptions>();

        builder.Services.AddDbContext<IdpDbContext>(options =>
            options.UseSqlServer(identityStoreOptions.ConnectionString,
                x =>
                {
                    x.MigrationsAssembly(identityStoreOptions.MigrationsAssembly);
                    x.MigrationsHistoryTable(identityStoreOptions.MigrationsHistoryTable, identityStoreOptions.Schema);
                }
            ));

        using (var scope = builder.Services.BuildServiceProvider().CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IdpDbContext>();
            db.Database.Migrate();
        }
    }

    private static void ConfigureIdentity(WebApplicationBuilder builder)
    {

        builder.Services.AddIdentity<IdpUser, IdentityRole>()
          .AddEntityFrameworkStores<IdpDbContext>()
          .AddDefaultTokenProviders();
    }
}