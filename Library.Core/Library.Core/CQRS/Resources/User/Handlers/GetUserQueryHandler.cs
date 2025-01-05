using AutoMapper;
using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.CQRS.Resources.User.Queries;
using Library.Core.Exceptions;
using Library.Core.Models.ViewModels.UserViewModels;
using Library.Core.Services;

namespace Library.Core.CQRS.Resources.User.Handlers
{
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        private readonly IMapper mapper;
        public GetUserQueryHandler(IDataBaseContext context, IUserContext user, IMapper mapper)
        {
            this.context = context;
            this.user = user;
            this.mapper = mapper;
        }

        public UserViewModel Handle(GetUserQuery query)
        {
            var userData = context.User.FirstOrDefault(x => x.UID == user.UID);

            if (userData == null)
                throw new UserNotFoundExceptions("Nie znaleziono użytkownika!");

            var model = mapper.Map<Entities.User, UserViewModel>(userData);

            return model;
        }
    }
}
