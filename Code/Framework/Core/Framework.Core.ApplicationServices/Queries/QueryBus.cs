using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.ApplicationServices.Queries
{
    public class QueryBus : IQueryBus
    {
        private readonly IQueryHandlerResolver _handlerResolver;

        public QueryBus(IQueryHandlerResolver handlerResolver)
        {
            _handlerResolver = handlerResolver;
        }

        public Task<QueryResult<TResponse>> ExecuteAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken) where TQuery : class, IQuery<TResponse>
        {

            var handler = _handlerResolver.ResolveHandlers<TQuery, TResponse>(query);
            return handler.HandleAsync(query, cancellationToken);
        }

    }
}
