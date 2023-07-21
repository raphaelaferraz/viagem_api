using System.ComponentModel.DataAnnotations;

namespace viagem_api.Data.Dtos;

public class ReadDepoimentoDto
{
    public string UrlFoto { get; set; }
    public string NomeUsuario { get; set; }
    public string DepoimentoUsuario { get; set; }
    public ReadDepoimentoDto()
    {
        
    }
    public ReadDepoimentoDto(string urlFoto, string nomeUsuario, string depoimentoUsuario)
    {
        UrlFoto = urlFoto;
        NomeUsuario = nomeUsuario;
        DepoimentoUsuario = depoimentoUsuario;
    }
}
