using Autofac;
using Framework.Core.ApplicationServices.Commands;
using Framework.Core.ApplicationServices.Queries;
using System.Reflection;
using Framework.Core.ApplicationServices.ApplicationServices;
using Framework.Core.Domian.DomainServices;
using Framework.Core.Domian.Events;

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

    public void RegisterScoped<TService, TImplementation>() where TImplementation : notnull, TService
    {
        _container.RegisterType<TImplementation>().As<TService>().InstancePerLifetimeScope();
    }

    public void RegisterSingleton<TService, TImplementation>() where TImplementation : TService
    {
        _container.RegisterType<TImplementation>().As<TService>().SingleInstance();
    }

    public void RegisterSingleton<TService, TInstance>(TInstance instance)
        where TService : class
        where TInstance : TService
    {
        _container.RegisterInstance<TService>(instance).SingleInstance();
    }

    public void RegisterTransient<TService, TImplementation>() where TImplementation : TService
    {
        _container.RegisterType<TImplementation>().As<TService>().InstancePerDependency();
    }

    public void RegisterDecorator<TService, TDecorator>() where TDecorator : TService
    {
        _container.RegisterDecorator<TDecorator, TService>();
    }

    public void RegisterDecorator(Type service, Type decorator)
    {
        _container.RegisterGenericDecorator(decorator, service);
    }
}