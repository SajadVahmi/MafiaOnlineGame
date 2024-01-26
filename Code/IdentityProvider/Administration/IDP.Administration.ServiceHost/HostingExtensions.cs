using FluentValidation;
using FluentValidation.AspNetCore;
using IDP.Administration.Api.V1.Users.Validations;
using IDP.Administration.Services.Users.Services;
using IDP.Shared.IdentityStore.DbContexts;
using IDP.Shared.IdentityStore.Models;
using IDP.Shared.IdentityStore.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IDP.Administration.ServiceHost;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IUserServices, UserServices>();

        builder.Services.AddAutoMapper(
            typeof(IDP.Administration.Api.V1.Users.Mappers.UserMapper),
            typeof(Services.Users.Mappers.UserMapper));

        builder.Services.AddFluentValidationAutoValidation();

        builder.Services.AddValidatorsFromAssemblyContaining<CreateUserRequestBodyValidator>();

        var identityStoreOptions = builder.Configuration.GetSection("IdentityStore").Get<IdentityStoreOptions>();

        builder.Services.AddDbContext<IdpDbContext>(options =>
            options.UseSqlServer(identityStoreOptions.ConnectionString,
                x =>
                {
                    x.MigrationsAssembly(identityStoreOptions.MigrationsAssembly);
                    x.MigrationsHistoryTable(identityStoreOptions.MigrationsHistoryTable, identityStoreOptions.Schema);
                }
            ));

        builder.Services.AddIdentity<IdpUser, IdentityRole>()
            .AddEntityFrameworkStores<IdpDbContext>()
            .AddDefaultTokenProviders();

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
}