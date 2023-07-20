using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using viagem_api.Data.Dtos;
using viagem_api.Models;

namespace viagem_api.Controllers;

[ApiController]
[Route("[controller]")]
public class DepoimentoController : ControllerBase
{
    private List<dynamic> _data;
    private IMapper _mapper;

    public DepoimentoController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public List<dynamic> AdicionaDepoimento(CreateDepoimentoDto depoimentoDto)
    {
        var depoimento = _mapper.Map<Depoimento>(depoimentoDto);

        _data.Add(depoimento);

        return _data;
    }
}
