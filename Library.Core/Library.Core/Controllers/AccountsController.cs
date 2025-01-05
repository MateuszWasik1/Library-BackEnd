using Microsoft.AspNetCore.Mvc;
using Library.Core.CQRS.Dispatcher;
using Library.Core.CQRS.Resources.Accounts.Queries;
using Library.Core.CQRS.Resources.Accounts.Commands;
using Library.Core.Models.ViewModels.AccountsViewModel;

namespace Library.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public AccountsController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpPost]
        [Route("Register")]
        public void Register(RegisterViewModel model)
            => dispatcher.DispatchCommand(new RegisterUserCommand() { Model = model });

        [HttpGet]
        [Route("Login")]
        public string Login(string username, string password)
            => dispatcher.DispatchQuery<LoginQuery, string>(new LoginQuery() { Username = username, Password = password });
    }
}