using LocadoraFilmes.Domain.Entities.Common;
using LocadoraFilmes.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraFilmes.Domain.Entities
{
    // As propriedades são privadas para garantir que apenas os métodos internos possam modificá-las.
    // Isso evita alterações não validadas e garante que a entidade seja salva somente após a validação adequada.
    public sealed class Locacao : Entity
    {
        // Construtor padrão para a criação de uma instância de Cliente.
        // Útil em cenários onde a entidade precisa ser inicializada sem dados específicos.
        public Locacao()
        {

        }
        public Locacao(List<Filme> filmesLocacao, string cpfCliente, DateTime? dataLocacao)
        {
            ValidateFilmes(filmesLocacao);
            ValidateCpf(cpfCliente);
            ValidateDataLocacao(dataLocacao);

            FilmesLocacao = filmesLocacao;
            CpfCliente = cpfCliente;
            DataLocacao = dataLocacao;
        }
        public Locacao(int id, List<Filme> filmesLocacao, string cpfCliente, DateTime? dataLocacao)
        {
            ValidateFilmes(filmesLocacao);
            ValidateCpf(cpfCliente);
            ValidateDataLocacao(dataLocacao);

            Id = id;
            FilmesLocacao = filmesLocacao;
            CpfCliente = cpfCliente;
            DataLocacao = dataLocacao;
        }

        public int? Id { get; private set; }
        public List<Filme> FilmesLocacao { get; private set; } = Enumerable.Empty<Filme>().ToList();
        public int IdFilmesLocacao { get; private set; }
        public string CpfCliente { get; private set; }
        public DateTime? DataLocacao { get; private set; }

        public Cliente Cliente { get; private set; }

        private void ValidateCpf(string cpfCliente)
        {
            DomainExceptionValidation.When(cpfCliente.Length > 14, "Cpf deve ser menor que 14 caracteres!");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(cpfCliente), "Cpf não pode ser vazio!");
        }

        private void ValidateFilmes(List<Filme> filmesLocacao)
        {
            DomainExceptionValidation.When(filmesLocacao is null, "Lista de filmes não pode ser nulla!");
        }

        private void ValidateDataLocacao(DateTime? dataLocacao)
        {
            DomainExceptionValidation.When(dataLocacao is null, "Data de locação não pode ser vazia!");
        }
    }
}
