using AutoMapper;
using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.CQRS.Resources.Publishers.Queries;
using Library.Core.Models.ViewModels.PublishersViewModels;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.CQRS.Resources.Publishers.Handlers
{
    public class GetPublishersQueryHandler : IQueryHandler<GetPublishersQuery, PublishersListViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetPublishersQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public PublishersListViewModel Handle(GetPublishersQuery query)
        {
            var publishers = context.Publishers.AsNoTracking().ToList();
            var publishersViewModel = new List<PublishersViewModel>();

            var count = publishers.Count;
            publishers = publishers.Skip(query.Skip).Take(query.Take).ToList();

            publishers.ForEach(x =>
            {
                var pVM = mapper.Map<Library.Core.Entities.Publishers, PublishersViewModel>(x);

                publishersViewModel.Add(pVM);
            });

            var model = new PublishersListViewModel()
            {
                List = publishersViewModel,
                Count = count
            };

            return model;
        }
    }
}
