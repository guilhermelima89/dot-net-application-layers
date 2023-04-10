using Core.Models;
using FluentValidation;

namespace Core.Validations;

public class ProdutoValidation : AbstractValidator<Produto>
{
    public ProdutoValidation()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithMessage(MessageValidation.CAMPO_OBRIGATORIO)
            .Length(1, 50)
            .WithMessage(MessageValidation.TAMANHO_CAMPO);
    }
}
