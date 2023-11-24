using LocadoraFilmes.Domain.Entities.Common;
using LocadoraFilmes.Domain.Validation;
using System;

namespace LocadoraFilmes.Domain.Entities
{
    // As propriedades são privadas para garantir que apenas os métodos internos possam modificá-las.
    // Isso evita alterações não validadas e garante que a entidade seja salva somente após a validação adequada.
    public sealed class Cliente : Entity
    {
        // Construtor padrão para a criação de uma instância de Cliente.
        // Útil em cenários onde a entidade precisa ser inicializada sem dados específicos.
        public Cliente()
        {

        }
        public Cliente(string cpf, string senha)
        {
            ValidateCpf(cpf);
            ValidateSenha(senha);
            Cpf = cpf;
            Senha = senha;
        }
        public Cliente(int id, string cpf, string senha)
        {
            ValidateCpf(cpf);
            ValidateSenha(senha);
            Id = id;
            Cpf = cpf;
            Senha = senha;
        }

        public int? Id { get; private set; }
        public string Cpf { get; private set; }
        public string Senha { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public Locacao Locacao { get; private set; }

        private void ValidateCpf(string cpf)
        {
            DomainExceptionValidation.When(cpf.Length > 14, "Cpf deve ser menor que 14 caracteres!");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(cpf), "Cpf não pode ser vazio!");
        }
        private void ValidateSenha(string senha)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(senha), "Senha não pode ser vazia!");
        }
    }
}
