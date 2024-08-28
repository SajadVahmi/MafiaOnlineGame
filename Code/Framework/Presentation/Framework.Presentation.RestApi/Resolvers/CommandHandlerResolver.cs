using Framework.Core.Application.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Presentation.RestApi.Resolvers;

public class CommandHandlerResolver : ICommandHandlerResolver
{
    private readonly IServiceProvider _serviceProvider;

    public CommandHandlerResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICommandHandler<TCommand> ResolveHandlers<TCommand>(TCommand command) where TCommand : ICommand
    {
        return _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
    }
}