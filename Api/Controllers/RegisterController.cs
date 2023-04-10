using Api.Services;
using Api.ViewModels;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class RegisterController : MainController
{
    private readonly AuthenticationService _authenticationService;

    public RegisterController(INotificadorService notificador, IUserService appUser, AuthenticationService authenticationService) : base(notificador, appUser)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(UsuarioRegistro usuarioRegistro)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var user = new ApplicationUser
        {
            FullName = usuarioRegistro.Nome.ToUpper(),
            UserName = usuarioRegistro.Usuario,
            Email = usuarioRegistro.Email,
            EmailConfirmed = true
        };

        var result = await _authenticationService.UserManager.CreateAsync(user, usuarioRegistro.Senha);

        if (result.Succeeded)
        {
            await _authenticationService.UserManager.AddToRoleAsync(user, "NORMAL");
            return CustomResponse();
        }

        foreach (var error in result.Errors)
        {
            NotificarErro(error.Description);
        }

        return CustomResponse();
    }

    [HttpPost("recuperar-usuario")]
    public async Task<IActionResult> Recovery(AlterarSenhaViewModel alterarSenhaUsuario)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var user = await _authenticationService.UserManager.FindByNameAsync(alterarSenhaUsuario.Usuario);

        if (user is null) return NotFound();

        var novaSenha = _authenticationService.UserManager.PasswordHasher.HashPassword(user, alterarSenhaUsuario.NovaSenha);

        user.PasswordHash = novaSenha;

        var result = await _authenticationService.UserManager.UpdateAsync(user);

        if (result.Succeeded) return CustomResponse();

        foreach (var error in result.Errors)
        {
            NotificarErro(error.Description);
        }

        return CustomResponse();
    }

    [HttpDelete("{userName}")]
    public async Task<IActionResult> Delete(string userName)
    {
        var user = await _authenticationService.UserManager.FindByNameAsync(userName);

        if (user is null) return NotFound();

        await _authenticationService.UserManager.DeleteAsync(user);

        return CustomResponse();
    }
}
