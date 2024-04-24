using Entity.Common;
using Entity;
using BUS.Cargo;

namespace Facade.Cargo
{
    public static class CargoFacade
    {
        /// <summary>
        /// Gravar dados de um novo cargo ou atualiza um ja existente.
        /// </summary>
        /// <param name="pIdDepartamento">Id da empresa.</param>
        /// <param name="pCargo">Dados do cargo.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        public static RetornoPostInfo Gravar(int pIdDepartamento, CargoInfo pCargo)
        {
            CargoBUS busCargo = new CargoBUS();
            return busCargo.Gravar(pIdDepartamento, pCargo);
        }

        /// <summary>
        /// Exclui um cargo de acordo com o Id informado.
        /// </summary>
        /// <param name="pCargo">Dados do cargo.</param>
        /// <returns>Exclui um cargo.</returns>
        public static RetornoPostInfo Excluir(int pId)
        {
            CargoBUS busCargo = new CargoBUS();
            return busCargo.Excluir(pId);
        }

        /// <summary>
        /// Seleciona uma lista de informações dos cargos disponíveis.
        /// </summary>
        /// <returns>sta contendo informações dos cargos.</returns>
        public static List<CargoInfo> SelecionarLista()
        {
            CargoBUS busCargo = new CargoBUS();
            return busCargo.SelecionarLista();
        }

        /// <summary>
        /// Retorna uma lista de cargos cadastrados para uma determinada empresa.
        /// </summary>
        /// <param name="pIdDepartamento">Id da empresa.</param>
        /// <returns>Lista de cargos por empresa.</returns>
        public static List<CargoInfo> SelecionarLista(int pIdDepartamento)
        {
            CargoBUS busCargo = new CargoBUS();
            return busCargo.SelecionarLista(pIdDepartamento);
        }

        /// <summary>
        /// Retorna uma lista de cargos cadastrados filtrando pelo nome.
        /// </summary>
        /// <param name="pNome">Nome do cargo.</param>
        /// <returns>Lista de cargos filtrados pelo nome.</returns>
        public static List<CargoInfo> SelecionarListaPorNome(string pNome)
        {
            CargoBUS busCargo = new CargoBUS();
            return busCargo.SelecionarListaPorNome(pNome);
        }

        /// <summary>
        /// Retorna um cargo de acordo com o ID informado.
        /// </summary>
        /// <param name="pId">Id do cargo.</param>
        /// <returns>Dados do cargo.</returns>
        public static CargoInfo Selecionar(int pId)
        {
            CargoBUS busCargo = new CargoBUS();
            return busCargo.Selecionar(pId);
        }
    }
}
