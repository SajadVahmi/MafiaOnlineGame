using Autofac;
using Framework.Core.ApplicationServices.Commands;
using Framework.Core.ApplicationServices.Queries;

namespace Framework.Configuration.Autofac;

public class AutofacModule : IFrameworkIocModule
{
    private readonly ContainerBuilder _builder;
    public AutofacModule(ContainerBuilder builder)
    {
        _builder = builder;
    }
    public void Register(IDependencyRegister dependencyRegister)
    {
        dependencyRegister.RegisterScoped<ICommandHandlerResolver, AutofacCommandHandlerResolver>();
        dependencyRegister.RegisterScoped<IQueryHandlerResolver, AutofacQueryHandlerResolver>();
    }

    public IDependencyRegister CreateServiceRegistry()
    {
        return new AutofacDependencyRegister(_builder);
    }
}