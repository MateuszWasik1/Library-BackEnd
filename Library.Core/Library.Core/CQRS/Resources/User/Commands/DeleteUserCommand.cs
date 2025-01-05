using Library.Core.CQRS.Abstraction.Commands;

namespace Library.Core.CQRS.Resources.User.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public Guid UGID { get; set; }
    }
}
