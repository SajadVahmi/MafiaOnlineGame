using Autofac;
using Framework.Core.ApplicationServices.Queries;

namespace Framework.Configuration.Autofac;

public class AutofacQueryHandlerResolver(ILifetimeScope lifetimeScope) : IQueryHandlerResolver
{
    public IQueryHandler<TQuery, TResponse> ResolveHandlers<TQuery, TResponse>(TQuery request) where TQuery : class, IQuery<TResponse>
    {
        return lifetimeScope.Resolve<IQueryHandler<TQuery, TResponse>>();
    }
}