using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.CQRS.Resources.Publishers.Commands;
using Library.Core.Exceptions.Publishers;

namespace Library.Core.CQRS.Resources.Publishers.Handlers
{
    public class UpdatePublisherCommandHandler : ICommandHandler<UpdatePublisherCommand>
    {
        private readonly IDataBaseContext context;
        public UpdatePublisherCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(UpdatePublisherCommand command)
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

            var publisher = context.AllPublishers.FirstOrDefault(x => x.BGID == command.Model.BGID);

            if (publisher == null)
                throw new PublisherNotFoundException("Nie udało się znaleźć książki!");

            publisher.BTitle = command.Model.BTitle;
            publisher.BISBN = command.Model.BISBN;
            publisher.BGenre = command.Model.BGenre;
            publisher.BLanguage = command.Model.BLanguage;
            publisher.BDescription = command.Model.BDescription;

            context.CreateOrUpdate(publisher);
            context.SaveChanges();
        }
    }
}
