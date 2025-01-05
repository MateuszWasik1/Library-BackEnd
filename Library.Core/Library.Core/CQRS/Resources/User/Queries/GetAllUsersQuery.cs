using Library.Core.Models.ViewModels.UserViewModels;
using Library.Core.CQRS.Abstraction.Queries;

namespace Library.Core.CQRS.Resources.User.Queries
{
    public class GetAllUsersQuery : IQuery<GetUsersAdminViewModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
