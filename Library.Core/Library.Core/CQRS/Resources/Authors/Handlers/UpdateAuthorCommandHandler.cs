using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.CQRS.Resources.Authors.Commands;
using Library.Core.Exceptions.Authors;

namespace Library.Core.CQRS.Resources.Authors.Handlers
{
    public class UpdateAuthorCommandHandler : ICommandHandler<UpdateAuthorCommand>
    {
        private readonly IDataBaseContext context;
        public UpdateAuthorCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(UpdateAuthorCommand command)
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

            var author = context.AllAuthors.FirstOrDefault(x => x.BGID == command.Model.BGID);

            if (author == null)
                throw new AuthorNotFoundException("Nie udało się znaleźć książki!");

            author.BTitle = command.Model.BTitle;
            author.BISBN = command.Model.BISBN;
            author.BGenre = command.Model.BGenre;
            author.BLanguage = command.Model.BLanguage;
            author.BDescription = command.Model.BDescription;

            context.CreateOrUpdate(author);
            context.SaveChanges();
        }
    }
}
