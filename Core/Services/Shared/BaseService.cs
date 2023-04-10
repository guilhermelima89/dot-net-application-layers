using Core.Interfaces;
using Core.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Core.Services;

public abstract class BaseService
{
    private readonly INotificadorService _notificador;

    protected BaseService(INotificadorService notificador)
    {
        _notificador = notificador;
    }

    protected void Notificar(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notificar(error.ErrorMessage);
        }
    }

    protected void Notificar(string mensagem)
    {
        _notificador.Handle(new Notificacao(mensagem));
    }

    protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
    {
        var validator = validacao.Validate(entidade);

        if (validator.IsValid)
        {
            return true;
        }

        Notificar(validator);

        return false;
    }
}
