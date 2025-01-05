using Microsoft.AspNetCore.Mvc;
using Library.Core.CQRS.Dispatcher;
using Library.Core.CQRS.Resources.Accounts.Queries;
using Library.Core.Entities;

namespace Library.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public TestsController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetTestsData")]
        public List<Tests> GetTestsData()
            => dispatcher.DispatchQuery<GetTestDataQuery, List<Tests>>(new GetTestDataQuery());
    }
}