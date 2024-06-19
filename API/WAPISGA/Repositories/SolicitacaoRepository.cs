using Entity.Common;
using Entity;
using Entity.Solicitacao;
using Facade;

namespace WAPISGA.Repositories
{
    public class SolicitacaoRepository
    {
        /// <summary>
        /// Retorna uma lista de Solicitações cadastradas.
        /// </summary>
        /// <returns>Lista de Solicitações.</returns>
        public List<SolicitacaoDTOInfo> SelecionarLista()
        {
            return SolicitacaoFacade.SelecionarLista();
        }
        /// <summary>
        /// Retorna uma Solicitação pelo Id.
        /// </summary>
        /// <param name="id">Id da Solicitação.</param>
        /// <returns>Dados da Solicitação.</returns>
        public SolicitacaoDTOInfo Selecionar(int id)
        {
            return SolicitacaoFacade.Selecionar(id);
        }

        /// <summary>
        /// Grava dados de uma nova solicitação ou atualiza uma ja existente.
        /// </summary>
        /// <param name="pSolicitacao">Dados da Solicitação.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        public RetornoPostInfo Gravar(SolicitacaoDTOInfo pSolicitacao)
        {
            return SolicitacaoFacade.Gravar(pSolicitacao);
        }
    }
}
