using Framework.Core.ApplicationServices.Commands;
using Framework.Core.ApplicationServices.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Configuration.Scrutor;

public class ScrutorModule : IFrameworkIocModule
{
    private readonly IServiceCollection _serviceCollection;

    public ScrutorModule(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public IDependencyRegister CreateServiceRegistry()
    {
        return new ScrutorDependencyRegister(_serviceCollection);
    }

    public void Register(IDependencyRegister dependencyRegister)
    {
        dependencyRegister.RegisterScoped<ICommandHandlerResolver, ScrutorCommandHandlerResolver>();
        dependencyRegister.RegisterScoped<IQueryHandlerResolver, ScrutorQueryHandlerResolver>();
    }
}