using Autofac;
using Framework.Core.Application.Commands;
using Framework.Core.Application.Queries;


namespace Framework.Configuration.Autofac;

public class AutofacModule(ContainerBuilder builder) : IFrameworkIocModule
{
    public void Register(IDependencyRegister dependencyRegister)
    {
        dependencyRegister.RegisterScoped<ICommandHandlerResolver, AutofacCommandHandlerResolver>();
        dependencyRegister.RegisterScoped<IQueryHandlerResolver, AutofacQueryHandlerResolver>();
    }

    public IDependencyRegister CreateServiceRegistry()
    {
        return new AutofacDependencyRegister(builder);
    }
}