using System;

namespace LocadoraFilmes.Application.DTOs.Response
{
    //Criado classes Request e Response pq vi a nessecidade de a entrada
    //ser diferente da resposta, então decidi criar
    public class GeneroResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? DataCriacao { get; set; }
        public bool Ativo { get; set; }

    }
}
