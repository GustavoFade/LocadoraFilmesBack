using System;

namespace LocadoraFilmes.Application.DTOs.Request
{
    public class GeneroRequestDto
    {
        public string Nome { get; set; }
        public DateTime? DataCriacao { get; set; }
        public bool Ativo { get; set; }

    }
}
