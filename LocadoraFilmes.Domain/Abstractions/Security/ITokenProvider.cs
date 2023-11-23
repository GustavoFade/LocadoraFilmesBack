namespace LocadoraFilmes.Domain.Abstractions.Security
{
    public interface ITokenProvider
    {
        string GenerateToken(string cpf);
    }
}
