using System;

namespace LocadoraFilmes.Domain.Validation
{
    public class DomainExceptionValidation : Exception
    {
        //Classe criada para poder fazer a validação de regras da entidade,
        //se houver alguma falha, retornará para as camadas superiores
        public DomainExceptionValidation(string message) : base(message)
        {
        }
        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new DomainExceptionValidation(error);
        }
    }
}
