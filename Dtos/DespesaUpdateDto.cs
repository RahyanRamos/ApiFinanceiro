using System.ComponentModel.DataAnnotations;

namespace ApiFinanceiro.Dtos
{
    public class DespesaUpdateDto : DespesaDto
    {
        [Required]
        public required string Situacao { get; set; } = "Pendente";
        [Required]
        public DateTime DataPagemnto { get; set; }
    }
}
