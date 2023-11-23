using LocadoraFilmes.Domain.Entities.Common;
using LocadoraFilmes.Domain.Validation;
using System;
using System.Collections.Generic;

namespace LocadoraFilmes.Domain.Entities
{
    public sealed class Filme : Entity
    {
        public Filme()
        {

        }
        public Filme(int id)
        {
            Id = Id;
        }
        public Filme(string nome, bool ativo, List<Genero> generos)
        {
            ValidateName(nome);
            Nome = nome;
            Ativo = ativo;
            Generos = generos;
        }

        public Filme(int id, string nome, bool ativo, List<Genero> generos)
        {
            ValidateName(nome);
            Id = id;
            Nome = nome;
            Ativo = ativo;
            Generos = generos;
        }

        public void AtualizarEntidade(string nome, bool ativo, List<Genero> generos)
        {
            ValidateName(nome);
            Nome = nome;
            Ativo = ativo;
            Generos = generos;
        }
        public void ChangeStatus(bool ativo)
        {
            Ativo = ativo;
        }
        public void AdicionarGeneros(List<Genero> generos)
        {
            Generos = generos;
        }

        public int? Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime? DataCriacao { get; private set; }
        public bool Ativo { get; private set; }

        public List<Genero> Generos { get; private set; }

        public List<Locacao> Locacoes { get; private set; }

        private void ValidateName(string nome)
        {
            DomainExceptionValidation.When(nome.Length > 200, "Nome do filme deve ser menor que 200 caracteres!");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(nome), "Nome do filme não pode ser vazio!");
        }
    }
}
