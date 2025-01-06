using Library.Core.Models.ViewModels.BooksViewModels;
using Library.Core.CQRS.Abstraction.Queries;

namespace Library.Core.CQRS.Resources.Books.Queries
{
    public class GetBooksQuery : IQuery<BooksViewModel>
    {
        public string? CGID { get; set; }
        public int Status { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
