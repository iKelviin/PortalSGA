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
        public List<SolicitacaoInfo> SelecionarLista()
        {
            return SolicitacaoFacade.SelecionarLista();
        }
    }
}
