using AutoMapper;
using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.CQRS.Resources.Books.Queries;
using Library.Core.Models.Enums;
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
            var books = new List<Entities.Books>();

            if (query.Genre == GenreEnum.All)
                books = context.AllBooks.AsNoTracking().ToList(); //ToDo rozdzielić w zależności od roli
            else
                books = context.AllBooks.Where(x => x.BGenre == query.Genre).AsNoTracking().ToList();

            if(query.AGID != Guid.Empty)
                books = books.Where(x => x.BAuthorGID == query.AGID).ToList();

            if (query.PGID != Guid.Empty)
                books = books.Where(x => x.BPublisherGID == query.PGID).ToList();

            var booksViewModel = new List<BooksViewModel>();

            var count = books.Count;
            books = books.Skip(query.Skip).Take(query.Take).ToList();

            books.ForEach(x =>
            {
                var bVM = mapper.Map<Entities.Books, BooksViewModel>(x);

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
