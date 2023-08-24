using IDP.Shared.IdentityStore.DbContexts;
using IDP.Shared.IdentityStore.Models;
using IDP.Shared.IdentityStore.Options;
using IDP.STS.ConfigurationStore.Options;
using IDP.STS.OperationalStore.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IDP.STS.UI;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var configurationStoreOptions = builder.Configuration.GetSection("ConfigurationStore").Get<ConfiguratonStoreOptions>();

        var operationalStoreOptions = builder.Configuration.GetSection("OperationalStore").Get<OperationalStoreOptions>();

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

        builder.Services.AddIdentityServer()
            .AddAspNetIdentity<IdpUser>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(configurationStoreOptions.ConnectionString,
                        sql => sql.MigrationsAssembly(configurationStoreOptions.MigrationsAssembly)
                            .MigrationsHistoryTable(configurationStoreOptions.MigrationsHistoryTable, configurationStoreOptions.Schema));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(operationalStoreOptions.ConnectionString,
                        sql => sql.MigrationsAssembly(operationalStoreOptions.MigrationsAssembly)
                            .MigrationsHistoryTable(operationalStoreOptions.MigrationsHistoryTable,
                                operationalStoreOptions.Schema));

                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 3600;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients);

        builder.Services.AddRazorPages();

        builder.Services.AddAuthentication();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}