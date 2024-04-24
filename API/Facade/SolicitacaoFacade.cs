using BUS.Solicitacao;
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
        public static List<SolicitacaoInfo> SelecionarLista()
        {
            SolicitacaoBUS busSolicitacao = new SolicitacaoBUS();
            return busSolicitacao.SelecionarLista();
        }
    }
}
