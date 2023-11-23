using LocadoraFilmes.Domain.Entities.Common;
using LocadoraFilmes.Domain.Validation;
using System;
using System.Collections.Generic;

namespace LocadoraFilmes.Domain.Entities
{
    public sealed class Genero : Entity
    {
        public Genero() { }
        public Genero(int id) { Id = id; }
        public Genero(string nome, bool ativo)
        {
            ValidateNome(nome);
            Nome = nome;
            Ativo = ativo;
        }

        public Genero(int id, string nome, bool ativo)
        {
            ValidateNome(nome);
            Id = id;
            Nome = nome;
            Ativo = ativo;
        }

        public void ChangeIdGenero(int idGenero)
        {
            Id = idGenero;
        }

        public void ChangeStatus(bool ativo)
        {
            Ativo = ativo;
        }

        public int? Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime? DataCriacao { get; private set; }
        public bool Ativo { get; private set; }
        public List<Filme> Filme { get; private set; }

        private void ValidateNome(string nome)
        {
            DomainExceptionValidation.When(nome.Length > 100, "Nome do gênero deve ser menor que 100 caracteres!");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(nome), "Nome do gênero não pode ser vazio!");
        }
    }
}
