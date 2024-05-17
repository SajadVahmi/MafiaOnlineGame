using Autofac;
using Framework.Core.Domain.Data;
using Framework.Core.Domain.DomainServices;
using System.Reflection;
using Framework.Core.Application.ApplicationServices;
using Framework.Core.Application.Commands;
using Framework.Core.Application.Queries;

namespace Framework.Configuration.Autofac;

public class AutofacDependencyRegister(ContainerBuilder container) : IDependencyRegister
{
    public void RegisterDomainServices(Assembly assembly)
    {
        container.RegisterAssemblyTypes(assembly)
            .As(type => type.GetInterfaces()
                .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IDomainService))))
            .InstancePerLifetimeScope();
    }

    public void RegisterApplicationServices(Assembly assembly)
    {
        container.RegisterAssemblyTypes(assembly)
            .As(type => type.GetInterfaces()
                .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IApplicationService))))
            .InstancePerLifetimeScope();
    }

    public void RegisterCommandHandlers(Assembly assembly)
    {
        container.RegisterAssemblyTypes(assembly)
            .As(type => type.GetInterfaces()
                .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(ICommandHandler<>))))
            .InstancePerLifetimeScope();
    }

    public void RegisterQueryHandlers(Assembly assembly)
    {
        container
            .RegisterAssemblyTypes(assembly)
            .AsClosedTypesOf(typeof(IQueryHandler<,>))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }

    public void RegisterRepositories(Assembly assembly)
    {
        container.RegisterAssemblyTypes(assembly)
            .As(type => type.GetInterfaces()
                .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IRepository<,>))))
            .InstancePerLifetimeScope();
    }

    public void RegisterScoped<TService, TImplementation>() where TImplementation : TService where TService : notnull
    {
        container.RegisterType<TImplementation>().As<TService>().InstancePerLifetimeScope();
    }

    public void RegisterScoped<TService>(Func<TService> factory, Action<TService>? release = null) where TService : class
    {
        var registration = container.Register(_ => factory.Invoke()).InstancePerLifetimeScope();
        if (release != null)

            registration.OnRelease(release);
    }

    public void RegisterSingleton<TService, TImplementation>() where TImplementation : TService where TService : notnull
    {
        container.RegisterType<TImplementation>().As<TService>().SingleInstance();
    }

    public void RegisterSingleton<TService, TInstance>(TInstance instance) where TService : class where TInstance : TService
    {
        container.RegisterInstance<TService>(instance).SingleInstance();
    }

    public void RegisterSingleton<TService>(Func<TService> factory, Action<TService>? release = null) where TService : class
    {
        var registration = container.Register(_ => factory.Invoke()).SingleInstance();

        if (release != null)
            registration.OnRelease(release);
    }

    public void RegisterTransient<TService, TImplementation>() where TImplementation : TService where TService : notnull
    {
        container.RegisterType<TImplementation>().As<TService>().InstancePerDependency();
    }

    public void RegisterDecorator<TService, TDecorator>() where TDecorator : TService where TService : notnull
    {
        container.RegisterDecorator<TDecorator, TService>();
    }

    public void RegisterDecorator(Type service, Type decorator)
    {
        container.RegisterGenericDecorator(decorator, service);
    }


}