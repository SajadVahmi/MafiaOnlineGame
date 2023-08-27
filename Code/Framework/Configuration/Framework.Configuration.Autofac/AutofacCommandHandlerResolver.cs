using Autofac;
using Framework.Core.ApplicationServices.Commands;

namespace Framework.Configuration.Autofac
{
    public class AutofacCommandHandlerResolver : ICommandHandlerResolver
    {
        private readonly IComponentContext _context;

        public AutofacCommandHandlerResolver(IComponentContext context)
        {
            _context = context;
        }


        public ICommandHandler<TCommand> ResolveHandlers<TCommand>(TCommand command) where TCommand : ICommand
        {
            return _context.Resolve<ICommandHandler<TCommand>>();
        }

        public ICommandHandler<TCommand, TData> ResolveHandlers<TCommand, TData>(TCommand command) where TCommand : ICommand<TData>
        {
            return _context.Resolve<ICommandHandler<TCommand,TData>>();
        }
    }
}
