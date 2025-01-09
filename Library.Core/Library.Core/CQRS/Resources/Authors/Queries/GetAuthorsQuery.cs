using Library.Core.Models.ViewModels.AuthorsViewModels;
using Library.Core.CQRS.Abstraction.Queries;

namespace Library.Core.CQRS.Resources.Authors.Queries
{
    public class GetAuthorsQuery : IQuery<AuthorsListViewModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
