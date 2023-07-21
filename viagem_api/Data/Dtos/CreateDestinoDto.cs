using System.ComponentModel.DataAnnotations;

namespace viagem_api.Data.Dtos;

public class CreateDestinoDto
{
    [Required(ErrorMessage = "O nome do destino é obrigatório.")]
    [StringLength(50, ErrorMessage = "O nome do destino deve ter apenas 50 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O preço do destino é obrigatório.")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "A foto do destino é obrigatória.")]
    public string UrlFoto { get; set; }
}
