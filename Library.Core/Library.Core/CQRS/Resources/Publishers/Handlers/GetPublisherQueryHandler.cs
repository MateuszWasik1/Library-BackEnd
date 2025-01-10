using AutoMapper;
using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.CQRS.Resources.Publishers.Queries;
using Library.Core.Exceptions.Publishers;
using Library.Core.Models.ViewModels.PublishersViewModels;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.CQRS.Resources.Publishers.Handlers
{
    public class GetPublisherQueryHandler : IQueryHandler<GetPublisherQuery, PublisherViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetPublisherQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public PublisherViewModel Handle(GetPublisherQuery query)
        {
            var publisher = context.Publishers.AsNoTracking().FirstOrDefault(x => x.PGID == query.PGID);

            if (publisher == null)
                throw new PublisherNotFoundException("Nie udało się znaleźć książki!");

            var publisherViewModel = mapper.Map<Library.Core.Entities.Publishers, PublisherViewModel>(publisher);

            return publisherViewModel;
        }
    }
}
