namespace Framework.Core.Application.Commands;

public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
where TCommand : ICommand
{
    public abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);

}

public abstract class CommandHandler<TCommand,TResult> : ICommandHandler<TCommand,TResult>
    where TCommand : ICommand<TResult>
{
    public abstract Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);

}

