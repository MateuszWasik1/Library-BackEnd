using AutoMapper;
using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.CQRS.Resources.Authors.Queries;
using Library.Core.Exceptions.Authors;
using Library.Core.Models.ViewModels.AuthorsViewModels;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.CQRS.Resources.Authors.Handlers
{
    public class GetAuthorQueryHandler : IQueryHandler<GetAuthorQuery, AuthorViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetAuthorQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public AuthorViewModel Handle(GetAuthorQuery query)
        {
            var author = context.AllAuthors.AsNoTracking().FirstOrDefault(x => x.AGID == query.AGID); //ToDo Zrobić ifa który rozdzieli AllAuthors i UserAuthors w zależności od roli.

            if (author == null)
                throw new AuthorNotFoundException("Nie udało się znaleźć książki!");

            var AuthorViewModel = mapper.Map<Library.Core.Entities.Authors, AuthorViewModel>(author);

            return AuthorViewModel;
        }
    }
}
