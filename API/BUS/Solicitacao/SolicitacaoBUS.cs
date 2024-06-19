using DAL.Departamento;
using DAL.Solicitacao;
using Entity.Common;
using Entity;
using Entity.Solicitacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumeracoes;
using DAL.Colaborador;
using DAL.Sistema;
using Entity.AcessoSistema;

namespace BUS.Solicitacao
{
    public class SolicitacaoBUS
    {

        /// <summary>
        /// Retorna uma lista de Solicitações cadastradas.
        /// </summary>
        /// <returns>Lista de Solicitações.</returns>
        public List<SolicitacaoDTOInfo> SelecionarLista()
        {
            SolicitacaoDAL dalSolicitacao = new SolicitacaoDAL(Banco.SGA);
            AcessoSistemaDAL dalAcesso = new AcessoSistemaDAL(Banco.SGA);
            List<SolicitacaoDTOInfo> solicitacoes = dalSolicitacao.SelecionarLista();
            List<AcessoSistemaInfo> acessoSistemas = new List<AcessoSistemaInfo>();
            List<SistemaInfo> sistemas = new List<SistemaInfo>();

            if (solicitacoes.Count > 0)
            {
                foreach (SolicitacaoDTOInfo solicitacao in solicitacoes)
                {
                    acessoSistemas = dalAcesso.SelecionarByCodigo(solicitacao.Codigo);

                    foreach (AcessoSistemaInfo acesso in acessoSistemas)
                    {
                        SistemaInfo sistema = new SistemaInfo
                        {
                            Id = acesso.IdSistema,
                            Nome = acesso.Nome
                        };
                        
                        sistemas.Add(sistema);
                    }
                        solicitacao.Sistemas = sistemas;

                }
            }

            return solicitacoes;
        }

        /// <summary>
        /// Retorna uma Solicitação pelo Id.
        /// </summary>
        /// <param name="id">Id da Solicitação.</param>
        /// <returns>Dados da Solicitação.</returns>
        public SolicitacaoDTOInfo Selecionar(int id)
        {
            SolicitacaoDAL dalSolicitacao = new SolicitacaoDAL(Banco.SGA);
            return dalSolicitacao.Selecionar(id);
        }

        /// <summary>
        /// Grava dados de uma nova solicitação ou atualiza uma ja existente.
        /// </summary>
        /// <param name="pSolicitacao">Dados da Solicitação.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        public RetornoPostInfo Gravar(SolicitacaoDTOInfo pSolicitacao)
        {
            try
            {
                ColaboradorDAL colDal = new ColaboradorDAL(Banco.SGA);
                SolicitacaoDAL oDal = new SolicitacaoDAL(Banco.SGA);
                AcessoSistemaDAL sisDal = new AcessoSistemaDAL(Banco.SGA);
                int idGravado;

                idGravado = oDal.Gravar(pSolicitacao);

                ColaboradorInfo colaborador = new ColaboradorInfo
                {
                    Codigo = pSolicitacao.Codigo,
                    NomeCompleto = pSolicitacao.NomeCompleto,
                    IdEmpresa = pSolicitacao.IdEmpresa,
                    IdCargo = pSolicitacao.IdCargo,
                    Superior = pSolicitacao.Superior,
                    TipoContrato = pSolicitacao.TipoContrato,
                    CentroCusto = pSolicitacao.CentroCusto
                };

                int idColaborador = colDal.Gravar(colaborador);

                if (idColaborador == 0) return new RetornoPostInfo() { Mensagem = "Erro ao gravar o colaborador da solicitação." };


                foreach (SistemaInfo sistema in pSolicitacao.Sistemas)
                {

                    AcessoSistemaInfo access = new AcessoSistemaInfo
                    {
                        Codigo = pSolicitacao.Codigo,
                        IdSistema = sistema.Id
                    };

                    int idInserido = sisDal.Gravar(access);

                    if (idInserido == 0) return new RetornoPostInfo() { Mensagem = "Erro ao gravar o Acesso ao sistema." };
                }

                return new RetornoPostInfo()
                {
                    Mensagem = "Gravacao da solicitação realizada com sucesso.",
                    Dados = idGravado.ToString()
                };
            }
            catch (Exception ex)
            {

                return new RetornoPostInfo()
                {
                    Mensagem = "Erro ao gravar dados da solicitação.",
                    Dados = ex.Message
                };
            }
        }
    }
}
