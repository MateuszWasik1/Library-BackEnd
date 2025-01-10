using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.CQRS.Resources.Publishers.Commands;
using Library.Core.Exceptions.Books;
using Library.Core.Exceptions.Publishers;

namespace Library.Core.CQRS.Resources.Publishers.Handlers
{
    public class UpdatePublisherCommandHandler : ICommandHandler<UpdatePublisherCommand>
    {
        private readonly IDataBaseContext context;
        public UpdatePublisherCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(UpdatePublisherCommand command)
        {
            if (command.Model.PName.Length < 3)
                throw new AuthorRequiredException("Nazwa wydawnictwa powinna być dłuższa niż 3 znaki!");

            if (command.Model.PName.Length > 255)
                throw new PublisherRequiredException("Nazwa wydawnictwa nie powinien przekraczać 255 znaków!");

            if (command.Model.PCountry.Length > 255)
                throw new TitleNameMax255CharactersException("Kraj wydawnictwa powinien być krószy niż 255 znaków!");

            if (command.Model.PCity.Length > 255)
                throw new LanguageMax255CharactersException("Miasto wydawnictwa nie powinien przekraczać 255 znaków!");

            if (command.Model.PEmail.Length > 255)
                throw new LanguageMax255CharactersException("Email wydawnictwa nie powinien przekraczać 255 znaków!");

            if (command.Model.PPhone.Length > 255)
                throw new LanguageMax255CharactersException("Telefon wydawnictwa nie powinien przekraczać 255 znaków!");

            var publisher = context.Publishers.FirstOrDefault(x => x.PGID == command.Model.PGID);

            if (publisher == null)
                throw new PublisherNotFoundException("Nie udało się znaleźć książki!");

            publisher.PName = command.Model.PName;
            publisher.PCountry = command.Model.PCountry;
            publisher.PCity = command.Model.PCity;
            publisher.PEmail = command.Model.PEmail;
            publisher.PPhone = command.Model.PPhone;

            context.CreateOrUpdate(publisher);
            context.SaveChanges();
        }
    }
}
