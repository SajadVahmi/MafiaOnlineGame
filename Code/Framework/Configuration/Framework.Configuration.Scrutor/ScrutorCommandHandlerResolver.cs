using Framework.Core.Application.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Configuration.Scrutor;

public class ScrutorCommandHandlerResolver(IServiceProvider serviceProvider) : ICommandHandlerResolver
{
    public ICommandHandler<TCommand> ResolveHandlers<TCommand>(TCommand command) where TCommand : ICommand
    {
        return serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
    }
}