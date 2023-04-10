using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotificadorService _notificador;
    private readonly IUserService _user;
    protected Guid UsuarioId { get; set; }
    protected string UsuarioNome { get; set; }
    protected bool UsuarioAutenticado { get; set; }

    protected MainController(INotificadorService notificador, IUserService user)
    {
        _notificador = notificador;
        _user = user;

        if (_user.IsAuthenticated())
        {
            UsuarioId = _user.GetUserId();
            UsuarioNome = _user.GetUserName();
            UsuarioAutenticado = true;
        }
    }

    protected bool OperacaoValida()
    {
        return !_notificador.TemNotificacao();
    }

    protected IActionResult CustomResponse(object result = null)
    {
        if (OperacaoValida())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
        });
    }

    protected IActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid)
        {
            NotificarErroModelInvalida(modelState);
        }

        return CustomResponse();
    }

    protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);

        foreach (var erro in erros)
        {
            var errorMsg = erro.Exception is null ? erro.ErrorMessage : erro.Exception.Message;
            NotificarErro(errorMsg);
        }
    }

    protected void NotificarErro(string mensagem)
    {
        _notificador.Handle(new Notificacao(mensagem));
    }
}
