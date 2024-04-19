using Entity.Common;
using Entity;
using BUS.Departamento;

namespace Facade.Departamento
{
    public static class DepartamentoFacade
    {
        /// <summary>
        /// Gravar dados de um novo departamento ou atualiza um ja existente.
        /// </summary>
        /// <param name="pIdEmpresa">Id da empresa.</param>
        /// <param name="pDepartamento">Dados do departamento.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        public static RetornoPostInfo Gravar(int pIdEmpresa,DepartamentoInfo pDepartamento)
        {
            DepartamentoBUS busDepartamento = new DepartamentoBUS();
            return busDepartamento.Gravar(pIdEmpresa,pDepartamento);
        }

        /// <summary>
        /// Exclui um departamento de acordo com o Id informado.
        /// </summary>
        /// <param name="pDepartamento">Dados do departamento.</param>
        /// <returns>Exclui um departamento.</returns>
        public static RetornoPostInfo Excluir(int pId)
        {
            DepartamentoBUS busDepartamento = new DepartamentoBUS();
            return busDepartamento.Excluir(pId);
        }

        /// <summary>
        /// Seleciona uma lista de informações dos departamentos disponíveis.
        /// </summary>
        /// <returns>sta contendo informações dos departamentos.</returns>
        public static List<DepartamentoInfo> SelecionarLista()
        {
            DepartamentoBUS busDepartamento = new DepartamentoBUS();
            return busDepartamento.SelecionarLista();
        }

        /// <summary>
        /// Retorna uma lista de departamentos cadastrados para uma determinada empresa.
        /// </summary>
        /// <param name="pIdEmpresa">Id da empresa.</param>
        /// <returns>Lista de departamentos por empresa.</returns>
        public static List<DepartamentoInfo> SelecionarLista(int pIdEmpresa)
        {
            DepartamentoBUS busDepartamento = new DepartamentoBUS();
            return busDepartamento.SelecionarLista(pIdEmpresa);
        }

        /// <summary>
        /// Retorna uma lista de departamentos cadastrados filtrando pelo nome.
        /// </summary>
        /// <param name="pNome">Nome do departamento.</param>
        /// <returns>Lista de departamentos filtrados pelo nome.</returns>
        public static List<DepartamentoInfo> SelecionarListaPorNome(string pNome)
        {
            DepartamentoBUS busDepartamento = new DepartamentoBUS();
            return busDepartamento.SelecionarListaPorNome(pNome);
        }

        /// <summary>
        /// Retorna um departamento de acordo com o ID informado.
        /// </summary>
        /// <param name="pId">Id do departamento.</param>
        /// <returns>Dados do departamento.</returns>
        public static DepartamentoInfo Selecionar(int pId)
        {
            DepartamentoBUS busDepartamento = new DepartamentoBUS();
            return busDepartamento.Selecionar(pId);
        }
    }
}
