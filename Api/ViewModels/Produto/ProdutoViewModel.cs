using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels;

public class ProdutoViewModel : EntityViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public bool Ativo { get; set; } = true;
}
