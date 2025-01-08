using Library.Core.Models.ViewModels.BooksViewModels;
using Library.Core.CQRS.Abstraction.Queries;

namespace Library.Core.CQRS.Resources.Books.Queries
{
    public class GetBookQuery : IQuery<BookViewModel>
    {
        public Guid BGID { get; set; }
    }
}
