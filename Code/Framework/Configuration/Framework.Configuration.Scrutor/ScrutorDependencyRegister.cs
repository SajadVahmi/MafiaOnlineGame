using Framework.Core.ApplicationServices.ApplicationServices;
using Framework.Core.ApplicationServices.Commands;
using Framework.Core.ApplicationServices.Queries;
using Framework.Core.Domian.Data;
using Framework.Core.Domian.DomainServices;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Framework.Configuration.Scrutor;

public class ScrutorDependencyRegister : IDependencyRegister
{
    private readonly IServiceCollection _services;

    public ScrutorDependencyRegister(IServiceCollection services)
    {
        _services = services;
    }

    public void RegisterDomainServices(Assembly assembly)
    {
        _services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableToAny(typeof(IDomainService)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    public void RegisterApplicationServices(Assembly assembly)
    {
        _services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableToAny(typeof(IApplicationService)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    public void RegisterCommandHandlers(Assembly assembly)
    {
        _services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableToAny(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    public void RegisterQueryHandlers(Assembly assembly)
    {
        _services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableToAny(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    public void RegisterRepositories(Assembly assembly)
    {
        _services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableToAny(typeof(IRepository<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    public void RegisterScoped<TService, TImplementation>() where TImplementation : notnull, TService where TService : notnull
    {
        _services.AddScoped(typeof(TService), typeof(TImplementation));
    }

    public void RegisterScoped<TService>(Func<TService> factory, Action<TService>? release = null) where TService : class
    {
        _services.AddScoped(s => factory.Invoke());
    }

    public void RegisterSingleton<TService, TImplementation>() where TImplementation : notnull, TService where TService : notnull
    {
        _services.AddSingleton(typeof(TService), typeof(TImplementation));
    }

    public void RegisterSingleton<TService, TInstance>(TInstance instance) where TService : class where TInstance : TService
    {
        _services.AddSingleton(typeof(TService), instance);
    }

    public void RegisterSingleton<TService>(Func<TService> factory, Action<TService>? release = null) where TService : class
    {
        _services.AddSingleton(s => factory.Invoke());
    }

    public void RegisterTransient<TService, TImplementation>() where TImplementation : notnull, TService where TService : notnull
    {
        _services.AddTransient(typeof(TService), typeof(TImplementation));
    }

    public void RegisterDecorator<TService, TDecorator>() where TDecorator : notnull, TService where TService : notnull
    {
        _services.Decorate<TService, TDecorator>();
    }

    public void RegisterDecorator(Type service, Type decorator)
    {
        _services.Decorate(service, decorator);
    }

   
}