using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.CQRS.Abstraction.Queries;

namespace Library.Core.CQRS.Dispatcher
{
    public interface IDispatcher
    {
        TResponse DispatchQuery<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;
        void DispatchCommand<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
