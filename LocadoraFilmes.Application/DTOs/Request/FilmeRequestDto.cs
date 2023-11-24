using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LocadoraFilmes.Application.DTOs.Request
{
    //Criado classes Request e Response pq vi a nessecidade de a entrada
    //ser diferente da resposta, então decidi criar
    public class FilmeRequestDto
    {
        public string Nome { get; set; }
        public DateTime? DataCriacao { get; set; }
        public bool Ativo { get; set; }
        public List<int> IdsGenero { get; set; } = Enumerable.Empty<int>().ToList();

    }
}
