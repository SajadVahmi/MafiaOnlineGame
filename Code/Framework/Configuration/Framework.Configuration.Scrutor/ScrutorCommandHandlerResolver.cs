using Framework.Core.ApplicationServices.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Configuration.Scrutor;

public class ScrutorCommandHandlerResolver : ICommandHandlerResolver
{
    private readonly IServiceProvider _serviceProvider;

    public ScrutorCommandHandlerResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public ICommandHandler<TCommand> ResolveHandlers<TCommand>(TCommand command) where TCommand : ICommand
    {
        return _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
    }
}