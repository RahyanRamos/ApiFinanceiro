using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFinanceiro.Models
{
    [Table("despesas"), PrimaryKey(nameof(Id))]
    public class Despesa
    {
        [Column("id_despesa")]
        public int Id { get; set; }

        [Column("descricao_des")]
        public required string Descricao { get; set; }

        [Column("categoria_des")]
        public required string Categoria { get; set; }

        [Column("valor_des")]
        public required decimal Valor { get; set; }

        [Column("data_vencimento_des")]
        public required DateOnly DataVencimento { get; set; }

        [Column("situacao_des")]
        public required string Situacao { get; set; }

        [Column("data_pagamento_des")]
        public DateTime? DataPagamento { get; set; }
    }
}
