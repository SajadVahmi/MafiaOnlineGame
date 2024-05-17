using Framework.Core.Application.Commands;
using Framework.Core.Application.Queries;
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