﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using viagem_api.Controllers;
using viagem_api.Data;
using viagem_api.Data.Dtos;
using viagem_api.Models;
using viagem_api.Profiles;

namespace viagem_api.Tests;

[TestFixture]
public class DestinoTest
{
    private DestinoController _controller;
    private ViagemContext _context;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        // Configuração incial antes de cada teste
        var options = new DbContextOptionsBuilder<ViagemContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ViagemContext(options);

        // Configuração do Mapper usando AutoMapper.Configuration.MapperConfigurationExpression
        var mockMapper = new MapperConfiguration(configuracao =>
        {
            configuracao.AddProfile(new DestinoProfile());
        });

        _mapper = mockMapper.CreateMapper();

        _controller = new DestinoController(_context, _mapper);
    }

    [TearDown]
    public void TearDown()
    {
        // Limpeza após cada teste
        _context.Database.EnsureCreated();
        _context.Dispose();
    }

    [Test]
    public void ListaDestinos_RetornaTodosOsDestinos()
    {
        // Arrange - Configuração do ambiente para o teste
        var destinos = new List<Destino>
        {
            new Destino { Nome = "Destino 1",
            UrlFoto1 = "Foto.png",
            UrlFoto2 = "Foto2.png",
            Preco = 50,
            Meta = "Meta destino 1",
            TextoDescritivo = "Texto descritivo destino 1"}
        };

        _context.Destino.AddRange(destinos);

        _context.SaveChanges();

        // Act - Execução do método que está sendo testado
        var resultado = _controller.ListaDestinos();

        // Assert - Verificação dos resultados
        Assert.IsInstanceOf<OkObjectResult>(resultado);

        var resultadoOk = resultado as OkObjectResult;
        Assert.IsNotNull(resultadoOk);

        var listaDestinoDto = resultadoOk.Value as List<ReadDestinoDto>;
        Assert.IsNotNull(listaDestinoDto);
        Assert.AreEqual(1, listaDestinoDto.Count);
    }

    [Test]
    public void ListaDestinosPorId_ExisteId_RetornaDestino()
    {
        // Arrange 
        var destinoId = 1;
        var destino = new Destino
        {
            Nome = "Destino 1",
            UrlFoto1 = "Foto.png",
            UrlFoto2 = "Foto2.png",
            Preco = 50,
            Meta = "Meta destino 1",
            TextoDescritivo = "Texto descritivo destino 1"
        };

        _context.Destino.Add(destino);

        _context.SaveChanges();

        // Act 
        var resultado = _controller.ListaDestinosPorId(destinoId);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(resultado);

        var resultadoOk = resultado as OkObjectResult;
        Assert.IsNotNull(resultadoOk.Value);

        var destinoDto = resultadoOk.Value as ReadDestinoDto;
        Assert.IsNotNull(destinoDto);
        Assert.AreEqual(destino.Nome, destinoDto.Nome);
        Assert.AreEqual(destino.UrlFoto1, destinoDto.UrlFoto1);
        Assert.AreEqual(destino.UrlFoto2, destinoDto.UrlFoto2);
        Assert.AreEqual(destino.Meta, destinoDto.Meta);
        Assert.AreEqual(destino.TextoDescritivo, destinoDto.TextoDescritivo);
        Assert.AreEqual(destino.Preco, destinoDto.Preco);
    }

    [Test]
    public void ListaDestinosPorId_NaoExisteId_RetornaNotFoundEDestino()
    {
        // Arrange
        var destinoId = 1;

        // Act
        var resultado = _controller.ListaDestinosPorId(destinoId);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(resultado);
    }

    [Test]
    public void BuscaDestino_ExisteDestino_RetornaDestino()
    {
        // Arrange
        var nomeDestino = "Destino 1";
        var destino = new Destino
        {
            Nome = "Destino 1",
            UrlFoto1 = "Foto.png",
            UrlFoto2 = "Foto2.png",
            Preco = 50,
            Meta = "Meta destino 1",
            TextoDescritivo = "Texto descritivo destino 1"
        };

        _context.Destino.Add(destino);

        _context.SaveChanges();

        // Act
        var resultado = _controller.BuscaDestino(nomeDestino);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(resultado);
        
        var resultadoOk = resultado as OkObjectResult;
        Assert.IsNotNull(resultadoOk.Value);

        var destinoDto = resultadoOk.Value as ReadDestinoDto;
        Assert.IsNotNull(destinoDto);
        Assert.AreEqual(nomeDestino, destinoDto.Nome);
    }

    [Test]
    public void BuscaDestino_NaoExisteDestino_RetornaNotFound()
    {
        // Arrange 
        var nomeDestino = "Destino Inexistente";

        // Act
        var resultado = _controller.BuscaDestino(nomeDestino);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(resultado);
    }

    [Test]
    public void AdicionaDestino_DestinoValido_CriaERetornaDestino()
    {
        // Arrange 
        var destinoDto = new CreateDestinoDto
        {
            Nome = "Destino 1",
            UrlFoto1 = "Foto.png",
            UrlFoto2 = "Foto2.png",
            Preco = 50,
            Meta = "Meta destino 1",
            TextoDescritivo = "Texto descritivo destino 1"
        };

        // Act 
        var resultado = _controller.AdicionaDestino(destinoDto);

        // Assert 
        Assert.IsInstanceOf<CreatedAtActionResult>(resultado); ;

        var resultadoDeCriacao = resultado as CreatedAtActionResult;
        Assert.IsNotNull(resultadoDeCriacao.Value);

        var destinoDtoResultado = resultadoDeCriacao.Value as Destino;
        Assert.IsNotNull(destinoDtoResultado);
        Assert.AreEqual(destinoDto.Nome, destinoDtoResultado.Nome);
        Assert.AreEqual(destinoDto.UrlFoto1, destinoDtoResultado.UrlFoto1);
        Assert.AreEqual(destinoDto.UrlFoto2, destinoDtoResultado.UrlFoto2);
        Assert.AreEqual(destinoDto.Meta, destinoDtoResultado.Meta);
        Assert.AreEqual(destinoDto.TextoDescritivo, destinoDtoResultado.TextoDescritivo);
        Assert.AreEqual(destinoDto.Preco, destinoDtoResultado.Preco);

        // Verifica se o destino foi adicionado ao contexto de banco de dados
        Assert.AreEqual(1, _context.Destino.Count());
        Assert.AreEqual(destinoDto.Nome, _context.Destino.First().Nome);
        Assert.AreEqual(destinoDto.UrlFoto1, _context.Destino.First().UrlFoto1);
        Assert.AreEqual(destinoDto.UrlFoto2, _context.Destino.First().UrlFoto2);
        Assert.AreEqual(destinoDto.Meta, _context.Destino.First().Meta);
        Assert.AreEqual(destinoDto.TextoDescritivo, _context.Destino.First().TextoDescritivo);
        Assert.AreEqual(destinoDto.Preco, _context.Destino.First().Preco);
    }

    [Test]
    public void AtualizaDestino_ExisteId_RetornaDestinoENoContent()
    {
        // Arrange
        var destinoId = 1;
        var destino = new Destino
        {
            Id = destinoId,
            Nome = "Destino 1",
            UrlFoto1 = "Foto.png",
            UrlFoto2 = "Foto2.png",
            Preco = 50,
            Meta = "Meta destino 1",
            TextoDescritivo = "Texto descritivo destino 1"
        };

        _context.Destino.Add(destino);

        _context.SaveChanges();

        var destinoUpdateDto = new UpdateDestinoDto
        {
            Nome = "Destino 1 atualizado",
            UrlFoto1 = "Foto.png",
            UrlFoto2 = "Foto2.png",
            Preco = 50,
            Meta = "Meta destino 1",
            TextoDescritivo = "Texto descritivo destino 1"
        };

        // Act
        var resultado = _controller.AtualizaDestino(destinoId, destinoUpdateDto);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(resultado);

        // Verifica se o destino foi atualizado
        var destinoAtualizado = _context.Destino.FirstOrDefault(destino => destino.Id == destinoId);
        Assert.IsNotNull(destinoAtualizado);
        Assert.AreEqual(destinoUpdateDto.Nome, destinoAtualizado.Nome);
        Assert.AreEqual(destinoUpdateDto.UrlFoto1, destinoAtualizado.UrlFoto1);
        Assert.AreEqual(destinoUpdateDto.UrlFoto2, destinoAtualizado.UrlFoto2);
        Assert.AreEqual(destinoUpdateDto.Meta, destinoAtualizado.Meta);
        Assert.AreEqual(destinoUpdateDto.TextoDescritivo, destinoAtualizado.TextoDescritivo);
        Assert.AreEqual(destinoUpdateDto.Preco, destinoAtualizado.Preco);
    }

    [Test]
    public void AtualizaDestino_NaoExisteId_RetornaDestinoENotFound()
    {
        // Arrange 
        var destinoId = 1;
        var destinoUpdate = new UpdateDestinoDto
        {
            Nome = "Destino 1 atualizado",
            UrlFoto1 = "Foto.png",
            UrlFoto2 = "Foto2.png",
            Preco = 50,
            Meta = "Meta destino 1",
            TextoDescritivo = "Texto descritivo destino 1"
        };

        // Act 
        var resultado = _controller.AtualizaDestino(destinoId, destinoUpdate);

        // Assert 
        Assert.IsInstanceOf<NotFoundResult>(resultado);
    }

    [Test]
    public void DeletaDestino_ExisteId_RetornaDestinoENoContent()
    {
        // Arrange
        var destinoId = 1;
        var destino = new Destino
        {
            Nome = "Destino 1",
            UrlFoto1 = "Foto.png",
            UrlFoto2 = "Foto2.png",
            Preco = 50,
            Meta = "Meta destino 1",
            TextoDescritivo = "Texto descritivo destino 1"
        };

        _context.Destino.Add(destino);

        _context.SaveChanges();

        // Act 
        var resultado = _controller.DeletaDestino(destinoId);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(resultado);

        var destinoDeletado = _context.Destino.FirstOrDefault(destino => destino.Id == destinoId);
        Assert.IsNull(destinoDeletado);
    }

    [Test]
    public void DeletaDestino_NaoExisteId_RetornaDestinoENotFound()
    {
        // Arrange 
        var destinoId = 1;

        // Act
        var resultado = _controller.DeletaDestino(destinoId);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(resultado);
    }
}
