using IDP.Shared.IdentityStore.DbContexts;
using IDP.Shared.IdentityStore.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Administration.Api.Test.Integration.Fixtures
{
    public class IdpWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        private static string _appsettingsName = "appsettings.Test.json";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.ConfigureTestServices(Services =>
            {
                OverrideDatabaseConfigurations(Services);
            });
        }


        private void OverrideDatabaseConfigurations(IServiceCollection services)
        {
            services.RemoveAll(typeof(DbContextOptions<IdpDbContext>));

            var identityStoreOptions = GetIdpDbOptions();

            var idpDbContext = CreateIdpDbContext(services, identityStoreOptions);

            idpDbContext.Database.EnsureDeleted();
        }

        private static IdpDbContext CreateIdpDbContext(IServiceCollection services, IdentityStoreOptions identityStoreOptions)
        {
            services.AddDbContext<IdpDbContext>(options =>
            options.UseSqlServer(identityStoreOptions.ConnectionString,
               x =>
               {
                   x.MigrationsAssembly(identityStoreOptions.MigrationsAssembly);
                   x.MigrationsHistoryTable(identityStoreOptions.MigrationsHistoryTable, identityStoreOptions.Schema);
               })

            );

            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            var idpDbContext = scope.ServiceProvider.GetRequiredService<IdpDbContext>();
            return idpDbContext;
        }

        private static IdentityStoreOptions GetIdpDbOptions()
        {
            var configuration = new ConfigurationBuilder()
           .AddTestConfiguration(_appsettingsName)
           .Build();

            var identityStoreOptions = configuration.GetSection("IdentityStore").Get<IdentityStoreOptions>();

            if (identityStoreOptions is null)
                throw new Exception($"There is'nt any IdentityStore in  {_appsettingsName} file .");

            return identityStoreOptions;
        }

    }
}
