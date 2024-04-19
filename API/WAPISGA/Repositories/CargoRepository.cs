using Entity.Common;
using Entity;
using Facade.Cargo;

namespace WAPISGA.Repositories
{
    
    /// <summary>
    /// Repositorio para acesso aos dados do cargo.
    /// </summary>
    public class CargoRepository
    {

        /// <summary>
        /// Grava dados de um novo cargo ou atualiza um ja existente.
        /// </summary>
        /// <param name="pIdDepartamento">Id do departamento.</param>
        /// <param name="pCargo">Dados do cargo.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        public RetornoPostInfo Gravar(int pIdDepartamento, CargoInfo pCargo)
        {
            return CargoFacade.Gravar(pIdDepartamento, pCargo);
        }

        /// <summary>
        /// Exclui um cargo de acordo com o Id informado.
        /// </summary>
        /// <param name="pId">Dados do cargo.</param>
        /// <returns>Exclui um cargo.</returns>
        public RetornoPostInfo Excluir(int pId)
        {
            return CargoFacade.Excluir(pId);
        }

        /// <summary>
        /// Retorna uma lista de cargos cadastrados.
        /// </summary>
        /// <returns>Lista de cargos.</returns>
        public List<CargoInfo> SelecionarLista()
        {
            return CargoFacade.SelecionarLista();
        }

        /// <summary>
        /// Retorna uma lista de cargos cadastrados para um determinado departamento.
        /// </summary>
        /// <param name="pIdDepartamento">Id do departamento.</param>
        /// <returns>Lista de cargos por departamento.</returns>
        public List<CargoInfo> SelecionarLista(int pIdDepartamento)
        {
            return CargoFacade.SelecionarLista(pIdDepartamento);
        }

        /// <summary>
        /// Retorna uma lista de cargos cadastrados filtrando pelo nome.
        /// </summary>
        /// <param name="pNome">Nome do cargo.</param>
        /// <returns>Lista de cargos filtrados pelo nome.</returns>
        public List<CargoInfo> SelecionarListaPorNome(string pNome)
        {
            return CargoFacade.SelecionarListaPorNome(pNome);
        }

        /// <summary>
        /// Retorna um cargo de acordo com o Id informado.
        /// </summary>
        /// <param name="pId">ID do cargo.</param>
        /// <returns>Dados do cargo.</returns>
        public CargoInfo Selecionar(int pId)
        {
            return CargoFacade.Selecionar(pId);
        }

    }
}
