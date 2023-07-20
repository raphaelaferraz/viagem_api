using System.ComponentModel.DataAnnotations;

namespace viagem_api.Data.Dtos;

public class CreateDepoimentoDto
{
    [Required(ErrorMessage = "A foto é obrigatória.")]
    public string UrlFoto { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(50, ErrorMessage = "O nome deve ter apenas 50 caracteres.")]
    public string NomeUsuario { get; set; }

    [Required(ErrorMessage = "O depoimento é obrigatório.")]
    [StringLength(255, ErrorMessage = "O depoimento deve ter apenas 250 caracteres.")]
    public string DepoimentoUsuario { get; set; }
}
