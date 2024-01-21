using Autofac;
using Framework.Core.ApplicationServices.ApplicationServices;
using Framework.Core.ApplicationServices.Commands;
using Framework.Core.ApplicationServices.Queries;
using Framework.Core.Domian.Data;
using Framework.Core.Domian.DomainServices;
using System.Reflection;

namespace Framework.Configuration.Autofac;

public class AutofacDependencyRegister : IDependencyRegister
{
    private readonly ContainerBuilder _container;

    public AutofacDependencyRegister(ContainerBuilder container)
    {
        _container = container;
    }

    public void RegisterDomainServices(Assembly assembly)
    {
        _container.RegisterAssemblyTypes(assembly)
            .As(type => type.GetInterfaces()
                .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IDomainService))))
            .InstancePerLifetimeScope();
    }

    public void RegisterApplicationServices(Assembly assembly)
    {
        _container.RegisterAssemblyTypes(assembly)
            .As(type => type.GetInterfaces()
                .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IApplicationService))))
            .InstancePerLifetimeScope();
    }

    public void RegisterCommandHandlers(Assembly assembly)
    {
        _container.RegisterAssemblyTypes(assembly)
            .As(type => type.GetInterfaces()
                .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(ICommandHandler<>))))
            .InstancePerLifetimeScope();
    }

    public void RegisterQueryHandlers(Assembly assembly)
    {
        _container
            .RegisterAssemblyTypes(assembly)
            .AsClosedTypesOf(typeof(IQueryHandler<,>))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }

    public void RegisterRepositories(Assembly assembly)
    {
        _container.RegisterAssemblyTypes(assembly)
            .As(type => type.GetInterfaces()
                .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IRepository<,>))))
            .InstancePerLifetimeScope();
    }

    public void RegisterScoped<TService, TImplementation>() where TImplementation : notnull, TService where TService : notnull
    {
        _container.RegisterType<TImplementation>().As<TService>().InstancePerLifetimeScope();
    }

    public void RegisterScoped<TService>(Func<TService> factory, Action<TService>? release = null) where TService : class
    {
        var registration = _container.Register(a => factory.Invoke()).InstancePerLifetimeScope();
        if (release != null)

            registration.OnRelease(release);
    }

    public void RegisterSingleton<TService, TImplementation>() where TImplementation : notnull, TService where TService : notnull
    {
        _container.RegisterType<TImplementation>().As<TService>().SingleInstance();
    }

    public void RegisterSingleton<TService, TInstance>(TInstance instance) where TService : class where TInstance : notnull, TService
    {
        _container.RegisterInstance<TService>(instance).SingleInstance();
    }

    public void RegisterSingleton<TService>(Func<TService> factory, Action<TService>? release = null) where TService : class
    {
        var registration = _container.Register(a => factory.Invoke()).SingleInstance();

        if (release != null)
            registration.OnRelease(release);
    }

    public void RegisterTransient<TService, TImplementation>() where TImplementation : notnull, TService where TService : notnull
    {
        _container.RegisterType<TImplementation>().As<TService>().InstancePerDependency();
    }

    public void RegisterDecorator<TService, TDecorator>() where TDecorator : notnull, TService where TService : notnull
    {
        _container.RegisterDecorator<TDecorator, TService>();
    }

    public void RegisterDecorator(Type service, Type decorator)
    {
        _container.RegisterGenericDecorator(decorator, service);
    }


}