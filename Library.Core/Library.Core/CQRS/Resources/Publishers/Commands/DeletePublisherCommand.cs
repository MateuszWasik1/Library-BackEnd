using Library.Core.CQRS.Abstraction.Commands;

namespace Library.Core.CQRS.Resources.Publishers.Commands
{
    public class DeletePublisherCommand : ICommand
    {
        public Guid PGID { get; set; }
    }
}
