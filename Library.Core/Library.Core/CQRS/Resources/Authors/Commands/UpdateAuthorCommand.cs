using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.Models.ViewModels.AuthorsViewModels;

namespace Library.Core.CQRS.Resources.Authors.Commands
{
    public class UpdateAuthorCommand : ICommand
    {
        public AuthorViewModel? Model { get; set; }
    }
}
