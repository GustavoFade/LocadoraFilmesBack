using LocadoraFilmes.Domain.Abstractions.Security;
using System;
using System.Security.Cryptography;

namespace LocadoraFilmes.Infra.Data.Security
{
    public class CryptographyPassword : ICryptographyPassword
    {
        public string CriptografaHMACSHA256(string senha, string chave)
        {
            string retorno = string.Empty;
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(chave);
            byte[] messageBytes = encoding.GetBytes(senha);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                hashmessage.ToString();
                retorno = BitConverter.ToString(hashmessage).Replace("-", "").ToUpper();
                return retorno;
            }
        }
    }
}
