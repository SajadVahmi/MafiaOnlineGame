using Framework.Core.Application.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Presentation.RestApi.Resolvers;

public class CommandHandlerResolver(IServiceProvider serviceProvider) : ICommandHandlerResolver
{
    public ICommandHandler<TCommand> ResolveHandlers<TCommand>(TCommand command) where TCommand : ICommand
    {
        return serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
    }
}