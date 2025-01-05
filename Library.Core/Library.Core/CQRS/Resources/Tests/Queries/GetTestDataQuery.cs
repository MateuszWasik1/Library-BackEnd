using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.Entities;

namespace Library.Core.CQRS.Resources.Accounts.Queries
{
    public class GetTestDataQuery : IQuery<List<Tests>>
    {
    }
}
