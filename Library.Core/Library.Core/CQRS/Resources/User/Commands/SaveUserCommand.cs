using Library.Core.Models.ViewModels.UserViewModels;
using Library.Core.CQRS.Abstraction.Commands;

namespace Library.Core.CQRS.Resources.User.Commands
{
    public class SaveUserCommand : ICommand
    {
        public UserViewModel? Model { get; set; }
    }
}
