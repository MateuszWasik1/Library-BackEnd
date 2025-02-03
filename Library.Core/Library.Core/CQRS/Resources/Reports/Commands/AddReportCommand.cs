using Library.Core.CQRS.Abstraction.Commands;
using Library.Core.Models.ViewModels.ReportsViewModels;

namespace Library.Core.CQRS.Resources.Reports.Commands
{
    public class AddReportCommand : ICommand
    {
        public ReportViewModel? Model { get; set; }
    }
}
