using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.CQRS.Abstraction.Queries;

namespace Library.Core.CQRS.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public Dispatcher(IServiceProvider serviceProvider) => this.serviceProvider = serviceProvider;

        public TResponse DispatchQuery<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>
        {
            var queryHandler = serviceProvider.GetService<IQueryHandler<TQuery, TResponse>>();

            return queryHandler.Handle(query);
        }

        public void DispatchCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandHandler = serviceProvider.GetService<ICommandHandler<TCommand>>();

            commandHandler.Handle(command);
        }
    }
}
