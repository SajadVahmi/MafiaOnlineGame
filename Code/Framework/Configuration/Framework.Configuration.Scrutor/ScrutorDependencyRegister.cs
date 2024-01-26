using Framework.Core.ApplicationServices.ApplicationServices;
using Framework.Core.ApplicationServices.Commands;
using Framework.Core.ApplicationServices.Queries;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Framework.Core.Domain.Data;
using Framework.Core.Domain.DomainServices;

namespace Framework.Configuration.Scrutor;

public class ScrutorDependencyRegister(IServiceCollection services) : IDependencyRegister
{
    public void RegisterDomainServices(Assembly assembly)
    {
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableToAny(typeof(IDomainService)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    public void RegisterApplicationServices(Assembly assembly)
    {
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableToAny(typeof(IApplicationService)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    public void RegisterCommandHandlers(Assembly assembly)
    {
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableToAny(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    public void RegisterQueryHandlers(Assembly assembly)
    {
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableToAny(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    public void RegisterRepositories(Assembly assembly)
    {
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableToAny(typeof(IRepository<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    public void RegisterScoped<TService, TImplementation>() where TImplementation : TService where TService : notnull
    {
        services.AddScoped(typeof(TService), typeof(TImplementation));
    }

    public void RegisterScoped<TService>(Func<TService> factory, Action<TService>? release = null) where TService : class
    {
        services.AddScoped(_ => factory.Invoke());
    }

    public void RegisterSingleton<TService, TImplementation>() where TImplementation : TService where TService : notnull
    {
        services.AddSingleton(typeof(TService), typeof(TImplementation));
    }

    public void RegisterSingleton<TService, TInstance>(TInstance instance) where TService : class where TInstance : TService
    {
        services.AddSingleton(typeof(TService), instance);
    }

    public void RegisterSingleton<TService>(Func<TService> factory, Action<TService>? release = null) where TService : class
    {
        services.AddSingleton(_ => factory.Invoke());
    }

    public void RegisterTransient<TService, TImplementation>() where TImplementation : TService where TService : notnull
    {
        services.AddTransient(typeof(TService), typeof(TImplementation));
    }

    public void RegisterDecorator<TService, TDecorator>() where TDecorator : TService where TService : notnull
    {
        services.Decorate<TService, TDecorator>();
    }

    public void RegisterDecorator(Type service, Type decorator)
    {
        services.Decorate(service, decorator);
    }
}