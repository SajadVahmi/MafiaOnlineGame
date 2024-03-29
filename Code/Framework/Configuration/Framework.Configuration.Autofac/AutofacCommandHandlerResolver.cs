﻿using Autofac;
using Framework.Core.ApplicationServices.Commands;

namespace Framework.Configuration.Autofac;

public class AutofacCommandHandlerResolver(IComponentContext context) : ICommandHandlerResolver
{
    public ICommandHandler<TCommand> ResolveHandlers<TCommand>(TCommand command) where TCommand : ICommand
    {
        return context.Resolve<ICommandHandler<TCommand>>();
    }


}