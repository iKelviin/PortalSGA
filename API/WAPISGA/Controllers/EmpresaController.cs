using Entity.Common;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using WAPISGA.Repositories;

namespace WAPISGA.Controllers
{
    /// <summary>
    /// Controller responsavel pelas requisicoes de dados de Empresa.
    /// </summary>
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        static readonly EmpresaRepository repEmpresa = new EmpresaRepository();

        /// <summary>
        /// Grava dados de uma nova empresa ou atualiza uma ja existente.
        /// </summary>
        /// <param name="pEmpresa">Dados da empresa.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("api/empresas")]
        public IActionResult Gravar([FromBody] EmpresaInfo pEmpresa)
        {
            RetornoPostInfo resultado = new RetornoPostInfo();
            try
            {
                resultado = repEmpresa.Gravar(pEmpresa);

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
                resultado.Mensagem = string.Concat("Erro ao salvar dados da empresa: ", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, resultado);
            }
        }

        /// <summary>
        /// Retorna uma lista de empresas cadastradas.
        /// </summary>
        /// <returns>Lista de empresas.</returns>]
        [AllowAnonymous]
        [HttpGet]
        [Route("api/empresas")]
        public List<EmpresaInfo> SelecionarLista()
        {
            return repEmpresa.SelecionarLista();
        }

        /// <summary>
        /// Retorna uma lista de empresas cadastradas filtrando pelo nome.
        /// </summary>
        /// <param name="nome">Nome da empresa.</param>
        /// <returns>Lista de empresas filtrada pelo nome.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/empresas/por-nome/{nome}")]
        public List<EmpresaInfo> SelecionarListaPorNome(string nome)
        {
            return repEmpresa.SelecionarListaPorNome(nome);
        }

        /// <summary>
        /// Retorna uma empresa de acordo com o ID informado.
        /// </summary>
        /// <param name="id">ID da empresa.</param>
        /// <returns>Dados da empresa.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/empresas/{id}")]
        public EmpresaInfo Selecionar(int id)
        {
            return repEmpresa.Selecionar(id);
        }


        /// <summary>
        /// Exclui uma empresa existente.
        /// </summary>
        /// <param name="id">Id da empresa.</param>
        /// <returns>Resultado da operacao de exclusão.</returns>
        [AllowAnonymous]
        [HttpDelete]
        [Route("api/empresas/{id}")]
        public IActionResult Excluir(int id)
        {
            RetornoPostInfo resultado = new RetornoPostInfo();
            try
            {
                resultado = repEmpresa.Excluir(id);

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
                resultado.Mensagem = string.Concat("Erro ao excluir a empresa: ", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, resultado);
            }
        }
    }
}
