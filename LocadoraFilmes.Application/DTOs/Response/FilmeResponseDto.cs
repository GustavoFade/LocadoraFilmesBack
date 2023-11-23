using System;
using System.Collections.Generic;
using System.Linq;

namespace LocadoraFilmes.Application.DTOs.Response
{
    public class FilmeResponseDto
    {
        public string Nome { get; set; }
        public int? Id { get; set; }
        public DateTime? DataCriacao { get; set; }
        public bool Ativo { get; set; }
        public List<GeneroResponseDto> Generos { get; set; } = Enumerable.Empty<GeneroResponseDto>().ToList();
    }
}
