using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels;

public class AlterarSenhaViewModel
{
    [Required(ErrorMessage = "Campo obrigatório")]
    public string Usuario { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Campo obrigatório")]
    public string NovaSenha { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    [DataType(DataType.Password)]
    [Compare("NovaSenha", ErrorMessage = "A nova senha e a senha de confirmação não coincidem.")]
    public string ConfirmaSenha { get; set; }
}

