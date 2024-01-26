using Framework.Core.Contracts;
using Framework.JsonSerializer.NewtonSoft;
using Framework.Persistence.EF;
using Framework.Presentation.AspNetCore.Helpers;
using Framework.Test.EntityFramework;
using Framework.Test.Stubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Players.Persistence.SQL.DbContexts;
using Players.SharedTestClasses.Shared.Data;
using static Players.SharedTestClasses.Shared.Data.UserTestData;

namespace Players.Infrastructure.PersistTests.Shared;

public class PlayersPersistTestFixture : EfCoreSandboxTest<PlayersDbContext>
{

    public IEntityFrameworkSequenceService SequenceService;

    public PlayersPersistTestFixture()
        : base(CreatePlayersDbContextOptions())
    {


        SequenceService = new EntityFrameworkSequenceService(DbContext);
    }

    private static DbContextOptions CreatePlayersDbContextOptions()
    {
        var services = BuildServiceCollection();

        var builder = new DbContextOptionsBuilder();

        builder.UseSqlServer(GetConnectionString())
               .UseApplicationServiceProvider(services.BuildServiceProvider());

        return builder.Options;
    }

    private static IServiceCollection BuildServiceCollection()
    {
        IAuthenticatedUser authenticatedUserService = SetupAuthenticatedUserService();

        IClock clockService = SetupClockService();

        IJsonSerializerAdapter jsonSerializerAdapter = SetupJsonSerializerAdapterService();

        IServiceCollection services = new ServiceCollection();

        services.AddSingleton(authenticatedUserService);

        services.AddSingleton(clockService);

        services.AddSingleton(jsonSerializerAdapter);

        return services;
    }

    private static IJsonSerializerAdapter SetupJsonSerializerAdapterService()
    {
        return new NewtonSoftSerializerAdapter(new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,

            ContractResolver = new PrivateSetterContractResolver()
        });
    }

    private static IClock SetupClockService()
    {
        return ClockStub.InstantiateOn(DateTimeTestData.Friday29December2023);
    }

    private static IAuthenticatedUser SetupAuthenticatedUserService()
    {
        return new AuthenticatedUserStub(
             sub: AuthenticatedUser.Sub,
             userAgent: AuthenticatedUser.UserAgent,
             userIp: AuthenticatedUser.UserIp,
             userName: AuthenticatedUser.UserName,
             isCurrentUser: AuthenticatedUser.IsCurrentUser);
    }

    private static string GetConnectionString()
    {
        string? connectionString = ConfigurationReader.Read<string>("appsettings.json", "ConnectionStrings:PlayersDbContext");

        if (string.IsNullOrEmpty(connectionString)) throw new Exception("There is not any connection string with specified key in configuration file");

        return RandomConnectionString(connectionString);
    }

}
