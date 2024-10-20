using System.Reflection;
using EventStore.Client;
using Framework.Core.Application.Commands;
using Framework.Core.Domain.Aggregates;
using Framework.Core.Domain.DomainServices;
using Framework.Core.Domain.Events;
using Framework.Core.Domain.Queries;
using Framework.Core.Domain.Repository;
using Framework.Core.Domain.Snapshots;
using Framework.Core.ServiceContracts;
using Framework.Core.Services;
using Framework.Persistence.EventStore;
using Framework.Persistence.EventStore.Repositories;
using Framework.Presentation.AspNet.Resolvers;
using Framework.Presentation.AspNet.Services;
using Framework.Tools.AutoMapper;
using Framework.Tools.NewtonSoft;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Framework.Presentation.AspNet.Extensions;

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
       
        services.AddScoped<IQueryHandlerResolver, QueryHandlerResolver>();

        services.AddSingleton<IClock, UtcClock>();

        services.AddTransient<ICommandBus, CommandBus>();
        
        services.AddTransient<IQueryBus, QueryBus>();

        services.AddSingleton<IIdGenerator, GuidIdGenerator>();

        return services;
    }

    public static IServiceCollection AddNewtonSoft(this IServiceCollection services,JsonSerializerSettings? jsonSerializerSettings=null)
    {
        jsonSerializerSettings??= new JsonSerializerSettings()
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

        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableToAny(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddQueryHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableToAny(typeof(IQueryHandler<,>)))
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

    public static IServiceCollection AddAutoMapperConfigs(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddAutoMapper(assemblies);

        services.AddSingleton<IMapperAdapter, AutoMapperAdapter>();

        return services;
    }

    public static IServiceCollection AddEventSourceDatabase(this IServiceCollection services
        , string connectionString, params Assembly[] assemblies)
    {
        services.AddSingleton<IAggregateFactory>(_ => new AggregateFactory());
        services.AddSingleton(_ => CreateEventTypeResolver(assemblies));
        services.AddSingleton(_ => CreateEventStoreClient(connectionString));
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
    private static EventStoreClient CreateEventStoreClient(string connectionString)
    {
        var settings = EventStoreClientSettings.Create(connectionString);

        return new EventStoreClient(settings);
    }

}
