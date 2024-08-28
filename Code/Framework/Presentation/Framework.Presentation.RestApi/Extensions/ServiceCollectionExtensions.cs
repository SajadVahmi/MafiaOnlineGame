using System.Reflection;
using EventStore.ClientAPI;
using Framework.Core.Application.Commands;
using Framework.Core.Contracts;
using Framework.Core.Domain.Data;
using Framework.Core.Domain.DomainServices;
using Framework.Core.Services;
using Framework.JsonSerializer.NewtonSoft;
using Framework.Persistence.EventStore;
using Framework.Presentation.RestApi.Resolvers;
using Framework.Presentation.RestApi.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Framework.Core.Domain.Aggregates;
using Framework.Core.Domain.Snapshots;
using Framework.Core.Domain.Events;
using Framework.Persistence.EventStore.Repositories;

namespace Framework.Presentation.RestApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHttpContextServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.TryAddSingleton<IAuthenticatedUser, AspNetCoreAuthenticatedUser>();

        return services;
    }

    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandlerResolver, CommandHandlerResolver>();

        services.AddSingleton<IClock, UtcClock>();

        services.AddTransient<ICommandBus, CommandBus>();

        services.AddSingleton<IIdGenerator, GuidIdGenerator>();

        var jsonSerializerSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,

            ContractResolver = new PrivateSetterContractResolver()
        };

        services.AddSingleton<IJsonSerializerAdapter>(_ => new NewtonSoftSerializerAdapter(jsonSerializerSettings));

        return services;
    }


    public static IServiceCollection AddCommandHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableToAny(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddDomainServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableToAny(typeof(IDomainService)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableToAny(typeof(IRepository)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }


    public static IServiceCollection AddEventSourceRepositories(this IServiceCollection services
        , string connectionString, params Assembly[] assemblies)
    {
        services.AddSingleton<IAggregateFactory>(_ => new AggregateFactory());
        services.AddSingleton(_ => CreateEventTypeResolver(assemblies));
        services.AddSingleton(_ => CreateEventStoreConnection(connectionString));
        services.AddSingleton<IEventStore, EventStoreDb>();
        services.AddSingleton<ISnapshotStore, SqlSnapshotStore>();

        services.AddScoped(typeof(IEventSourceRepository<,>), typeof(EventSourceRepository<,>));


        return services;
    }

    private static IEventTypeResolver CreateEventTypeResolver(Assembly[] assemblies)
    {
        var typeResolver = new EventTypeResolver();
        foreach (var assembly in assemblies)
            typeResolver.AddTypesFromAssembly(assembly);
        return typeResolver;
    }
    private static IEventStoreConnection CreateEventStoreConnection(string connectionString)
    {
        var conn = EventStoreConnection.Create(new Uri(connectionString));
        conn.ConnectAsync().Wait();
        return conn;
    }

}
