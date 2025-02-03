using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.CQRS.Resources.Reports.Commands;
using Library.Core.Exceptions.Reports;

namespace Library.Core.CQRS.Resources.Reports.Handlers
{
    public class DeleteReportCommandHandler : ICommandHandler<DeleteReportCommand>
    {
        private readonly IDataBaseContext context;
        public DeleteReportCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(DeleteReportCommand command)
        {
            var Report = context.Reports.FirstOrDefault(x => x.RGID == command.RGID);

            if (Report == null)
                throw new ReportNotFoundException("Nie udało się znaleźć raportu!");

            context.DeleteReport(Report);
            context.SaveChanges();
        }
    }
}
