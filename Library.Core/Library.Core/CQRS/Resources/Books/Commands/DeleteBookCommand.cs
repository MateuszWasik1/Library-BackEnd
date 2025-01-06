using Library.Core.CQRS.Abstraction.Commands;

namespace Library.Core.CQRS.Resources.Books.Commands
{
    public class DeleteBookCommand : ICommand
    {
        public Guid BGID { get; set; }
    }
}
