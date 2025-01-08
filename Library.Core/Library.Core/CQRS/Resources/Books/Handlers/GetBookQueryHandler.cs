using AutoMapper;
using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.CQRS.Resources.Books.Queries;
using Library.Core.Exceptions.Books;
using Library.Core.Models.ViewModels.BooksViewModels;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.CQRS.Resources.Books.Handlers
{
    public class GetBookQueryHandler : IQueryHandler<GetBookQuery, BookViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetBookQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public BookViewModel Handle(GetBookQuery query)
        {
            var book = context.AllBooks.AsNoTracking().FirstOrDefault(x => x.BGID == query.BGID); //ToDo Zrobić ifa który rozdzieli AllBooks i UserBooks w zależności od roli.

            if (book == null)
                throw new BookNotFoundException("Nie udało się znaleźć książki!");

            var bookViewModel = mapper.Map<Library.Core.Entities.Books, BookViewModel>(book);

            return bookViewModel;
        }
    }
}
