using Library.Core.Models.ViewModels.UserViewModels;
using Library.Core.CQRS.Abstraction.Commands;

namespace Library.Core.CQRS.Resources.User.Commands
{
    public class SaveUserByAdminCommand : ICommand
    {
        public UserAdminViewModel? Model { get; set; }
    }
}
