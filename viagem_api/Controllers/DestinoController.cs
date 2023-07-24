using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using viagem_api.Data;
using viagem_api.Data.Dtos;
using viagem_api.Migrations;
using viagem_api.Models;

namespace viagem_api.Controllers;

[ApiController]
[Route("[controller]")]
public class DestinoController : ControllerBase
{
    private ViagemContext _context;
    private IMapper _mapper;

    public DestinoController(ViagemContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaDestino(CreateDestinoDto destinoDto)
    {
        var destino = _mapper.Map<viagem_api.Models.Destino>(destinoDto);

        _context.Destino.Add(destino);

        _context.SaveChanges();

        return CreatedAtAction(nameof(ListaDestinos), new { id = destino.Id }, destino);
    }

    [HttpGet]
    public IActionResult ListaDestinos()
    {
        List<viagem_api.Models.Destino> destinos = _context.Destino.ToList();

        if (destinos.Count == 0) return NotFound();

        List<ReadDestinoDto> destinoDto = _mapper.Map<List<ReadDestinoDto>>(destinos);

        return Ok(destinoDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaDestino(int id, [FromBody] UpdateDestinoDto destinoDto)
    {
        var destino = _context.Depoimento.FirstOrDefault(destino => destino.Id == id);

        if (destino == null) return NotFound();

        _mapper.Map(destinoDto, destino);

        _context.SaveChanges();

        return NoContent();
    }

}
