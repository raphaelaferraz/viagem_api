using System.ComponentModel.DataAnnotations;

namespace viagem_api.Data.Dtos;

public class ReadDestinoDto
{
    public string Nome { get; set; }

    public decimal Preco { get; set; }

    public string UrlFoto { get; set; }
}
