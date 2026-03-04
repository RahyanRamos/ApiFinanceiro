using System.ComponentModel.DataAnnotations;

namespace ApiFinanceiro.Dtos
{
    public class DespesaDto
    {
        [Required(ErrorMessage = "O campo 'Descricao' é obrigatório.")]
        [MinLength(5, ErrorMessage = "O campo 'Descricao' deve conter pelo menos 5 caracteres.")]
        public required string Descricao { get; set; }

        [Required(ErrorMessage = "O campo 'Categoria' é obrigatório.")]
        public required string Categoria { get; set; }

        [Required(ErrorMessage = "O campo 'Valor' é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O campo 'Valor' deve ser um número positivo.")]
        public required decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo 'DataVencimento' é obrigatório.")]
        public required DateOnly DataVencimento { get; set; }
    }
}
