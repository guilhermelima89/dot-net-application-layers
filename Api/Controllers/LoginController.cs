using Api.Services;
using Api.ViewModels;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
public class LoginController : MainController
{
    private readonly AuthenticationService _authenticationService;

    public LoginController(AuthenticationService authenticationService, IUserService user, INotificadorService notificador) : base(notificador, user)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(UsuarioLogin usuarioLogin)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _authenticationService.SignInManager.PasswordSignInAsync(usuarioLogin.Usuario, usuarioLogin.Senha, false, true);

        if (result.Succeeded)
        {
            return CustomResponse(await _authenticationService.GerarJwt(usuarioLogin.Usuario));
        }

        if (result.IsLockedOut)
        {
            NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas");
            return CustomResponse();
        }

        NotificarErro("Usuário ou senha incorretos");
        return CustomResponse();
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenViewModel tokenViewModel)
    {
        if (string.IsNullOrEmpty(tokenViewModel.RefreshToken))
        {
            NotificarErro("Refresh Token inválido");
            return CustomResponse();
        }

        var token = await _authenticationService.ObterRefreshToken(Guid.Parse(tokenViewModel.RefreshToken));

        if (token is null)
        {
            NotificarErro("Refresh Token expirado");
            return CustomResponse();
        }

        return CustomResponse(await _authenticationService.GerarJwt(token.Username));
    }
}
