using System.Security.Claims;

namespace Library.Core.Services
{
    public interface IUserContext
    {
        ClaimsPrincipal? User { get; }
        int UID { get; }
        string? UGID { get; }
    }
}
