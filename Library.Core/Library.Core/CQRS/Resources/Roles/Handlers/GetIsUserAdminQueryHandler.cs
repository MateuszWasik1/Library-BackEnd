using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.CQRS.Resources.Roles.Queries;
using Library.Core.Models.Enums;
using Library.Core.Services;

namespace Library.Core.CQRS.Resources.Roles.Handlers
{
    public class GetIsUserAdminQueryHandler : IQueryHandler<GetIsUserAdminQuery, bool>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        public GetIsUserAdminQueryHandler(IDataBaseContext context, IUserContext user)
        {
            this.context = context;
            this.user = user;
        }

        public bool Handle(GetIsUserAdminQuery query) => context.User.FirstOrDefault(x => x.UID == user.UID)?.URID == (int) RoleEnum.Admin;
    }
}
