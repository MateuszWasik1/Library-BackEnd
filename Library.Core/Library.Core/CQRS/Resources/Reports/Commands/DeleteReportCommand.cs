using Library.Core.CQRS.Abstraction.Commands;

namespace Library.Core.CQRS.Resources.Reports.Commands
{
    public class DeleteReportCommand : ICommand
    {
        public Guid RGID { get; set; }
    }
}
