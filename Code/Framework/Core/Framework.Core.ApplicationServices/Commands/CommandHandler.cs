namespace Framework.Core.ApplicationServices.Commands;

public abstract class AsyncCommandHandler<TCommand> : ICommandHandler<TCommand>
where TCommand : ICommand
{
    protected AsyncCommandHandler() { }
    public abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);

}

