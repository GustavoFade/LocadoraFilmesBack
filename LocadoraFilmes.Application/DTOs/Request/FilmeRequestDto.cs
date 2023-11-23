using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LocadoraFilmes.Application.DTOs.Request
{
    public class FilmeRequestDto
    {

        [Required]
        [MaxLength(200)]
        public string Nome { get; set; }
        public DateTime? DataCriacao { get; set; }
        public bool Ativo { get; set; }
        public List<int> IdsGenero { get; set; } = Enumerable.Empty<int>().ToList();

    }
}
