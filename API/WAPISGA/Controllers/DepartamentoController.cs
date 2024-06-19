using Entity.Common;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WAPISGA.Repositories;
using Microsoft.AspNetCore.Cors;

namespace WAPISGA.Controllers
{
    /// <summary>
    /// Controller responsavel pelas requisicoes de dados de Departamento.
    /// </summary>
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {

        static readonly DepartamentoRepository repDepartamento = new DepartamentoRepository();

        /// <summary>
        /// Grava dados de um nov departamento ou atualiza um ja existente.
        /// </summary>
        /// <param name="id">Id da empresa.</param>
        /// <param name="pDepartamento">Dados do departamento.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("api/empresas/{id}/departamentos")]
        public IActionResult Gravar(int id, [FromBody] DepartamentoInfo pDepartamento)
        {
            RetornoPostInfo resultado = new RetornoPostInfo();
            try
            {
                resultado = repDepartamento.Gravar(id,pDepartamento);

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
                resultado.Mensagem = string.Concat("Erro ao salvar dados do departamento: ", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, resultado);
            }
        }

        /// <summary>
        /// Retorna uma lista de departamentos cadastrados.
        /// </summary>
        /// <returns>Lista de departamentos.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/empresas/departamentos")]
        public List<DepartamentoInfo> SelecionarLista()
        {
            return repDepartamento.SelecionarLista();
        }

        /// <summary>
        /// Retorna uma lista de departamentos cadastrados para uma determinada empresa.
        /// </summary>
        /// <param name="id">Id da empresa.</param>
        /// <returns>Lista de departamentos por empresa.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/empresas/{id}/departamentos")]
        public List<DepartamentoInfo> SelecionarListaPorEmpresa(int id)
        {
            return repDepartamento.SelecionarLista(id);
        }

        /// <summary>
        /// Retorna uma lista de departamentos cadastrados filtrando pelo nome.
        /// </summary>
        /// <param name="nome">Nome do departamento.</param>
        /// <returns>Lista de departamentos filtrados pelo nome.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/empresas/departamentos/por-nome/{nome}")]
        public List<DepartamentoInfo> SelecionarListaPorNome(string nome)
        {
            return repDepartamento.SelecionarListaPorNome(nome);
        }

        /// <summary>
        /// Retorna um departamento de acordo com o ID informado.
        /// </summary>
        /// <param name="id">ID do departamento.</param>
        /// <returns>Dados do departamento.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/empresas/departamentos/{id}")]
        public DepartamentoInfo Selecionar(int id)
        {
            return repDepartamento.Selecionar(id);
        }


        /// <summary>
        /// Exclui um departamento existente.
        /// </summary>
        /// <param name="id">Id do departamento.</param>
        /// <returns>Resultado da operacao de exclusão.</returns>
        [AllowAnonymous]
        [HttpDelete]
        [Route("api/empresas/departamentos/{id}")]
        public IActionResult Excluir(int id)
        {
            RetornoPostInfo resultado = new RetornoPostInfo();
            try
            {
                resultado = repDepartamento.Excluir(id);

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
                resultado.Mensagem = string.Concat("Erro ao excluir o departamento: ", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, resultado);
            }
        }
    }
}
