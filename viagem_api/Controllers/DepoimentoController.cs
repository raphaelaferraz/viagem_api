﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using viagem_api.Data;
using viagem_api.Data.Dtos;
using viagem_api.Models;

namespace viagem_api.Controllers;

[ApiController]
[Route("[controller]")]
public class DepoimentoController : ControllerBase
{
    private ViagemContext _context;

    private IMapper _mapper;

    public DepoimentoController(IMapper mapper, ViagemContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpPost]
    public IActionResult AdicionaDepoimento(CreateDepoimentoDto depoimentoDto)
    {
        
        var depoimento = _mapper.Map<Depoimento>(depoimentoDto);

        _context.Depoimento.Add(depoimento);

        _context.SaveChanges();

        return CreatedAtAction(nameof(ListaDepoimentos), new {id = depoimento.Id}, depoimento);
    }

    [HttpGet]
    public IActionResult ListaDepoimentos()
    {
        List<Depoimento> depoimentos = _context.Depoimento.ToList();

        if (depoimentos.Count == 0) return NotFound();

        List<ReadDepoimentoDto> depoimentoDto = _mapper.Map<List<ReadDepoimentoDto>>(depoimentos);

        return Ok(depoimentoDto);
    }

    [HttpGet("{id}")]
    public IActionResult ListaDepoimentosPorId(int id)
    {
        Depoimento depoimento = _context.Depoimento.FirstOrDefault(depoimento => depoimento.Id == id);

        if (depoimento == null) return NotFound();

        ReadDepoimentoDto depoimentoDto = _mapper.Map<ReadDepoimentoDto>(depoimento);

        return Ok(depoimentoDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaDepoimento(int id, [FromBody] UpdateDepoimentoDto depoimentoDto)
    {
        Depoimento depoimento = _context.Depoimento.FirstOrDefault(depoimento => depoimento.Id == id);

        if(depoimento == null)
        {
            return NotFound();
        }

        _mapper.Map(depoimentoDto, depoimento);

        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaDepoimento(int id)
    {
        Depoimento depoimento = _context.Depoimento.FirstOrDefault(depoimento => depoimento.Id == id);

        if (depoimento == null)
        {
            return NotFound();
        }

        _context.Remove(depoimento);

        _context.SaveChanges();

        return NoContent();
    }
}
