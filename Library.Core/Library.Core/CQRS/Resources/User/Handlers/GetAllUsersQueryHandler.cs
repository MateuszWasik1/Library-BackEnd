using AutoMapper;
using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.CQRS.Resources.User.Queries;
using Library.Core.Models.ViewModels.UserViewModels;

namespace Library.Core.CQRS.Resources.User.Handlers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, GetUsersAdminViewModel>
    {

        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetAllUsersQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public GetUsersAdminViewModel Handle(GetAllUsersQuery query)
        {
            var usersData = context.AllUsers.ToList();

            var usersAdmViewModel = new List<UsersAdminViewModel>();

            var count = usersData.Count;
            usersData = usersData.Skip(query.Skip).Take(query.Take).ToList();

            usersData.ForEach(x => {
                var model = mapper.Map<Entities.User, UsersAdminViewModel>(x);
                usersAdmViewModel.Add(model);
            });

            var model = new GetUsersAdminViewModel()
            {
                List = usersAdmViewModel,
                Count = count,
            };

            return model;
        }
    }
}
