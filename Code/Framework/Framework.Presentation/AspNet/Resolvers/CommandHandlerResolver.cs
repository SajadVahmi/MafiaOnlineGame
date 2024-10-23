using Framework.Core.Application.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Presentation.AspNet.Resolvers;

public class CommandHandlerResolver(IServiceProvider serviceProvider) : ICommandHandlerResolver
{
    public ICommandHandler<TCommand> ResolveHandlers<TCommand>(TCommand command) where TCommand : ICommand
    {
        return serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
    }

    public ICommandHandler<TCommand, TResult> ResolveHandlers<TCommand, TResult>(TCommand command) where TCommand : ICommand<TResult>
    {
        return serviceProvider.GetRequiredService<ICommandHandler<TCommand,TResult>>();
    }
}