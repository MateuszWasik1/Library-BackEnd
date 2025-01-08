using AutoMapper;
using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.CQRS.Resources.Books.Queries;
using Library.Core.Models.ViewModels.BooksViewModels;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.CQRS.Resources.Books.Handlers
{
    public class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, BooksListViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetBooksQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public BooksListViewModel Handle(GetBooksQuery query)
        {
            var books = context.AllBooks.AsNoTracking().ToList(); //ToDo rozdzielić w zależności od roli
            var booksViewModel = new List<BooksViewModel>();

            var count = books.Count;
            books = books.Skip(query.Skip).Take(query.Take).ToList();

            books.ForEach(x =>
            {
                var bVM = mapper.Map<Library.Core.Entities.Books, BooksViewModel>(x);

                booksViewModel.Add(bVM);
            });

            var model = new BooksListViewModel()
            {
                List = booksViewModel,
                Count = count
            };

            return model;
        }
    }
}
