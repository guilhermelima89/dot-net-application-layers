using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Core.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
    public string FullName { get; set; }
}
