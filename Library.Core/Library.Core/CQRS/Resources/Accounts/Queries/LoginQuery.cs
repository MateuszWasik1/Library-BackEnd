using Library.Core.CQRS.Abstraction.Queries;

namespace Library.Core.CQRS.Resources.Accounts.Queries
{
    public class LoginQuery : IQuery<string>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
