using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.CQRS.Resources.Publishers.Commands;
using Library.Core.Exceptions.Publishers;

namespace Library.Core.CQRS.Resources.Publishers.Handlers
{
    public class DeletePublisherCommandHandler : ICommandHandler<DeletePublisherCommand>
    {
        private readonly IDataBaseContext context;
        public DeletePublisherCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(DeletePublisherCommand command)
        {
            var publisher = context.Publishers.FirstOrDefault(x => x.PGID == command.PGID);

            if (publisher == null)
                throw new PublisherNotFoundException("Nie udało się znaleźć wydawcy!");

            context.DeletePublisher(publisher);
            context.SaveChanges();
        }
    }
}
