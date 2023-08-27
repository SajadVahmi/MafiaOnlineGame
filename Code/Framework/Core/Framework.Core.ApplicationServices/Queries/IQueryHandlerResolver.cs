using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.ApplicationServices.Queries
{
    public interface IQueryHandlerResolver
    {
        IQueryHandler<TRequest, TResponse> ResolveHandlers<TRequest, TResponse>(TRequest request) where TRequest : class, IQuery<TResponse>;
    }
}

