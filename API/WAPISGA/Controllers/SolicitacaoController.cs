using Entity.Common;
using Entity;
using Entity.Solicitacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WAPISGA.Repositories;

namespace WAPISGA.Controllers
{
    /// <summary>
    /// Controller responsavel pelas requisicoes de dados de Solicitações.
    /// </summary>
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [ApiController]    
    public class SolicitacaoController : ControllerBase
    {
        static readonly SolicitacaoRepository repSolic = new SolicitacaoRepository();

        /// <summary>
        /// Retorna uma lista de Solicitações cadastradas.
        /// </summary>
        /// <returns>Lista de Solicitações.</returns>]
        [AllowAnonymous]
        [HttpGet]
        [Route("api/solicitacoes")]
        public List<SolicitacaoDTOInfo> SelecionarLista()
        {
            return repSolic.SelecionarLista();
        }

        /// <summary>
        /// Retorna uma Solicitação pelo Id.
        /// </summary>
        /// <param name="id">Id da Solicitação.</param>
        /// <returns>Dados da Solicitação.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/solicitacoes/{id}")]
        public SolicitacaoDTOInfo Selecionar(int id)
        {
            return repSolic.Selecionar(id);
        }

        /// <summary>
        /// Grava dados de uma nova solicitação ou atualiza uma ja existente.
        /// </summary>
        /// <param name="pSolicitacao">Dados da Solicitação.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("api/solicitacoes")]
        public IActionResult Gravar([FromBody] SolicitacaoDTOInfo pSolicitacao)
        {
            RetornoPostInfo resultado = new RetornoPostInfo();
            try
            {
                resultado = repSolic.Gravar(pSolicitacao);

                if (resultado.Mensagem.Contains("Erro"))
                {
                    return BadRequest(resultado);
                }
                else
                {
                    return new OkObjectResult(resultado);
                }
            }
            catch (Exception ex)
            {
                resultado.Mensagem = string.Concat("Erro ao salvar dados da solicitação: ", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, resultado);
            }
        }
    }
}
