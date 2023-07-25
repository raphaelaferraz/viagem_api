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

namespace viagem_api.Tests
{
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
        public void ListaDepoimentos_ShouldReturnDepoimentos()
        {
            // Arrange - Configuração do ambiente para o teste
            var depoimentos = new List<Models.Depoimento>
            {
                new Models.Depoimento { NomeUsuario = "Usuário 1", UrlFoto = "foto.png", DepoimentoUsuario = "Teste"},
            };

            _context.Depoimento.AddRange(depoimentos);
            _context.SaveChanges();

            // Act - Execução do método que será testado
            var result = _controller.ListaDepoimentos();

            // Assert - Verificação dos resultados
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);

            var depoimentoDtoList = okResult.Value as List<ReadDepoimentoDto>;
            Assert.IsNotNull(depoimentoDtoList);
            Assert.AreEqual(1, depoimentoDtoList.Count); // Verifique se o número de depoimentos retornados está correto
        }
    }
}
