using Microsoft.AspNetCore.Mvc;
using Library.Core.CQRS.Dispatcher;
using Library.Core.Models.ViewModels.ReportsViewModels;
using Library.Core.CQRS.Resources.Reports.Commands;
using Library.Core.CQRS.Resources.Reports.Queries;

namespace Library.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public ReportsController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetReports")]
        public ReportsListViewModel GetReports(int skip, int take)
            => dispatcher.DispatchQuery<GetReportsQuery, ReportsListViewModel>(new GetReportsQuery() { Skip = skip, Take = take });

        [HttpPost]
        [Route("AddReport")]
        public void AddReport(ReportViewModel model)
            => dispatcher.DispatchCommand(new AddReportCommand() { Model = model });

        [HttpDelete]
        [Route("DeleteReport/{pgid}")]
        public void Delete(Guid pgid)
             => dispatcher.DispatchCommand(new DeleteReportCommand() { PGID = pgid });
    }
}