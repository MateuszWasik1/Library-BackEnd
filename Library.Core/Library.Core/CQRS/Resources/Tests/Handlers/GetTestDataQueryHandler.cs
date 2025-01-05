using Library.Core.Context;
using Library.Core.CQRS.Abstraction.Queries;
using Library.Core.CQRS.Resources.Accounts.Queries;
using Library.Core.Entities;

namespace Library.Core.CQRS.Resources.Accounts.Handlers
{
    public class GetTestDataQueryHandler : IQueryHandler<GetTestDataQuery, List<Tests>>
    {
        private readonly IDataBaseContext context;
        public GetTestDataQueryHandler(IDataBaseContext context) => this.context = context;

        public List<Tests> Handle(GetTestDataQuery query) 
        {
            var result = context.Tests.ToList();

            return result;
        }
    }
}