using System.Threading.Tasks;

namespace GS.Business.Infrastructure.Query
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
    }

    public interface IQueryHandler
    {
        Task<TResult> Handle<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult> where TResult : class;
    }
}
