using Library.Core.Models.ViewModels.PublishersViewModels;
using Library.Core.CQRS.Abstraction.Queries;

namespace Library.Core.CQRS.Resources.Publishers.Queries
{
    public class GetPublisherQuery : IQuery<PublisherViewModel>
    {
        public Guid PGID { get; set; }
    }
}
