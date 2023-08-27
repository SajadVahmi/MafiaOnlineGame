using Autofac;
using Framework.Core.ApplicationServices.Queries;

namespace Framework.Configuration.Autofac;

public class AutofacQueryHandlerResolver : IQueryHandlerResolver
{
    private readonly ILifetimeScope _lifetimeScope;

    public AutofacQueryHandlerResolver(ILifetimeScope lifetimeScope)
    {
        _lifetimeScope = lifetimeScope;
    }
    public IQueryHandler<TQuery, TResponse> ResolveHandlers<TQuery, TResponse>(TQuery request) where TQuery : class, IQuery<TResponse>
    {
        return _lifetimeScope.Resolve<IQueryHandler<TQuery, TResponse>>();
    }
}