using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.ApplicationServices.Queries
{
    public interface IQueryBus
    {
        Task<QueryResult<TResponse>> ExecuteAsync<TQuery, TResponse>(TQuery query,CancellationToken cancellationToken=default) where TQuery : class, IQuery<TResponse>;
    }

   
}
