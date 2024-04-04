using DAL.Solicitacao;
using Entity.Solicitacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumeracoes;

namespace BUS.Solicitacao
{
    public class SolicitacaoBUS
    {

        /// <summary>
        /// Retorna uma lista de Solicitações cadastradas.
        /// </summary>
        /// <returns>Lista de Solicitações.</returns>
        public List<SolicitacaoInfo> SelecionarLista()
        {
            SolicitacaoDAL dalSolicitacao = new SolicitacaoDAL(Banco.SGA);
            return dalSolicitacao.SelecionarLista();
        }
    }
}
