using LocadoraFilmes.Domain.Entities;
using LocadoraFilmes.Domain.Validation;
using Xunit;

namespace LocadoraFilmes.Domain.Tests
{
    public class GeneroUnitTest
    {
        [Fact]
        public void Genero_DeveSerValidoComDadosCorretos()
        {
            // Arrange
            var nome = "Ação";
            var ativo = true;

            // Act
            var genero = new Genero(nome, ativo);

            // Assert
            Assert.NotNull(genero);
            Assert.Equal(nome, genero.Nome);
            Assert.Equal(ativo, genero.Ativo);
        }

        [Fact]
        public void Genero_DeveLancarExcecaoSeNomeForMaiorQue100Caracteres()
        {
            // Arrange
            var nome = new string('A', 101);
            var ativo = true;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => new Genero(nome, ativo));
        }

        [Fact]
        public void Genero_DeveLancarExcecaoSeNomeForVazio()
        {
            // Arrange
            var nome = string.Empty;
            var ativo = true;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => new Genero(nome, ativo));
        }

        [Fact]
        public void Genero_DevePermitirAlterarIdGenero()
        {
            // Arrange
            var genero = new Genero("Ação", true);
            var novoId = 2;

            // Act
            genero.ChangeIdGenero(novoId);

            // Assert
            Assert.Equal(novoId, genero.Id);
        }

        [Fact]
        public void Genero_DevePermitirAlterarStatus()
        {
            // Arrange
            var genero = new Genero("Ação", true);
            var novoStatus = false;

            // Act
            genero.ChangeStatus(novoStatus);

            // Assert
            Assert.Equal(novoStatus, genero.Ativo);
        }
    }
}
