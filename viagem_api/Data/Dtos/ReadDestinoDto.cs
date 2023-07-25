using System.ComponentModel.DataAnnotations;

namespace viagem_api.Data.Dtos;

public class ReadDestinoDto
{
    public string Nome { get; set; }

    public decimal Preco { get; set; }

    public string UrlFoto1 { get; set; }

    public string UrlFoto2 { get; set; }

    public string Meta { get; set; }

    public string TextoDescritivo { get; set; }
}
