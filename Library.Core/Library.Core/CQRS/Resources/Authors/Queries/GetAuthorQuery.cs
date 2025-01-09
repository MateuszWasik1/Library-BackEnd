using Library.Core.Models.ViewModels.AuthorsViewModels;
using Library.Core.CQRS.Abstraction.Queries;

namespace Library.Core.CQRS.Resources.Authors.Queries
{
    public class GetAuthorQuery : IQuery<AuthorViewModel>
    {
        public Guid AGID { get; set; }
    }
}
