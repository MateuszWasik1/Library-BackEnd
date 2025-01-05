namespace Library.Core.CQRS.Abstraction.Queries
{
    public interface IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        TResponse Handle(TQuery query);
    }
}
