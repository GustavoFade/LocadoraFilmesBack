using LocadoraFilmes.Domain.Entities;
using LocadoraFilmes.Domain.Validation;
using Xunit;

namespace LocadoraFilmes.Domain.Tests
{
    public class ClienteUnitTest
    {
        [Fact]
        public void Cliente_DeveSerValidoComDadosCorretos()
        {
            // Arrange
            var cpf = "12345678901";
            var senha = "Senha123";

            // Act
            var cliente = new Cliente(cpf, senha);

            // Assert
            Assert.NotNull(cliente);
            Assert.Equal(cpf, cliente.Cpf);
            Assert.Equal(senha, cliente.Senha);
        }

        [Fact]
        public void Cliente_DeveLancarExcecaoSeCpfForMaiorQue14Caracteres()
        {
            // Arrange
            var cpf = "123456789012345";
            var senha = "Senha123";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => new Cliente(cpf, senha));
        }

        [Fact]
        public void Cliente_DeveLancarExcecaoSeCpfForVazio()
        {
            // Arrange
            var cpf = string.Empty;
            var senha = "Senha123";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => new Cliente(cpf, senha));
        }

        [Fact]
        public void Cliente_DeveLancarExcecaoSeSenhaForVazia()
        {
            // Arrange
            var cpf = "12345678901";
            var senha = string.Empty;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() => new Cliente(cpf, senha));
        }
    }
}
