using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.CQRS.Resources.Books.Commands;
using Library.Core.Exceptions.Books;

namespace Library.Core.CQRS.Resources.Books.Handlers
{
    public class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand>
    {
        private readonly IDataBaseContext context;
        public UpdateBookCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(UpdateBookCommand command)
        {
            if (command.Model.BAuthorGID == Guid.Empty)
                throw new AuthorRequiredException("Dodanie autora jest wymagane!");

            if (command.Model.BPublisherGID == Guid.Empty)
                throw new PublisherRequiredException("Dodanie wydawcy jest wymagane!");

            if (command.Model.BTitle.Length < 3)
                throw new TitleNameMin3CharactersException("Tytuł książki powinien być dłuższy niż 3 znaki!");
                                                            
            if (command.Model.BTitle.Length > 255)            
                throw new TitleNameMax255CharactersException("Tytuł książki powinien być krószy niż 255 znaków!");

            if (command.Model.BISBN.Length != 13)
                throw new ISBNDifferentThan13CharactersException("ISBN powinien posiadać 13 znaków!");

            if (command.Model.BLanguage.Length > 255)
                throw new LanguageMax255CharactersException("Język książki nie powinien przekraczać 255 znaków!");

            if (command.Model.BDescription?.Length > 2000)
                throw new DescriptionMax2000CharactersException("Opis książki nie powinien przekraczać 2000 znaków!");

            var book = context.AllBooks.FirstOrDefault(x => x.BGID == command.Model.BGID);

            if (book == null)
                throw new BookNotFoundException("Nie udało się znaleźć książki!");

            book.BTitle = command.Model.BTitle;
            book.BISBN = command.Model.BISBN;
            book.BGenre = command.Model.BGenre;
            book.BLanguage = command.Model.BLanguage;
            book.BDescription = command.Model.BDescription;

            context.CreateOrUpdate(book);
            context.SaveChanges();
        }
    }
}
