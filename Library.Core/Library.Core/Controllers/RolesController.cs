using Library.Core.CQRS.Dispatcher;
using Library.Core.CQRS.Resources.Roles.Queries;
using Library.Core.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public RolesController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetUserRoles")]
        public RolesViewModel GetUserRoles()
            => dispatcher.DispatchQuery<GetUserRolesQuery, RolesViewModel>(new GetUserRolesQuery());

        [HttpGet]
        [Route("GetIsUserAdmin")]
        public bool GetIsUserAdmin() 
            => dispatcher.DispatchQuery<GetIsUserAdminQuery, bool>(new GetIsUserAdminQuery());

        [HttpGet]
        [Route("GetIsUserSupport")]
        public bool GetIsUserSupport() 
            => dispatcher.DispatchQuery<GetIsUserSupportQuery, bool>(new GetIsUserSupportQuery());

        [HttpGet]
        [Route("GetIsUserPremium")]
        public bool GetIsUserPremium()
            => dispatcher.DispatchQuery<GetIsUserPremiumQuery, bool>(new GetIsUserPremiumQuery());
    }
}
