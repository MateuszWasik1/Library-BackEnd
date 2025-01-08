using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.Models.ViewModels.BooksViewModels;

namespace Library.Core.CQRS.Resources.Books.Commands
{
    public class UpdateBookCommand : ICommand
    {
        public BookViewModel? Model { get; set; }
    }
}
