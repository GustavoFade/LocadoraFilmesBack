using System.ComponentModel.DataAnnotations;

namespace LocadoraFilmes.Application.DTOs
{
    public class ClienteDto
    {
        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Senha { get; set; }
        public int? Id { get; set; }

    }
}
