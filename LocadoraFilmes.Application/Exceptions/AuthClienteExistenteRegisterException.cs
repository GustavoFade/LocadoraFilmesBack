using System;

namespace LocadoraFilmes.Application.Exceptions
{
    public class AuthClienteExistenteRegisterException : Exception
    {
        const string message = "Cliente já cadastrado!";
        public AuthClienteExistenteRegisterException() : base(message)
        {
        }
    }
}
