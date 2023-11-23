using LocadoraFilmes.Domain.Abstractions.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LocadoraFilmes.Infra.Data.Security
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _configuration;
        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(string cpf)
        {
            var jwtKey = _configuration.GetSection("JwtKey").Value;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtKey);
            var claims = GetClaims(cpf);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private IEnumerable<Claim> GetClaims(string cpf)
        {
            var result = new List<Claim>
            {
                new("Cpf", cpf)
            };
            return result;
        }
    }
}
