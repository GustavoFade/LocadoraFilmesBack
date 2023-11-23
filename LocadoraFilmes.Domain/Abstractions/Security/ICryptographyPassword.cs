namespace LocadoraFilmes.Domain.Abstractions.Security
{
    public interface ICryptographyPassword
    {
        string CriptografaHMACSHA256(string senha, string chave);
    }
}
