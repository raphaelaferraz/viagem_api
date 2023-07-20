using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using viagem_api.Data.Dtos;
using viagem_api.Models;

namespace viagem_api.Controllers;

[ApiController]
[Route("[controller]")]
public class DepoimentoController : ControllerBase
{
    private static List<Depoimento> data = new List<Depoimento>();

    private IMapper _mapper;

    public DepoimentoController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public List<Depoimento> AdicionaDepoimento(CreateDepoimentoDto depoimentoDto)
    {
        
        var depoimento = _mapper.Map<Depoimento>(depoimentoDto);

        data.Add(depoimento);

        return data;
    }

    [HttpGet]
    public List<ReadDepoimentoDto> ListaDepoimentos()
    {
        return _mapper.Map<List<ReadDepoimentoDto>>(data); // Retorna diretamente a lista estática
    }
}
