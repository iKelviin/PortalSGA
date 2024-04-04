using Entity.Common;
using Entity;
using Facade.Empresa;
using BUS.Empresa;

namespace WAPISGA.Repositories
{
    /// <summary>
    /// Repositorio para acesso aos dados da Empresa.
    /// </summary>
    public class EmpresaRepository
    {
        /// <summary>
        /// Grava dados de uma nova empresa ou atualiza uma ja existente.
        /// </summary>
        /// <param name="pEmpresa">Dados da empresa.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        public RetornoPostInfo Gravar(EmpresaInfo pEmpresa)
        {
            return EmpresaFacade.Gravar(pEmpresa);
        }

        /// <summary>
        /// Exclui uma empresa de acordo com o ID informado.
        /// </summary>
        /// <param name="pEmpresa">Dados da empresa.</param>
        /// <returns>Exclui uma empresa.</returns>
        public RetornoPostInfo Excluir(int pId)
        {
            return EmpresaFacade.Excluir(pId);
        }

        /// <summary>
        /// Retorna uma lista de empresas cadastradas.
        /// </summary>
        /// <returns>Lista de empresas.</returns>
        public List<EmpresaInfo> SelecionarLista()
        {
            return EmpresaFacade.SelecionarLista();
        }

        /// <summary>
        /// Retorna uma lista de empresas cadastradas filtrando pelo nome.
        /// </summary>
        /// <param name="nome">Nome da empresa.</param>
        /// <returns>Lista de empresas filtrada pelo nome.</returns>
        public List<EmpresaInfo> SelecionarListaPorNome(string pNome)
        {
            return EmpresaFacade.SelecionarListaPorNome(pNome);
        }

        /// <summary>
        /// Retorna uma empresa de acordo com o ID informado.
        /// </summary>
        /// <param name="pId">ID da empresa.</param>
        /// <returns>Dados da empresa.</returns>
        public EmpresaInfo Selecionar(int pId)
        {
            return EmpresaFacade.Selecionar(pId);
        }
    }
}
