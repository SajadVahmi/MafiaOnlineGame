using Framework.Core.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Presentation.RestApi.Resolvers;

public class QueryHandlerResolver(IServiceProvider serviceProvider) : IQueryHandlerResolver
{

    public IQueryHandler<TQuery, TResult> ResolveHandlers<TQuery, TResult>(TQuery request) where TQuery : class, IQuery<TResult>
    {
        return serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
    }
}