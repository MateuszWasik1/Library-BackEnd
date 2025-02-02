using Library.Core.Models.ViewModels.ReportsViewModels;
using Library.Core.CQRS.Abstraction.Queries;

namespace Library.Core.CQRS.Resources.Reports.Queries
{
    public class GetReportsQuery : IQuery<ReportsListViewModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
