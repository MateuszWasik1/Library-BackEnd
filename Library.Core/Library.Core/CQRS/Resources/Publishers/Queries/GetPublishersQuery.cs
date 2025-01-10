using Library.Core.Models.ViewModels.PublishersViewModels;
using Library.Core.CQRS.Abstraction.Queries;

namespace Library.Core.CQRS.Resources.Publishers.Queries
{
    public class GetPublishersQuery : IQuery<PublishersListViewModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
