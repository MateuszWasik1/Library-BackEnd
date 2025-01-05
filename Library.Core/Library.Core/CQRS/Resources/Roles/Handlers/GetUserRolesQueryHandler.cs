using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.CQRS.Resources.Roles.Queries;
using Library.Core.Models.Enums;
using Library.Core.Models.ViewModels;
using Library.Core.Services;

namespace Library.Core.CQRS.Resources.Roles.Handlers
{
    public class GetUserRolesQueryHandler : IQueryHandler<GetUserRolesQuery, RolesViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        public GetUserRolesQueryHandler(IDataBaseContext context, IUserContext user)
        {
            this.context = context;
            this.user = user;
        }

        public RolesViewModel Handle(GetUserRolesQuery query) 
        {
            var userRole = context.User.FirstOrDefault(x => x.UID == user.UID)?.URID ?? (int) RoleEnum.User;

            var model = new RolesViewModel()
            {
                IsAdmin = userRole == (int) RoleEnum.Admin,
                IsPremium = userRole == (int) RoleEnum.Premium,
                IsSupport = userRole == (int) RoleEnum.Admin || userRole == (int) RoleEnum.Support,
                IsUser = userRole == (int) RoleEnum.Admin || userRole == (int) RoleEnum.Support || userRole == (int) RoleEnum.User,
            };

            return model;
        }
    }
}
