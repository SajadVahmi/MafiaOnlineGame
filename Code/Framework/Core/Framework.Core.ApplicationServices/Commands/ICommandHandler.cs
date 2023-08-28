namespace Framework.Core.ApplicationServices.Commands;


public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task HandleAsync(TCommand request,CancellationToken cancellationToken= default);
}



