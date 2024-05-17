using Framework.Core.Application.Commands;
using Framework.Core.Application.Queries;
using Framework.Core.Contracts;
using Framework.Core.Services;

namespace Framework.Configuration;

public class CoreModule : IFrameworkModule
{
    public void Register(IDependencyRegister dependencyRegister)
    {
        dependencyRegister.RegisterSingleton<IClock, UtcClock>();

        dependencyRegister.RegisterScoped<IQueryBus, QueryBus>();

        dependencyRegister.RegisterScoped<ICommandBus, CommandBus>();

        dependencyRegister.RegisterSingleton<IEventIdProvider,GuidEventIdProvider>();

    }
}