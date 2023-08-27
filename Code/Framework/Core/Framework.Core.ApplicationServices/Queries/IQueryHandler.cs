using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.ApplicationServices.Queries
{
    public interface IQueryHandler<in TQuery, TResponse>
        where TQuery : class, IQuery<TResponse>
    {
        Task<QueryResult<TResponse>> HandleAsync(TQuery request,CancellationToken cancellationToken=default);
    }
}
