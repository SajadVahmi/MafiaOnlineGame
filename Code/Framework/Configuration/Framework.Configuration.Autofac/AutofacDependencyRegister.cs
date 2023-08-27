using Autofac;
using Framework.Core.ApplicationServices.Commands;
using Framework.Core.ApplicationServices.Queries;
using System.Reflection;

namespace Framework.Configuration.Autofac
{
    public class AutofacDependencyRegister : IDependencyRegister
    {
        private readonly ContainerBuilder _container;

        public AutofacDependencyRegister(ContainerBuilder container)
        {
            _container = container;
        }

        public void RegisterCommandHandlers(Assembly assembly)
        {
            _container.RegisterAssemblyTypes(assembly)
                .As(type => type.GetInterfaces()
                    .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(ICommandHandler<>))))
                .InstancePerLifetimeScope();

            _container.RegisterAssemblyTypes(assembly)
                .As(type => type.GetInterfaces()
                    .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(ICommandHandler<,>))))
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

        public void RegisterScoped<TService>(Func<TService> factory, Action<TService>? release = null) where TService : notnull
        {
            var registration = _container.Register(a => factory.Invoke()).InstancePerLifetimeScope();
            if (release != null)
                registration.OnRelease(release);
        }

        public void RegisterScoped<TService, TImplementation>() where TImplementation : notnull, TService
        {
            _container.RegisterType<TImplementation>().As<TService>().InstancePerLifetimeScope();
        }

        public void RegisterSingleton<TService>(Func<TService> factory, Action<TService>? release = null) where TService : notnull
        {
            var registration = _container.Register(a => factory.Invoke()).SingleInstance();

            if (release != null)
                registration.OnRelease(release);
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
    }
}
