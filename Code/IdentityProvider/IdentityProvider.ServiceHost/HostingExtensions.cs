using IdentityProvider.Persistence.DbContexts;
using IdentityProvider.Persistence.Models;
using IdentityProvider.Persistence.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityProvider.ServiceHost;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();

        var configurationStoreOptions = builder.Configuration.GetSection("ConfigurationStore").Get<ConfigurationStoreOptions>();
        ArgumentNullException.ThrowIfNull(configurationStoreOptions);
        ArgumentNullException.ThrowIfNull(configurationStoreOptions.ConnectionString);
        ArgumentNullException.ThrowIfNull(configurationStoreOptions.MigrationsHistoryTable);
        ArgumentNullException.ThrowIfNull(configurationStoreOptions.Schema);


        var operationalStoreOptions = builder.Configuration.GetSection("OperationalStore").Get<OperationalStoreOptions>();
        ArgumentNullException.ThrowIfNull(operationalStoreOptions);
        ArgumentNullException.ThrowIfNull(operationalStoreOptions.ConnectionString);
        ArgumentNullException.ThrowIfNull(operationalStoreOptions.MigrationsHistoryTable);
        ArgumentNullException.ThrowIfNull(operationalStoreOptions.Schema);

        var identityStoreOptions = builder.Configuration.GetSection("IdentityStore").Get<IdentityStoreOptions>();
        ArgumentNullException.ThrowIfNull(identityStoreOptions);
        ArgumentNullException.ThrowIfNull(identityStoreOptions.ConnectionString);
        ArgumentNullException.ThrowIfNull(identityStoreOptions.MigrationsHistoryTable);
        ArgumentNullException.ThrowIfNull(identityStoreOptions.Schema);

        builder.Services.AddDbContext<IdpDbContext>(options =>
            options.UseSqlServer(identityStoreOptions.ConnectionString,
                x =>
                {
                    x.MigrationsAssembly(typeof(IdpDbContext).Assembly.FullName);
                    x.MigrationsHistoryTable(identityStoreOptions.MigrationsHistoryTable, identityStoreOptions.Schema);
                }
            ));

        builder.Services.AddIdentity<IdpUser, IdentityRole>()
            .AddEntityFrameworkStores<IdpDbContext>()
            .AddDefaultTokenProviders();

        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(operationalStoreOptions.ConnectionString,
                        sql => sql.MigrationsAssembly(typeof(IdpDbContext).Assembly.FullName)
                            .MigrationsHistoryTable(operationalStoreOptions.MigrationsHistoryTable,
                                operationalStoreOptions.Schema));

                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 3600;
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(configurationStoreOptions.ConnectionString,
                        sql => sql.MigrationsAssembly(typeof(IdpDbContext).Assembly.FullName)
                            .MigrationsHistoryTable(configurationStoreOptions.MigrationsHistoryTable, configurationStoreOptions.Schema));
            })
            
            .AddAspNetIdentity<IdpUser>();


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