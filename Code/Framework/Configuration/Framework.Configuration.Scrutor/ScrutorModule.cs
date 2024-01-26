using Framework.Core.ApplicationServices.Commands;
using Framework.Core.ApplicationServices.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Configuration.Scrutor;

public class ScrutorModule(IServiceCollection serviceCollection) : IFrameworkIocModule
{
    public IDependencyRegister CreateServiceRegistry()
    {
        return new ScrutorDependencyRegister(serviceCollection);
    }

    public void Register(IDependencyRegister dependencyRegister)
    {
        dependencyRegister.RegisterScoped<ICommandHandlerResolver, ScrutorCommandHandlerResolver>();
        dependencyRegister.RegisterScoped<IQueryHandlerResolver, ScrutorQueryHandlerResolver>();
    }
}