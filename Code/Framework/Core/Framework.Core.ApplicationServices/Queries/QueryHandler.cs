using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Core.ApplicationServices.Services;

namespace Framework.Core.ApplicationServices.Queries
{
    public abstract class QueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse>
        where TQuery : class, IQuery<TResponse>
    {
        protected readonly QueryResult<TResponse> result = new();

        protected virtual Task<QueryResult<TResponse>> ResultAsync(TResponse data, ApplicationServiceStatus status)
        {
            result.Data = data;
            result.Status = status;
            return Task.FromResult(result);
        }

        protected virtual QueryResult<TResponse> Result(TResponse data, ApplicationServiceStatus status)
        {
            result.Data = data;
            result.Status = status;
            return result;
        }

        protected virtual Task<QueryResult<TResponse>> ResultAsync(TResponse data)
        {
            var status = data != null ? ApplicationServiceStatus.Ok : ApplicationServiceStatus.NotFound;
            return ResultAsync(data, status);
        }

        protected virtual QueryResult<TResponse> Result(TResponse data)
        {
            var status = data != null ? ApplicationServiceStatus.Ok : ApplicationServiceStatus.NotFound;
            return Result(data, status);
        }

        protected void AddMessage(string message)
        {
            result.AddMessage(message);
            //TODO: add translator
            //result.AddMessage(_zaminServices.Translator[message]);
        }

        //protected void AddMessage(string message, params string[] arguments)
        //{
        //    result.AddMessage(_zaminServices.Translator[message, arguments]);
        //}

        public abstract Task<QueryResult<TResponse>> HandleAsync(TQuery query,CancellationToken cancellationToken=default);
    }
}
