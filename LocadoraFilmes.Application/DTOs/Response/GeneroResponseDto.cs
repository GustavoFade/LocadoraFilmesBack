using System;

namespace LocadoraFilmes.Application.DTOs.Response
{
    public class GeneroResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? DataCriacao { get; set; }
        public bool Ativo { get; set; }

    }
}
