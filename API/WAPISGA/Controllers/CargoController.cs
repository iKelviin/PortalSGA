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
        /// <param name="id">Id do departamento.</param>
        /// <param name="pCargo">Dados do cargo.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("api/empresas/departamentos/{id}/cargos")]
        public IActionResult Gravar(int id, [FromBody] CargoInfo pCargo)
        {
            RetornoPostInfo resultado = new RetornoPostInfo();
            try
            {
                resultado = repCargo.Gravar(id, pCargo);

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
        /// <param name="id">Id do departamento.</param>
        /// <returns>Lista de cargos por departamento.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/empresas/departamentos/{id}/cargos")]
        public List<CargoInfo> SelecionarListaPorDepartamento(int id)
        {
            return repCargo.SelecionarLista(id);
        }

        /// <summary>
        /// Retorna uma lista de cargos cadastrados filtrando pelo nome.
        /// </summary>
        /// <param name="nome">Nome do cargo.</param>
        /// <returns>Lista de cargos filtrados pelo nome.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/empresas/departamentos/cargos/por-nome/{nome}")]
        public List<CargoInfo> SelecionarListaPorNome(string nome)
        {
            return repCargo.SelecionarListaPorNome(nome);
        }

        /// <summary>
        /// Retorna um cargo de acordo com o ID informado.
        /// </summary>
        /// <param name="id">ID do cargo.</param>
        /// <returns>Dados do cargo.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("api/empresas/departamentos/cargos/{id}")]
        public CargoInfo Selecionar(int id)
        {
            return repCargo.Selecionar(id);
        }


        /// <summary>
        /// Exclui um cargo existente.
        /// </summary>
        /// <param name="id">Id do cargo.</param>
        /// <returns>Resultado da operacao de exclusão.</returns>
        [AllowAnonymous]
        [HttpDelete]
        [Route("api/empresas/departamentos/cargos/{id}")]
        public IActionResult Excluir(int id)
        {
            RetornoPostInfo resultado = new RetornoPostInfo();
            try
            {
                resultado = repCargo.Excluir(id);

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
