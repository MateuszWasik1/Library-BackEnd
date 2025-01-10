using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.Models.ViewModels.PublishersViewModels;

namespace Library.Core.CQRS.Resources.Publishers.Commands
{
    public class AddPublisherCommand : ICommand
    {
        public PublisherViewModel? Model { get; set; }
    }
}
