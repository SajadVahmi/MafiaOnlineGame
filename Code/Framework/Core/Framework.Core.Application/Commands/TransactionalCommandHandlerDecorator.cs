﻿using Framework.Core.Domain.Data;

namespace Framework.Core.Application.Commands
{
    public class TransactionalCommandHandlerDecorator<TCommand>(
        ICommandHandler<TCommand> commandHandler,
        IUnitOfWork unitOfWork)
        : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                await unitOfWork.BeginAsync();

                await commandHandler.HandleAsync(command, cancellationToken);

                await unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await unitOfWork.RollbackAsync();

                throw;
            }
        }
    }
}
