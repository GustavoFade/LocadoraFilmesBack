using System;

namespace LocadoraFilmes.Application.DTOs.Request
{
    //Criado classes Request e Response pq vi a nessecidade de a entrada
    //ser diferente da resposta, então decidi criar
    public class GeneroRequestDto
    {
        public string Nome { get; set; }
        public DateTime? DataCriacao { get; set; }
        public bool Ativo { get; set; }

    }
}
