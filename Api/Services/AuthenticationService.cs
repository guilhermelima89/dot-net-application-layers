using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Extensions;
using Api.ViewModels;
using Core.Models;
using Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class AuthenticationService
{
    public readonly SignInManager<ApplicationUser> SignInManager;
    public readonly UserManager<ApplicationUser> UserManager;
    public readonly RoleManager<Role> RoleManager;
    private readonly ApplicationDbContext _context;

    public AuthenticationService(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        RoleManager<Role> roleManager,
        ApplicationDbContext context)
    {
        SignInManager = signInManager;
        UserManager = userManager;
        RoleManager = roleManager;
        _context = context;
    }

    public async Task<UsuarioRespostaLogin> GerarJwt(string name)
    {
        var user = await UserManager.FindByNameAsync(name);
        var claims = await UserManager.GetClaimsAsync(user);

        var identityClaims = await ObterClaimsUsuario(claims, user);
        var encodedToken = CodificarToken(identityClaims);
        var refreshToken = await GerarRefreshToken(name);

        return ObterRespostaToken(encodedToken, user, refreshToken);
    }

    public async Task<RefreshToken> ObterRefreshToken(Guid refreshToken)
    {
        var token = await _context.RefreshToken.AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(u => u.Token == refreshToken);

        return token != null && token.ExpirationDate.ToLocalTime() > DateTime.Now
            ? token
            : null;
    }

    private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, ApplicationUser user)
    {
        var userRoles = await UserManager.GetRolesAsync(user);

        claims.Add(new Claim("fullname", user.FullName));
        claims.Add(new Claim("username", user.UserName));
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim("role", userRole));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        return identityClaims;
    }

    private static string CodificarToken(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = AppSettings.Issuer,
            Audience = AppSettings.Audience,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(AppSettings.Expires),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandler.WriteToken(token);
    }

    private static UsuarioRespostaLogin ObterRespostaToken(string encodedToken, ApplicationUser user, RefreshToken refreshToken)
    {
        return new UsuarioRespostaLogin
        {
            AccessToken = encodedToken,
            RefreshToken = refreshToken.Token,
            ExpiresIn = TimeSpan.FromHours(AppSettings.Expires).TotalSeconds,
            User = new UsuarioToken
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                Name = user.UserName,
            }
        };
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    private async Task<RefreshToken> GerarRefreshToken(string email)
    {
        var refreshToken = new RefreshToken
        {
            Username = email,
            ExpirationDate = DateTime.UtcNow.AddHours(AppSettings.RefreshTokenExpiration)
        };

        _context.RefreshToken.RemoveRange(_context.RefreshToken.Where(u => u.Username == email));
        await _context.RefreshToken.AddAsync(refreshToken);

        await _context.SaveChangesAsync();

        return refreshToken;
    }
}
