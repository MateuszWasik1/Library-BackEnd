using Library.Core.Models.ViewModels.BooksViewModels;
using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.Models.Enums;

namespace Library.Core.CQRS.Resources.Books.Queries
{
    public class GetBooksQuery : IQuery<BooksListViewModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public GenreEnum Genre { get; set; }
        public Guid AGID { get; set; }
    }
}
