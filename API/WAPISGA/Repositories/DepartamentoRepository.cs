using Entity.Common;
using Entity;
using Facade;

namespace WAPISGA.Repositories
{

    /// <summary>
    /// Repositorio para acesso aos dados do departamento.
    /// </summary>
    public class DepartamentoRepository
    {

        /// <summary>
        /// Grava dados de um novo departamento ou atualiza um ja existente.
        /// </summary>
        /// <param name="pIdEmpresa">Id da empresa.</param>
        /// <param name="pDepartamento">Dados do departamento.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        public RetornoPostInfo Gravar(int pIdEmpresa,DepartamentoInfo pDepartamento)
        {
            return DepartamentoFacade.Gravar(pIdEmpresa,pDepartamento);
        }

        /// <summary>
        /// Exclui um departamento de acordo com o Id informado.
        /// </summary>
        /// <param name="pId">Dados do departamento.</param>
        /// <returns>Exclui um departamento.</returns>
        public RetornoPostInfo Excluir(int pId)
        {
            return DepartamentoFacade.Excluir(pId);
        }

        /// <summary>
        /// Retorna uma lista de departamentos cadastrados.
        /// </summary>
        /// <returns>Lista de departamentos.</returns>
        public List<DepartamentoInfo> SelecionarLista()
        {
            return DepartamentoFacade.SelecionarLista();
        }

        /// <summary>
        /// Retorna uma lista de departamentos cadastrados para uma determinada empresa.
        /// </summary>
        /// <param name="pIdEmpresa">Id da empresa.</param>
        /// <returns>Lista de departamentos por empresa.</returns>
        public List<DepartamentoInfo> SelecionarLista(int pIdEmpresa)
        {
            return DepartamentoFacade.SelecionarLista(pIdEmpresa);
        }

        /// <summary>
        /// Retorna uma lista de departamentos cadastrados filtrando pelo nome.
        /// </summary>
        /// <param name="pNome">Nome do departamento.</param>
        /// <returns>Lista de departamentos filtrados pelo nome.</returns>
        public List<DepartamentoInfo> SelecionarListaPorNome(string pNome)
        {
            return DepartamentoFacade.SelecionarListaPorNome(pNome);
        }

        /// <summary>
        /// Retorna um departamento de acordo com o Id informado.
        /// </summary>
        /// <param name="pId">ID do departamento.</param>
        /// <returns>Dados do departamento.</returns>
        public DepartamentoInfo Selecionar(int pId)
        {
            return DepartamentoFacade.Selecionar(pId);
        }

    }
}
