using System.Security.Claims;
using Core.Interfaces;

namespace Api.Extensions;

public class AspNetUser : IUserService
{
    private readonly IHttpContextAccessor _accessor;

    public AspNetUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string Name => _accessor.HttpContext.User.Identity.Name;

    public Guid GetUserId()
    {
        return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
    }

    public string GetUserEmail()
    {
        return IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";
    }

    public string GetUserName()
    {
        return IsAuthenticated() ? _accessor.HttpContext.User.GetUserName() : "";
    }

    public string GetFullName()
    {
        return IsAuthenticated() ? _accessor.HttpContext.User.GetFullName() : "";
    }

    public bool IsAuthenticated()
    {
        return _accessor.HttpContext.User.Identity.IsAuthenticated;
    }

    public bool IsInRole(string role)
    {
        return _accessor.HttpContext.User.IsInRole(role);
    }

    public IEnumerable<Claim> GetClaimsIdentity()
    {
        return _accessor.HttpContext.User.Claims;
    }
}

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal principal)
    {
        if (principal is null) throw new ArgumentException(null, nameof(principal));

        var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
        return claim?.Value;
    }

    public static string GetUserEmail(this ClaimsPrincipal principal)
    {
        if (principal is null) throw new ArgumentException(null, nameof(principal));

        var claim = principal.FindFirst(ClaimTypes.Email);
        return claim?.Value;
    }

    public static string GetUserName(this ClaimsPrincipal principal)
    {
        if (principal is null) throw new ArgumentException(null, nameof(principal));
        var claim = principal.FindFirst("username");
        return claim?.Value;
    }

    public static string GetFullName(this ClaimsPrincipal principal)
    {
        if (principal is null) throw new ArgumentException(null, nameof(principal));

        var claim = principal.FindFirst("fullname");
        return claim?.Value;
    }
}
