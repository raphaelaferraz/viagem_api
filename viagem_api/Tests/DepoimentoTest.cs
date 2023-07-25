using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using viagem_api.Controllers;
using viagem_api.Data.Dtos;
using viagem_api.Data;
using AutoMapper;
using viagem_api.Models;
using Moq;
using System.Collections.Generic;
using viagem_api.Profiles;
using viagem_api.Migrations;

namespace viagem_api.Tests;

[TestFixture]
public class DepoimentoTest
{
    private DepoimentoController _controller;
    private ViagemContext _context;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        // Configuração inicial antes de cada teste
        var options = new DbContextOptionsBuilder<ViagemContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ViagemContext(options);

        // Configurar o Mapper usando AutoMapper.Configuration.MapperConfigurationExpression
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DepoimentoProfile());
        });
        _mapper = mockMapper.CreateMapper();

        _controller = new DepoimentoController(_mapper, _context);
    }

    [TearDown]
    public void TearDown()
    {
        // Limpeza após cada teste
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public void ListaDepoimentos_RetornaTodosOsDepoimentos()
    {
        // Arrange - Configuração do ambiente para o teste
        var depoimentos = new List<Models.Depoimento>
        {
            new Models.Depoimento { NomeUsuario = "Usuário 1", UrlFoto = "foto.png", DepoimentoUsuario = "Teste"},
        };

        _context.Depoimento.AddRange(depoimentos);
        _context.SaveChanges();

        // Act - Execução do método que está sendo testado
        var result = _controller.ListaDepoimentos();

        // Assert - Verificação dos resultados
        Assert.IsInstanceOf<OkObjectResult>(result);

        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult.Value);

        var depoimentoDtoList = okResult.Value as List<ReadDepoimentoDto>;
        Assert.IsNotNull(depoimentoDtoList);
        Assert.AreEqual(1, depoimentoDtoList.Count);
    }

    [Test]
    public void ListaDepoimentosPorId_ExisteId_RetornaDepoimento()
    {
        // Arrange - Configuração do ambiente para o teste
        var depoimentoId = 1;
        var depoimento = new Models.Depoimento { Id = depoimentoId, NomeUsuario = "Usuário 1", DepoimentoUsuario = "Depoimento do Usuário 1", UrlFoto = "foto.png" };

        _context.Depoimento.Add(depoimento);
        _context.SaveChanges();

        // Act - Execução do método que está sendo testado
        var resultado = _controller.ListaDepoimentosPorId(depoimentoId);

        // Assert - Verificação dos resultados do teste
        Assert.IsInstanceOf<OkObjectResult>(resultado);

        var resultadoOk = resultado as OkObjectResult;
        Assert.IsNotNull(resultadoOk.Value);

        var depoimentoDto = resultadoOk.Value as ReadDepoimentoDto;
        Assert.IsNotNull(depoimentoDto);
        Assert.AreEqual(depoimento.NomeUsuario, depoimentoDto.NomeUsuario);
        Assert.AreEqual(depoimento.DepoimentoUsuario, depoimentoDto.DepoimentoUsuario);
        Assert.AreEqual(depoimento.UrlFoto, depoimentoDto.UrlFoto);

    }

    [Test]
    public void ListaDepoimentosPorId_NaoExisteId_RetornaNotFoundEDepoimento()
    {
        // Arrange - Configuração do ambiente para o teste
        var depoimentoId = 1;

        // Act - Execução do método que será testado
        var resultado = _controller.ListaDepoimentosPorId(depoimentoId);

        // Assert - Verificação dos resultados
        Assert.IsInstanceOf<NotFoundResult>(resultado);
    }

    [Test]
    public void AdicionaDepoimento_DepoimentoValido_CriaERetornaDepoimento()
    {
        // Arrange - Configuração do ambiente para o teste
        var depoimentoDto = new CreateDepoimentoDto
        {
            NomeUsuario = "Usuário 1",
            DepoimentoUsuario = "Depoimento usuário 1",
            UrlFoto = "foto.png"
        };

        // Act - Execução do método que está sendo testado
        var resultado = _controller.AdicionaDepoimento(depoimentoDto);

        // Assert - Verificação dos resultados
        Assert.IsInstanceOf<CreatedAtActionResult>(resultado);

        var resultadoDeCriacao = resultado as CreatedAtActionResult;
        Assert.IsNotNull(resultadoDeCriacao.Value);

        var depoimentoDtoResultado = resultadoDeCriacao.Value as Models.Depoimento;
        Assert.IsNotNull(depoimentoDtoResultado);
        Assert.AreEqual(depoimentoDto.NomeUsuario, depoimentoDtoResultado.NomeUsuario);
        Assert.AreEqual(depoimentoDto.DepoimentoUsuario, depoimentoDtoResultado.DepoimentoUsuario);
        Assert.AreEqual(depoimentoDto.UrlFoto, depoimentoDtoResultado.UrlFoto);

        // Verifica se o depoimento foi adicionado ao contexto do banco de dados
        Assert.AreEqual(1, _context.Depoimento.Count());
        Assert.AreEqual(depoimentoDto.NomeUsuario, _context.Depoimento.First().NomeUsuario);
        Assert.AreEqual(depoimentoDto.DepoimentoUsuario, _context.Depoimento.First().DepoimentoUsuario);
        Assert.AreEqual(depoimentoDto.UrlFoto, _context.Depoimento.First().UrlFoto);
    }

    public void AtualizaDepoimento_ExisteId_RetornaDepoimentoAtualizadoENoContent()
    {
        // Arrange 
        var depoimentoId = 1;
        var depoimento = new Models.Depoimento
        {
            Id = depoimentoId,
            NomeUsuario = "Usuário 1",
            DepoimentoUsuario = "Depoimento usuário 1",
            UrlFoto = "foto.png"
        };

        _context.Depoimento.Add(depoimento);
        _context.SaveChanges();

        var depoimentoUpdateDto = new UpdateDepoimentoDto
        {
            NomeUsuario = "Usuário atualizado",
            DepoimentoUsuario = "Depoimento atualizado",
            UrlFoto = "foto-atualizada.png"
        };

        // Act
        var resultado = _controller.AtualizaDepoimento(depoimentoId, depoimentoUpdateDto);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(resultado);

        // Verifica se o depoimento foi atualizado
        var depoimentoAtualizado = _context.Depoimento.FirstOrDefault(depoimento => depoimento.Id == depoimentoId);
        Assert.IsNotNull(depoimentoAtualizado);
        Assert.AreEqual(depoimentoUpdateDto.NomeUsuario, depoimentoAtualizado.NomeUsuario);
        Assert.AreEqual(depoimentoUpdateDto.DepoimentoUsuario, depoimentoAtualizado.DepoimentoUsuario);
        Assert.AreEqual(depoimentoUpdateDto.UrlFoto, depoimentoAtualizado.UrlFoto);
    }

    [Test]
    public void AtualizaDepoimento_NaoExisteId_RetornaDepoimentoAtualizadoENotFound()
    {
        // Arrange 
        var depoimentoId = 1;
        var depoimentoUpdate = new UpdateDepoimentoDto
        {
            NomeUsuario = "Usuário 1",
            DepoimentoUsuario = "Depoimento do usuário 1",
            UrlFoto = "foto.png"
        };

        // Act
        var resultado = _controller.AtualizaDepoimento(depoimentoId, depoimentoUpdate);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(resultado);
    }

    [Test]
    public void DeletaDepoimento_ExisteId_RetornaDepoimentoENoContent()
    {
        // Arrange 
        var depoimentoId = 1;
        var depoimento = new Models.Depoimento
        {
            Id = depoimentoId,
            NomeUsuario = "Usuário 1",
            DepoimentoUsuario = "Depoimento do usuário 1",
            UrlFoto = "foto.png"
        };

        _context.Depoimento.Add(depoimento);
        _context.SaveChanges();

        // Act
        var resultado = _controller.DeletaDepoimento(depoimentoId);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(resultado);

        var depoimentoDeletado = _context.Depoimento.FirstOrDefault(depoimento => depoimento.Id == depoimentoId);
        Assert.IsNull(depoimentoDeletado);
    }

    [Test]
    public void DeletaDepoimento_NaoExisteId_RetornaNotFound()
    {
        // Arrange 
        var depoimentoId = 1;

        // Act
        var resultado = _controller.DeletaDepoimento(depoimentoId);

        // Assert 
        Assert.IsInstanceOf<NotFoundResult>(resultado);
    }
}
