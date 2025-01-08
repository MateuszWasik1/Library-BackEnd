using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.CQRS.Resources.User.Commands;
using Library.Core.Exceptions;
using Library.Core.Exceptions.Tasks;

namespace Library.Core.CQRS.Resources.User.Handlers
{
    public class SaveUserByAdminCommandHandler : ICommandHandler<SaveUserByAdminCommand>
    {
        private readonly IDataBaseContext context;
        public SaveUserByAdminCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(SaveUserByAdminCommand command)
        {
            if (command.Model.UUserName.Length == 0)
                throw new UserNameRequiredException("Nazwa użytkownika nie może być pusta!");

            if (command.Model.UUserName.Length > 100)
                throw new UserNameMax100Exception("Nazwa użytkownika nie może mieć więcej niż 100 znaków!");

            if (command.Model.UFirstName.Length > 50)
                throw new UserFirstNameMax50Exception("Imię użytkownika nie może mieć więcej niż 50 znaków!");

            if (command.Model.ULastName.Length > 50)
                throw new UserLastNameMax50Exception("Nazwisko użytkownika nie może mieć więcej niż 50 znaków!");

            if (command.Model.UEmail.Length == 0)
                throw new UserEmailRequiredException("Email użytkownika nie może być pusty!");

            if (command.Model.UEmail.Length > 100)
                throw new UserEmailMax100Exception("Email użytkownika nie może mieć więcej niż 100 znaków!");

            if (command.Model.UPhone.Length > 100)
                throw new UserPhoneMax100Exception("Telefon użytkownika nie może mieć więcej niż 100 znaków!");

            var userData = context.AllUsers.FirstOrDefault(x => x.UGID == command.Model.UGID);

            if (userData == null)
                throw new UserNotFoundExceptions("Nie znaleziono użytkownika!");

            userData.URID = command.Model.URID;
            userData.UFirstName = command.Model.UFirstName;
            userData.ULastName = command.Model.ULastName;
            userData.UUserName = command.Model.UUserName;
            userData.UEmail = command.Model.UEmail;
            userData.UPhone = command.Model.UPhone;

            context.CreateOrUpdate(userData);
            context.SaveChanges();
        }
    }
}
