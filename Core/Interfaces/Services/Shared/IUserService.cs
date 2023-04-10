using System.Security.Claims;

namespace Core.Interfaces;

public interface IUserService
{
    string Name { get; }
    Guid GetUserId();
    string GetUserEmail();
    string GetUserName();
    string GetFullName();
    bool IsAuthenticated();
    bool IsInRole(string role);
    IEnumerable<Claim> GetClaimsIdentity();
}
