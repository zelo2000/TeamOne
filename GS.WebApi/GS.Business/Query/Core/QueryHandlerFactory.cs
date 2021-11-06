using GS.Business.Infrastructure.Query;
using System;

namespace GS.Business.Query.Core
{
    public class QueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IQueryHandler<TQuery, TResult> Create<TQuery, TResult>(TQuery query) where TQuery : IQuery where TResult : class
        {
            var genericType = typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(TResult));

            return _serviceProvider.GetService(genericType) as IQueryHandler<TQuery, TResult>;
        }
    }
}
