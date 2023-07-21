using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using viagem_api.Data;
using viagem_api.Data.Dtos;
using viagem_api.Models;

namespace viagem_api.Controllers;

[ApiController]
[Route("depoimentos-home")]
public class DepoimentoHomeController : ControllerBase
{
    private ViagemContext _context;

    public DepoimentoHomeController(ViagemContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult ListaDepoimentosRandomicos()
    {
        List<ReadDepoimentoDto> depoimentos = _context.Depoimento
        .OrderBy(d => Guid.NewGuid()) // Ordena aleatoriamente os depoimentos na memória
        .Take(3) // Pega os 3 primeiros depoimentos (aleatórios)
        .Select(depoimento => new ReadDepoimentoDto
        {
            UrlFoto = depoimento.UrlFoto,
            NomeUsuario = depoimento.NomeUsuario,
            DepoimentoUsuario = depoimento.DepoimentoUsuario,
        })
        .ToList();

        return Ok(depoimentos);
    }

}
