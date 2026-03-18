using Microsoft.AspNetCore.Mvc;
using ApiFinanceiro.Models;
using ApiFinanceiro.Dtos;
using ApiFinanceiro.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanceiro.Controllers
{
    [Route("/despesas")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DespesaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                var despesas = await _context.Despesas.ToListAsync();
                return Ok(despesas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Ocorreu um erro ao buscar as despesas.", detalhes = ex.Message });
            }
        }

        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] DespesaDto novaDespesa)
        {
            try
            {
                var despesa = new Despesa
                {
                    Descricao = novaDespesa.Descricao,
                    Valor = novaDespesa.Valor,
                    Categoria = novaDespesa.Categoria,
                    DataVencimento = novaDespesa.DataVencimento,
                    Situacao = "Pendente"
                };

                await _context.Despesas.AddAsync(despesa);
                await _context.SaveChangesAsync();

                return Created("", despesa);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> findById(int id)
        {
            try
            {
                var despesaById = await _context.Despesas.FirstOrDefaultAsync(d => d.Id == id);
                if (despesaById is null)
                {
                    return NotFound(new { mensagem = $"Despesa com id {id} não encontrada." });
                }
                return Ok(despesaById);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] DespesaUpdateDto despesaAtualizada)
        {
            try
            {
                var despesa = await _context.Despesas.FirstOrDefaultAsync(d => d.Id == id);
                if (despesa is null)
                {
                    return NotFound(new { mensagem = $"Despesa com id {id} não encontrada." });
                }

                despesa.Descricao = despesaAtualizada.Descricao;
                despesa.Categoria = despesaAtualizada.Categoria;
                despesa.Valor = despesaAtualizada.Valor;
                despesa.DataVencimento = despesaAtualizada.DataVencimento;
                despesa.Situacao = despesaAtualizada.Situacao;
                despesa.DataPagamento = despesaAtualizada.DataPagemnto;
                _context.Despesas.Update(despesa);
                await _context.SaveChangesAsync();
                return Ok(despesa);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var despesa = await _context.Despesas.FirstOrDefaultAsync(d => d.Id == id);
                if (despesa is null)
                {
                    return NotFound(new { mensagem = $"Despesa com id {id} não encontrada." });
                }
                _context.Despesas.Remove(despesa);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //private static List<Despesa> listaDespesas = new()
        //{
        //    new Despesa {
        //        Descricao = "Conta de Luz",
        //        Categoria = "Utilidades",
        //        Valor = 200,
        //        DataVencimento = new DateOnly(2024, 7, 10),
        //        Situacao = "Pendente"
        //    },
        //    new Despesa {
        //        Descricao = "Aluguel",
        //        Categoria = "Moradia",
        //        Valor = 1500,
        //        DataVencimento = new DateOnly(2024, 7, 5),
        //        Situacao = "Pendente"
        //    },
        //    new Despesa {
        //        Descricao = "Internet",
        //        Categoria = "Utilidades",
        //        Valor = 100,
        //        DataVencimento = new DateOnly(2024, 7, 15),
        //        Situacao = "Pendente"
        //    }
        //};

        //[HttpGet()]
        //public ActionResult FindAll()
        //{
        //    return Ok(listaDespesas);
        //}

        //[HttpPost()]
        //public ActionResult Create([FromBody] DespesaDto novaDespesa)
        //{
        //    var despesa = new Despesa
        //    {
        //        Descricao = novaDespesa.Descricao,
        //        Categoria = novaDespesa.Categoria,
        //        Valor = novaDespesa.Valor,
        //        DataVencimento = novaDespesa.DataVencimento,
        //        Situacao = "Pendente"
        //    };

        //    listaDespesas.Add(despesa);

        //    return Created("", despesa);
        //}

        //[HttpGet("{id}")]
        //public ActionResult findById(Guid id)
        //{
        //    var despesaById = listaDespesas.FirstOrDefault(d => d.Id == id);
        //    if (despesaById is null)
        //    {
        //        return NotFound(new { mensagem = $"Despesa com id {id} não encontrada." });
        //    }
        //    return Ok(despesaById);
        //}

        //[HttpPut("{id}")]
        //public ActionResult Update(Guid id, [FromBody] DespesaUpdateDto despesaAtualizada)
        //{
        //    var despesa = listaDespesas.FirstOrDefault(d => d.Id == id);
        //    if (despesa is null)
        //    {
        //        return NotFound(new { mensagem = $"Despesa com id {id} não encontrada." });
        //    }

        //    despesa.Descricao = despesaAtualizada.Descricao;
        //    despesa.Categoria = despesaAtualizada.Categoria;
        //    despesa.Valor = despesaAtualizada.Valor;
        //    despesa.DataVencimento = despesaAtualizada.DataVencimento;
        //    despesa.Situacao = despesaAtualizada.Situacao;
        //    despesa.DataPagamento = despesaAtualizada.DataPagemnto;

        //    return Ok(despesa);
        //}

        //[HttpDelete("{id}")]
        //public ActionResult Delete(Guid id)
        //{
        //    var despesa = listaDespesas.FirstOrDefault(d => d.Id == id);
        //    if (despesa is null)
        //    {
        //        return NotFound(new { mensagem = $"Despesa com id {id} não encontrada." });
        //    }

        //    listaDespesas.Remove(despesa);
        //    return NoContent();
        //}
    }
}