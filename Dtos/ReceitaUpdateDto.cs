using System.ComponentModel.DataAnnotations;

namespace ApiFinanceiro.Dtos
{
    public class ReceitaUpdateDto
    {
        [Required(ErrorMessage = "O campo 'Descricao' e obrigatorio.")]
        [MinLength(5, ErrorMessage = "O campo 'Descricao' deve conter pelo menos 5 caracteres.")]
        public required string Descricao { get; set; }

        [Required(ErrorMessage = "O campo 'Valor' e obrigatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O campo 'Valor' deve ser um numero positivo.")]
        public required decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo 'DataPrevisao' e obrigatorio.")]
        public required DateOnly DataPrevisao { get; set; }

        public DateOnly? DataRecebimento { get; set; }

        [Required(ErrorMessage = "O campo 'Categoria' e obrigatorio.")]
        public required string Categoria { get; set; }

        public string? Observacao { get; set; }

        [Required(ErrorMessage = "O campo 'Situacao' e obrigatorio.")]
        public required string Situacao { get; set; } = "Pendente";
    }
}
