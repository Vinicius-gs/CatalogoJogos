using CatalagoJogos.InputModel;
using CatalagoJogos.Services;
using CatalagoJogos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CatalagoJogos.Exception;


namespace CatalagoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]

    public class JogosController : ControllerBase
    {
        public readonly IJogoService _jogoService;

        public JogosController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        /// <summary>
        /// Buscar todos os jogos de froma paginada
        /// </summary>
        /// <remarks>
        /// Não é possivel retornar os jogos sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de registro por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>
        /// <returns></returns>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _jogoService.Obter(pagina, quantidade);

            if (jogos.Count() == 0)
                return NoContent();
            return Ok(jogos);
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            var jogos = await _jogoService.Obter(idJogo);

            if (jogos == null)
                return NoContent();
            return Ok(jogos);

        }

        [HttpPost]

        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogos = await _jogoService.Inserir(jogoInputModel);
                return Ok(jogos);
            }
            catch (JogonaCadastradoExcepiton ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }

        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, jogoInputModel);
                return Ok();
            }
            catch (JogonaCadastradoExcepiton ex)
            {
                return NotFound("Não existe esse jogo");
            }

        }

        [HttpPatch("{idJogo:guid/preco/{preco:double}")]
        public async Task<ActionResult<JogoViewModel>> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute]  double preco)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, preco);
                return Ok();
            }
            catch (JogonaCadastradoExcepiton ex)
            {
                return NotFound("Não existe esse jogo");
            }
        }
        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> ApagarJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoService.Remover(idJogo);
                return Ok();
            }
            catch (JogonaCadastradoExcepiton ex)
            {
                return NotFound("Não existe esse jogo");
            }
        }
    }
}
