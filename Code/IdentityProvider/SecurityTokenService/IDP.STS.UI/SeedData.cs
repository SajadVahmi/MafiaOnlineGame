using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityModel;
using IDP.Shared.IdentityStore.DbContexts;
using IDP.Shared.IdentityStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;

namespace IDP.STS.UI;

public class SeedData
{
    public static void EnsureSeedIdentitySeedData(WebApplication app)
    {
        using IServiceScope scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var dbContext = scope.ServiceProvider.GetService<IdpDbContext>();

        dbContext.Database.Migrate();

        if (dbContext.Users.Any())
        {
            Log.Debug("Users already exists");

            return;
        }

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdpUser>>();

        Log.Debug("Going to create Sajad.");

        IdpUser sajad = new IdpUser
        {
            Id = DateTime.Now.Ticks.ToString(),
            UserName = "sajad",
            Email = "sajad.vahmi@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "+989387607524",
            PhoneNumberConfirmed = true
        };

        var result = userManager.CreateAsync(sajad, "Pass123$").Result;

        if (!result.Succeeded)
            throw new Exception(result.Errors.First().Description);


        result = userManager.AddClaimsAsync(sajad, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Sajad Vahmi"),
                            new Claim(JwtClaimTypes.GivenName, "Sajad"),
                            new Claim(JwtClaimTypes.FamilyName, "Vahmi"),
                            new Claim(JwtClaimTypes.Email,sajad.Email)
                        }).Result;

        if (!result.Succeeded)
            throw new Exception(result.Errors.First().Description);

        Log.Debug("Sajad created.");

    }
    public static void EnsureSeedConfigurationSeedData(WebApplication app)
    {
        using IServiceScope scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var dbContext = scope.ServiceProvider.GetService<ConfigurationDbContext>();

        dbContext.Database.Migrate();

        if (dbContext.ApiScopes.Any() || dbContext.ApiScopes.Any() || dbContext.Clients.Any())
        {

            Log.Debug("Configuration data already exists");

            return;
        }

        Log.Debug("Going to populate ApiScopes.");

        foreach (var apiScope in Config.ApiScopes.ToList())
            dbContext.ApiScopes.Add(apiScope.ToEntity());

        Log.Debug("ApiScopes populated.");


        Log.Debug("Going to populate IdentiyResources.");

        foreach (var identiyResource in Config.IdentityResources.ToList())
            dbContext.IdentityResources.Add(identiyResource.ToEntity());

        Log.Debug("IdentiyResources populated.");

        dbContext.SaveChanges();

        Log.Debug("Going to populate IdentiyResources.");

        foreach (var client in Config.Clients.ToList())
            dbContext.Clients.Add(client.ToEntity());

        Log.Debug("clients populated.");

        dbContext.SaveChanges();

    }

    public static void EnsureSeedOperationalSeedData(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetService<PersistedGrantDbContext>();
            context.Database.Migrate();

        }
    }
}





