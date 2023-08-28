using Framework.Core.ApplicationServices.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Configuration.Scrutor;

public class ScrutorQueryHandlerResolver : IQueryHandlerResolver
{
    private readonly IServiceProvider _serviceProvider;

    public ScrutorQueryHandlerResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public IQueryHandler<TQuery, TResponse> ResolveHandlers<TQuery, TResponse>(TQuery request) where TQuery : class, IQuery<TResponse>
    {
        return _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResponse>>();
    }
}