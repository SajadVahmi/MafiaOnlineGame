using Framework.Core.Domian.Data;

namespace Framework.Core.ApplicationServices.Commands
{
    public class TransactionalCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _commandHandler;

        private readonly IUnitOfWork _unitOfWork;

        public TransactionalCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IUnitOfWork unitOfWork)
        {
            _commandHandler = commandHandler;

            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                await _unitOfWork.BeginAsync();

                await _commandHandler.HandleAsync(command, cancellationToken);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                try
                {
                    await _unitOfWork.RollbackAsync();
                }
                catch (Exception)
                {
                    throw;
                }

                throw;
            }
        }
    }
}
