using Framework.Core.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Configuration.Scrutor;

public class ScrutorQueryHandlerResolver(IServiceProvider serviceProvider) : IQueryHandlerResolver
{
    public IQueryHandler<TQuery, TResponse> ResolveHandlers<TQuery, TResponse>(TQuery request) where TQuery : class, IQuery<TResponse>
    {
        return serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResponse>>();
    }
}