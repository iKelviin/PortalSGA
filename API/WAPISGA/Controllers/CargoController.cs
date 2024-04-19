using Entity.Common;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WAPISGA.Repositories;

namespace WAPISGA.Controllers
{    
    /// <summary>
    /// Controller responsavel pelas requisicoes de dados de Departamento.
    /// </summary>
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class CargoController : ControllerBase
    {

        static readonly CargoRepository repCargo = new CargoRepository();

        /// <summary>
        /// Grava dados de um novo cargo ou atualiza um ja existente.
        /// </summary>
        /// <param name="pIdDepartamento">Id do departamento.</param>
        /// <param name="pCargo">Dados do cargo.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("api/empresas/departamentos/{pIdDepartamento}/cargos")]
        public IActionResult Gravar(int pIdDepartamento, [FromBody] CargoInfo pCargo)
        {
            RetornoPostInfo resultado = new RetornoPostInfo();
            try
            {
                resultado = repCargo.Gravar(pIdDepartamento, pCargo);

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
                resultado.Mensagem = string.Concat("Erro ao salvar dados do cargo: ", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, resultado);
            }
        }

        /// <summary>
        /// Retorna uma lista de cargos cadastrados.
        /// </summary>
        /// <returns>Lista de cargos.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/empresas/departamentos/cargos")]
        public List<CargoInfo> SelecionarLista()
        {
            return repCargo.SelecionarLista();
        }

        /// <summary>
        /// Retorna uma lista de cargos cadastrados para um determinad departamento.
        /// </summary>
        /// <param name="pIdDepartamento">Id do departamento.</param>
        /// <returns>Lista de cargos por departamento.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/empresas/departamentos/{pIdDepartamento}/cargos")]
        public List<CargoInfo> SelecionarListaPorDepartamento(int pIdDepartamento)
        {
            return repCargo.SelecionarLista(pIdDepartamento);
        }

        /// <summary>
        /// Retorna uma lista de cargos cadastrados filtrando pelo nome.
        /// </summary>
        /// <param name="pNome">Nome do cargo.</param>
        /// <returns>Lista de cargos filtrados pelo nome.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/empresas/departamentos/cargos/por-nome/{pNome}")]
        public List<CargoInfo> SelecionarListaPorNome(string pNome)
        {
            return repCargo.SelecionarListaPorNome(pNome);
        }

        /// <summary>
        /// Retorna um cargo de acordo com o ID informado.
        /// </summary>
        /// <param name="pId">ID do cargo.</param>
        /// <returns>Dados do cargo.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/empresas/departamentos/cargos/{pId}")]
        public CargoInfo Selecionar(int pId)
        {
            return repCargo.Selecionar(pId);
        }


        /// <summary>
        /// Exclui um cargo existente.
        /// </summary>
        /// <param name="pId">Id do cargo.</param>
        /// <returns>Resultado da operacao de exclusão.</returns>
        [AllowAnonymous]
        [HttpDelete]
        [Route("api/empresas/departamentos/cargos/{pId}")]
        public IActionResult Excluir(int pId)
        {
            RetornoPostInfo resultado = new RetornoPostInfo();
            try
            {
                resultado = repCargo.Excluir(pId);

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
                resultado.Mensagem = string.Concat("Erro ao excluir o cargo: ", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, resultado);
            }
        }
    }
}
