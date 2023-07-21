using System.ComponentModel.DataAnnotations;

namespace viagem_api.Models;

public class Destino
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do destino é obrigatório.")]
    [StringLength(50, ErrorMessage = "O nome do destino deve ter apenas 50 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O preço do destino é obrigatório.")]
    public decimal Preco { get; set; }
}
