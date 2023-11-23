using LocadoraFilmes.Domain.Entities;
using LocadoraFilmes.Domain.Validation;
using System;
using System.Collections.Generic;
using Xunit;

namespace LocadoraFilmes.Domain.Tests
{
    public class LocacaoUnitTest
    {
        [Fact]
        public void Locacao_DeveSerValidaComDadosCorretos()
        {
            // Arrange
            var filmesLocacao = new List<Filme>
            {
                new Filme (1),
                new Filme (2)
            };
            var cpfCliente = "12345678901";
            var dataLocacao = DateTime.Now;

            // Act
            var locacao = new Locacao(filmesLocacao, cpfCliente, dataLocacao);

            // Assert
            Assert.NotNull(locacao);
        }

        [Fact]
        public void Locacao_DeveLancarExcecaoSeCpfForMaiorQue14Caracteres()
        {
            // Arrange
            var filmesLocacao = new List<Filme>
            {
                new Filme (1)
            };
            var cpfCliente = "123456789012345";
            var dataLocacao = DateTime.Now;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => new Locacao(filmesLocacao, cpfCliente, dataLocacao));
        }

        [Fact]
        public void Locacao_DeveLancarExcecaoSeCpfForVazio()
        {
            // Arrange
            var filmesLocacao = new List<Filme>
            {
                new Filme (1)
            };
            var cpfCliente = string.Empty;
            var dataLocacao = DateTime.Now;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => new Locacao(filmesLocacao, cpfCliente, dataLocacao));
        }

        [Fact]
        public void Locacao_DeveLancarExcecaoSeListaDeFilmesForNula()
        {
            // Arrange
            List<Filme> filmesLocacao = null;
            var cpfCliente = "12345678901";
            var dataLocacao = DateTime.Now;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => new Locacao(filmesLocacao, cpfCliente, dataLocacao));
        }

        [Fact]
        public void Locacao_DeveLancarExcecaoSeDataLocacaoForNula()
        {
            // Arrange
            var filmesLocacao = new List<Filme>
            {
                new Filme(1)
            };
            var cpfCliente = "12345678901";
            DateTime? dataLocacao = null;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => new Locacao(filmesLocacao, cpfCliente, dataLocacao));
        }
    }
}
