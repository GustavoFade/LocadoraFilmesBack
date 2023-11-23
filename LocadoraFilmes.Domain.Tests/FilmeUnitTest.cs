using LocadoraFilmes.Domain.Entities;
using LocadoraFilmes.Domain.Validation;
using System.Collections.Generic;
using Xunit;

namespace LocadoraFilmes.Domain.Tests
{
    public class FilmeUnitTest
    {
        [Fact]
        public void Filme_CriacaoComSucesso()
        {
            // Arrange
            var nome = "Filme Teste";
            var ativo = true;
            var generos = new List<Genero> { new Genero("Ação", true) };

            // Act
            var filme = new Filme(nome, ativo, generos);

            // Assert
            Assert.NotNull(filme);
            Assert.Equal(nome, filme.Nome);
            Assert.True(filme.Ativo);
            Assert.Equal(generos, filme.Generos);
        }

        [Fact]
        public void Filme_CriacaoComNomeVazio_DeveLancarExcecao()
        {
            // Arrange
            var nome = "";
            var ativo = true;
            var generos = new List<Genero> { new Genero("Ação", true) };

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => new Filme(nome, ativo, generos));
        }

        [Fact]
        public void Filme_CriacaoComNomeMaiorQue200Caracteres_DeveLancarExcecao()
        {
            // Arrange
            var nome = new string('A', 201);
            var ativo = true;
            var generos = new List<Genero> { new Genero("Ação", true) };

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => new Filme(nome, ativo, generos));
        }

        [Fact]
        public void Filme_AdicionarGeneros_ComSucesso()
        {
            // Arrange
            var filme = new Filme("Filme Teste", true, new List<Genero> { new Genero("Ação", true) });
            var novosGeneros = new List<Genero> { new Genero("Drama", true), new Genero("Comédia", true) };

            // Act
            filme.AdicionarGeneros(novosGeneros);

            // Assert
            Assert.Equal(novosGeneros, filme.Generos);
        }

        [Fact]
        public void Filme_ChangeStatus_AtivoParaInativo_ComSucesso()
        {
            // Arrange
            var filme = new Filme("Filme Teste", true, new List<Genero> { new Genero("Ação", true) });

            // Act
            filme.ChangeStatus(false);

            // Assert
            Assert.False(filme.Ativo);
        }
    }
}
