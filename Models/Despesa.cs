namespace ApiFinanceiro.Models
{
    public class Despesa
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Descricao { get; set; }
        public required string Categoria { get; set; }
        public required decimal Valor { get; set; }
        public required DateOnly DataVencimento { get; set; }
        public required string Situacao { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
}
