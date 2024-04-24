using BUS.Empresa;
using Entity.Common;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Empresa;
using static Common.Enumeracoes;

namespace Facade.Empresa
{
    public static class EmpresaFacade
    {
        /// <summary>
        /// Gravar dados de uma nova empresa ou atualiza uma ja existente.
        /// </summary>
        /// <param name="pEmpresa">Dados da empresa.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        public static RetornoPostInfo Gravar(EmpresaInfo pEmpresa)
        {
            EmpresaBUS busEmpresa = new EmpresaBUS();
            return busEmpresa.Gravar(pEmpresa);
        }

        /// <summary>
        /// Exclui uma empresa de acordo com o ID informado.
        /// </summary>
        /// <param name="pEmpresa">Dados da empresa.</param>
        /// <returns>Exclui uma empresa.</returns>
        public static RetornoPostInfo Excluir(int pId)
        {
            EmpresaBUS busEmpresa = new EmpresaBUS();
            return busEmpresa.Excluir(pId);
        }

        /// <summary>
        /// Seleciona uma lista de informações das empresas disponíveis.
        /// </summary>
        /// <returns>sta contendo informações das empresas.</returns>
        public static List<EmpresaInfo> SelecionarLista()
        {
            EmpresaBUS busEmpresa = new EmpresaBUS();
            return busEmpresa.SelecionarLista();
        }

        /// <summary>
        /// Retorna uma lista de empresas cadastradas filtrando pelo nome.
        /// </summary>
        /// <param name="nome">Nome da empresa.</param>
        /// <returns>Lista de empresas filtrada pelo nome.</returns>
        public static List<EmpresaInfo> SelecionarListaPorNome(string nome)
        {
            EmpresaBUS busEmpresa = new EmpresaBUS();
            return busEmpresa.SelecionarListaPorNome(nome);
        }

        /// <summary>
        /// Retorna uma empresa de acordo com o ID informado.
        /// </summary>
        /// <param name="pId">ID da empresa.</param>
        /// <returns>Dados da empresa.</returns>
        public static EmpresaInfo Selecionar(int pId)
        {
            EmpresaBUS busEmpresa = new EmpresaBUS();
            return busEmpresa.Selecionar(pId);
        }
    }
}
