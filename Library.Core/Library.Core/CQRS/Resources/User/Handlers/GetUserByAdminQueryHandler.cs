using AutoMapper;
using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.CQRS.Resources.User.Queries;
using Library.Core.Exceptions;
using Library.Core.Models.ViewModels.UserViewModels;

namespace Library.Core.CQRS.Resources.User.Handlers
{
    public class GetUserByAdminQueryHandler : IQueryHandler<GetUserByAdminQuery, UserAdminViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetUserByAdminQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public UserAdminViewModel Handle(GetUserByAdminQuery query)
        {
            var userData = context.AllUsers.FirstOrDefault(x => x.UGID == query.UGID);

            if (userData == null)
                throw new UserNotFoundExceptions("Nie znaleziono użytkownika!");

            var model = mapper.Map<Entities.User, UserAdminViewModel>(userData);

            return model;
        }
    }
}