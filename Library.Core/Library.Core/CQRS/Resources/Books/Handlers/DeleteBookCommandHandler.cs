using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.CQRS.Resources.Books.Commands;
using Library.Core.Exceptions.Books;

namespace Library.Core.CQRS.Resources.Books.Handlers
{
    public class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand>
    {
        private readonly IDataBaseContext context;
        public DeleteBookCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(DeleteBookCommand command)
        {
            var book = context.AllBooks.FirstOrDefault(x => x.BGID == command.BGID);

            if (book == null)
                throw new BookNotFoundException("Nie udało się znaleźć książki!");

            context.DeleteBook(book);
            context.SaveChanges();
        }
    }
}
