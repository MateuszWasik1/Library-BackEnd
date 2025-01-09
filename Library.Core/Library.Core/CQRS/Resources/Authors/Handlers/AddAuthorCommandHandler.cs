using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.CQRS.Resources.Authors.Commands;
using Library.Core.Exceptions.Authors;
using Library.Core.Services;

namespace Library.Core.CQRS.Resources.Authors.Handlers
{
    public class AddAuthorCommandHandler : ICommandHandler<AddAuthorCommand>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        public AddAuthorCommandHandler(IDataBaseContext context, IUserContext user)
        {
            this.context = context;
            this.user = user;
        }

        public void Handle(AddAuthorCommand command)
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

            var author = new Entities.Authors()
            {
                BGID = Guid.NewGuid(),
                BAuthorGID = command.Model.BAuthorGID,
                BPublisherGID = command.Model.BPublisherGID,
                BUID = user.UID,
                BTitle = command.Model.BTitle,
                BISBN = command.Model.BISBN,
                BGenre = command.Model.BGenre,
                BLanguage = command.Model.BLanguage,
                BDescription = command.Model.BDescription,
            };

            context.CreateOrUpdate(author);
            context.SaveChanges();
        }
    }
}
