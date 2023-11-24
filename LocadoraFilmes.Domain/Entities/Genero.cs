using LocadoraFilmes.Domain.Entities.Common;
using LocadoraFilmes.Domain.Validation;
using System;
using System.Collections.Generic;

namespace LocadoraFilmes.Domain.Entities
{
    // As propriedades são privadas para garantir que apenas os métodos internos possam modificá-las.
    // Isso evita alterações não validadas e garante que a entidade seja salva somente após a validação adequada.
    public sealed class Genero : Entity
    {
        // Construtor padrão para a criação de uma instância de Cliente.
        // Útil em cenários onde a entidade precisa ser inicializada sem dados específicos.
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
