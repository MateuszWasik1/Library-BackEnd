using Library.Core.Models.ViewModels.AccountsViewModel;
using Library.Core.CQRS.Abstraction.Commands;

namespace Library.Core.CQRS.Resources.Accounts.Commands
{
    public class RegisterUserCommand : ICommand
    {
        public RegisterViewModel? Model { get; set; }
    }
}
