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
        public List<SolicitacaoInfo> SelecionarLista()
        {
            return repSolic.SelecionarLista();
        }
    }
}
