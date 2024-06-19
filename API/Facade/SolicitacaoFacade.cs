using BUS.Solicitacao;
using Entity.Common;
using Entity.Solicitacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    public static class SolicitacaoFacade
    {
        /// <summary>
        /// Retorna uma lista de Solicitações cadastradas.
        /// </summary>
        /// <returns>Lista de Solicitações.</returns>
        public static List<SolicitacaoDTOInfo> SelecionarLista()
        {
            SolicitacaoBUS busSolicitacao = new SolicitacaoBUS();
            return busSolicitacao.SelecionarLista();
        }

        /// <summary>
        /// Retorna uma Solicitação pelo Id.
        /// </summary>
        /// <param name="id">Id da Solicitação.</param>
        /// <returns>Dados da Solicitação.</returns>
        public static SolicitacaoDTOInfo Selecionar(int id)
        {
            SolicitacaoBUS busSolicitacao = new SolicitacaoBUS();
            return busSolicitacao.Selecionar(id);
        }

        /// <summary>
        /// Grava dados de uma nova solicitação ou atualiza uma ja existente.
        /// </summary>
        /// <param name="pSolicitacao">Dados da Solicitação.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        public static RetornoPostInfo Gravar(SolicitacaoDTOInfo pSolicitacao)
        {
            SolicitacaoBUS busSolicitacao = new SolicitacaoBUS();
            return busSolicitacao.Gravar(pSolicitacao);
        }
    }
}
