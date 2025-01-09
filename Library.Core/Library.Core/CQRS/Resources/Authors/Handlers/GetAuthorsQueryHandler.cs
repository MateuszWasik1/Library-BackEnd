using AutoMapper;
using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.CQRS.Resources.Authors.Queries;
using Library.Core.Models.ViewModels.AuthorsViewModels;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.CQRS.Resources.Authors.Handlers
{
    public class GetAuthorsQueryHandler : IQueryHandler<GetAuthorsQuery, AuthorsListViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetAuthorsQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public AuthorsListViewModel Handle(GetAuthorsQuery query)
        {
            var authors = context.AllAuthors.AsNoTracking().ToList(); //ToDo rozdzielić w zależności od roli
            var authorsViewModel = new List<AuthorsViewModel>();

            var count = authors.Count;
            authors = authors.Skip(query.Skip).Take(query.Take).ToList();

            authors.ForEach(x =>
            {
                var aVM = mapper.Map<Library.Core.Entities.Authors, AuthorsViewModel>(x);

                AuthorsViewModel.Add(bVM);
            });

            var model = new AuthorsListViewModel()
            {
                List = AuthorsViewModel,
                Count = count
            };

            return model;
        }
    }
}
