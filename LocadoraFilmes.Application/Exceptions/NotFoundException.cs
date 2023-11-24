using System;

namespace LocadoraFilmes.Application.Exceptions
{
    //Criado para poder ser identificado no controller que não foi achado a entidade
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
