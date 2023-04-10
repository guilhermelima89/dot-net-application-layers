using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public abstract class Entity
{
    [Key]
    public int Id { get; set; }
    public string UsuarioCriacao { get; set; }
    public DateTime? DataCriacao { get; set; }
    public string UsuarioAlteracao { get; set; }
    public DateTime? DataAlteracao { get; set; }
}
