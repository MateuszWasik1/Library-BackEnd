using Library.Core.CQRS.Abstraction.Commands;

namespace Library.Core.CQRS.Resources.Authors.Commands
{
    public class DeleteAuthorCommand : ICommand
    {
        public Guid AGID { get; set; }
    }
}
