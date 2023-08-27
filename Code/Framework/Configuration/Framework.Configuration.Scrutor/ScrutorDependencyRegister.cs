﻿using Framework.Core.ApplicationServices.Commands;
using Framework.Core.ApplicationServices.Queries;
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

    public void RegisterScoped<TService, TImplementation>() where TImplementation : notnull, TService
    {
        _services.AddScoped(typeof(TService), typeof(TImplementation));
    }


    public void RegisterSingleton<TService, TImplementation>() where TImplementation : TService
    {
        _services.AddSingleton(typeof(TService), typeof(TImplementation));
    }

    public void RegisterSingleton<TService, TInstance>(TInstance instance) where TService : class where TInstance : TService
    {
        _services.AddSingleton(typeof(TService), instance);
    }

    public void RegisterTransient<TService, TImplementation>() where TImplementation : TService
    {
        _services.AddTransient(typeof(TService), typeof(TImplementation));
    }
}