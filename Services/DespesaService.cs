using ApiFinanceiro.DataContexts;
using ApiFinanceiro.Dtos;
using ApiFinanceiro.Exceptions;
using ApiFinanceiro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanceiro.Services
{
    public class DespesaService
    {
        private readonly AppDbContext _context;

        public DespesaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Despesa>> FindAll()
        {
            try
            {
                return await _context.Despesas.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar despesas: {ex.Message}");
            }
        }

        public async Task<Despesa> Create(DespesaDto novaDespesa)
        {
            try
            {
                var despesa = new Despesa
                {
                    Descricao = novaDespesa.Descricao,
                    Categoria = novaDespesa.Categoria,
                    Valor = novaDespesa.Valor,
                    DataVencimento = novaDespesa.DataVencimento,
                    Situacao = "Pendente"
                };

                await _context.Despesas.AddAsync(despesa);
                await _context.SaveChangesAsync();

                return despesa;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Despesa> FindById(int id)
        {
            try
            {
                var despesa = await _context.Despesas.FirstOrDefaultAsync(x => x.Id == id);

                if (despesa is null)
                {
                    throw new ErrorServiceException($"Despesa com id {id} não encontrada.", c => c.NotFound(new { message = $"Despesa com id {id} não encontrada." }));
                }
                return despesa;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Despesa> Update(int id, DespesaUpdateDto despesaDto)
        {
            try
            {
                var despesa = await FindById(id);

                var dataVencimento = new DateOnly(despesaDto.DataVencimento.Year, despesaDto.DataVencimento.Month, despesaDto.DataVencimento.Day);
                var dataPagamento = new DateTime(despesaDto.DataPagemnto.Year, despesaDto.DataPagemnto.Month, despesaDto.DataPagemnto.Day);

                if (dataPagamento < dataVencimento.ToDateTime(new TimeOnly(0, 0)))
                {
                    throw new ErrorServiceException("", c => c.Conflict(new { message = $"A data de pagamento não pode ser anterior à data de vencimento." }));
                }

                despesa.Descricao = despesaDto.Descricao;
                despesa.Categoria = despesaDto.Categoria;
                despesa.Valor = despesaDto.Valor;
                despesa.DataVencimento = despesaDto.DataVencimento;
                despesa.Situacao = despesaDto.Situacao;
                despesa.DataPagamento = despesaDto.DataPagemnto;

                

                _context.Despesas.Update(despesa);
                await _context.SaveChangesAsync();

                return despesa;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Remove(int id)
        {
            try
            {
                var despesa = await FindById(id);

                _context.Despesas.Remove(despesa);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
