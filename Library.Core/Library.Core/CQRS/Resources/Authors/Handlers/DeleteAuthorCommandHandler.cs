using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.CQRS.Resources.Authors.Commands;
using Library.Core.Exceptions.Authors;

namespace Library.Core.CQRS.Resources.Authors.Handlers
{
    public class DeleteAuthorCommandHandler : ICommandHandler<DeleteAuthorCommand>
    {
        private readonly IDataBaseContext context;
        public DeleteAuthorCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(DeleteAuthorCommand command)
        {
            var Author = context.AllAuthors.FirstOrDefault(x => x.AGID == command.AGID);

            if (Author == null)
                throw new AuthorNotFoundException("Nie udało się znaleźć książki!");

            context.DeleteAuthor(Author);
            context.SaveChanges();
        }
    }
}
