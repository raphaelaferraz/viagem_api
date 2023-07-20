using System.ComponentModel.DataAnnotations;

namespace viagem_api.Data.Dtos;

public class ReadDepoimentoDto
{
    public string UrlFoto { get; }
    public string NomeUsuario { get; }
    public string DepoimentoUsuario { get; }

    public ReadDepoimentoDto(string urlFoto, string nomeUsuario, string depoimentoUsuario)
    {
        UrlFoto = urlFoto;
        NomeUsuario = nomeUsuario;
        DepoimentoUsuario = depoimentoUsuario;
    }
}
