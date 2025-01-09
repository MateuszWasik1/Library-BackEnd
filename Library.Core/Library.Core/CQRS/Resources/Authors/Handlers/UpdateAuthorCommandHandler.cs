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
            if (command.Model.AFirstName.Length < 1)
                throw new AFirstNameMin1CharacterException("Imię autora nie powinno być krótsze niż 1 znak!");

            if (command.Model.AFirstName.Length > 255)
                throw new AFirstNameMax255CharacterException("Imię autora nie powinno być dłuższe niż 255 znaków!");

            if (command.Model?.AMiddleName?.Length > 255)
                throw new AMiddleNameMax255CharacterException("Środkowe imię autora nie powinno być dłuższe niż 255 znaków!");

            if (command.Model.ALastName.Length < 1)
                throw new ALastNameMin1CharacterException("Nazwisko autora nie powinno być krótsze niż 1 znak!");

            if (command.Model.ALastName.Length > 255)
                throw new ALastNameMax255CharacterException("Nazwisko autora nie powinno być dłuższe niż 255 znaków!");

            if (command.Model?.ANickName?.Length > 255)
                throw new ANickNameMax255CharacterException("Przydomek autora nie powinien być dłuższy niż 255 znaków!");

            if (command.Model?.ANationality?.Length > 255)
                throw new ANationalityMax255CharacterException("Narodowość autora nie powinien być dłuższy niż 255 znaków!");

            var author = context.Authors.FirstOrDefault(x => x.AGID == command.Model.AGID);

            if (author == null)
                throw new AuthorNotFoundException("Nie udało się znaleźć autora!");

            author.AFirstName = command.Model.AFirstName;
            author.AMiddleName = command.Model.AMiddleName ?? "";
            author.ALastName = command.Model.ALastName;
            author.ANickName = command.Model.ANickName ?? "";
            author.ANationality = command.Model.ANationality ?? "";

            context.CreateOrUpdate(author);
            context.SaveChanges();
        }
    }
}
