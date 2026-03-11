using ApiFinanceiro.Dtos;
using ApiFinanceiro.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanceiro.Controllers
{
    [Route("/receitas")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        private static readonly List<Receita> listaReceitas = new()
        {
            new Receita
            {
                Descricao = "Salario",
                Valor = 5000,
                DataPrevisao = new DateOnly(2026, 3, 5),
                DataRecebimento = new DateOnly(2026, 3, 5),
                Categoria = "Trabalho",
                Observacao = "Credito em conta corrente",
                Situacao = "Recebido"
            },
            new Receita
            {
                Descricao = "Freelance de site",
                Valor = 1200,
                DataPrevisao = new DateOnly(2026, 3, 20),
                Categoria = "Freelance",
                Observacao = "Projeto web",
                Situacao = "Pendente"
            }
        };

        [HttpGet]
        public ActionResult FindAll()
        {
            return Ok(listaReceitas);
        }

        [HttpGet("{id}")]
        public ActionResult FindById(Guid id)
        {
            var receitaById = listaReceitas.FirstOrDefault(receita => receita.Id == id);
            if (receitaById is null)
            {
                return NotFound(new { mensagem = $"Receita com id {id} nao encontrada." });
            }

            return Ok(receitaById);
        }

        [HttpPost]
        public ActionResult Create([FromBody] ReceitaDto novaReceita)
        {
            var situacaoNormalizada = NormalizarSituacao(novaReceita.Situacao);
            if (situacaoNormalizada is null)
            {
                return BadRequest(new { mensagem = "Situacao invalida. Use apenas 'Pendente' ou 'Recebido'." });
            }

            if (!TryValidarSituacao(situacaoNormalizada, novaReceita.DataRecebimento, out var mensagem))
            {
                return BadRequest(new { mensagem });
            }

            var receita = new Receita
            {
                Descricao = novaReceita.Descricao,
                Valor = novaReceita.Valor,
                DataPrevisao = novaReceita.DataPrevisao,
                DataRecebimento = novaReceita.DataRecebimento,
                Categoria = novaReceita.Categoria,
                Observacao = novaReceita.Observacao,
                Situacao = situacaoNormalizada
            };

            listaReceitas.Add(receita);
            return Created($"/receitas/{receita.Id}", receita);
        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, [FromBody] ReceitaUpdateDto receitaAtualizada)
        {
            var receita = listaReceitas.FirstOrDefault(item => item.Id == id);
            if (receita is null)
            {
                return NotFound(new { mensagem = $"Receita com id {id} nao encontrada." });
            }

            var situacaoNormalizada = NormalizarSituacao(receitaAtualizada.Situacao);
            if (situacaoNormalizada is null)
            {
                return BadRequest(new { mensagem = "Situacao invalida. Use apenas 'Pendente' ou 'Recebido'." });
            }

            if (!TryValidarSituacao(situacaoNormalizada, receitaAtualizada.DataRecebimento, out var mensagem))
            {
                return BadRequest(new { mensagem });
            }

            receita.Descricao = receitaAtualizada.Descricao;
            receita.Valor = receitaAtualizada.Valor;
            receita.DataPrevisao = receitaAtualizada.DataPrevisao;
            receita.DataRecebimento = receitaAtualizada.DataRecebimento;
            receita.Categoria = receitaAtualizada.Categoria;
            receita.Observacao = receitaAtualizada.Observacao;
            receita.Situacao = situacaoNormalizada;

            return Ok(receita);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var receita = listaReceitas.FirstOrDefault(item => item.Id == id);
            if (receita is null)
            {
                return NotFound(new { mensagem = $"Receita com id {id} nao encontrada." });
            }

            listaReceitas.Remove(receita);
            return NoContent();
        }

        private static string? NormalizarSituacao(string? situacao)
        {
            if (string.IsNullOrWhiteSpace(situacao))
            {
                return null;
            }

            return situacao.Trim().ToLowerInvariant() switch
            {
                "pendente" => "Pendente",
                "recebido" => "Recebido",
                _ => null
            };
        }

        private static bool TryValidarSituacao(string situacao, DateOnly? dataRecebimento, out string mensagem)
        {
            if (situacao == "Recebido" && dataRecebimento is null)
            {
                mensagem = "DataRecebimento e obrigatoria quando a situacao for 'Recebido'.";
                return false;
            }

            if (situacao == "Pendente" && dataRecebimento is not null)
            {
                mensagem = "DataRecebimento deve ser nula quando a situacao for 'Pendente'.";
                return false;
            }

            mensagem = string.Empty;
            return true;
        }
    }
}
