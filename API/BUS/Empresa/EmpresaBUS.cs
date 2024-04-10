using DAL.Empresa;
using Entity;
using Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumeracoes;

namespace BUS.Empresa
{
    public class EmpresaBUS
    {

        /// <summary>
        /// Retorna uma empresa de acordo com o ID informado.
        /// </summary>
        /// <param name="pId">ID da empresa.</param>
        /// <returns>Dados da empresa.</returns>
        public EmpresaInfo Selecionar(int id)
        {
            EmpresaDAL oDal = new EmpresaDAL(Banco.SGA);
            return oDal.Selecionar(id);
        }

        /// <summary>
        /// Retorna uma lista de empresas cadastradas.
        /// </summary>
        /// <returns>Lista de empresas.</returns>
        public List<EmpresaInfo> SelecionarLista()
        {
            EmpresaDAL oDal = new EmpresaDAL(Banco.SGA);
            return oDal.SelecionarLista();
        }

        /// <summary>
        /// Retorna uma lista de empresas cadastradas filtrando pelo nome.
        /// </summary>
        /// <param name="nome">Nome da empresa.</param>
        /// <returns>Lista de empresas filtrada pelo nome.</returns>
        public List<EmpresaInfo> SelecionarListaPorNome(string nome)
        {
            EmpresaDAL oDal = new EmpresaDAL(Banco.SGA);
            return oDal.SelecionarListaPorNome(nome);
        }

        /// <summary>
        /// Grava dados de uma nova empresa ou atualiza uma ja existente.
        /// </summary>
        /// <param name="pEmpresa">Dados da empresa.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        public RetornoPostInfo Gravar(EmpresaInfo pEmpresa)
        {
            try
            {
                EmpresaDAL oDal = new EmpresaDAL(Banco.SGA);
                int idGravado;

                idGravado = oDal.Gravar(pEmpresa);
                return new RetornoPostInfo()
                {
                    Mensagem = "Gravacao da empresa realizada com sucesso.",
                    Dados = idGravado.ToString()
                };
            }
            catch (Exception ex)
            {

                return new RetornoPostInfo()
                {
                    Mensagem = "Erro ao gravar dados de empresa.",
                    Dados = ex.Message
                };
            }
        }

        /// <summary>
        /// Exclui uma empresa de acordo com o ID informado.
        /// </summary>
        /// <param name="pEmpresa">Dados da empresa.</param>
        /// <returns>Exclui uma empresa.</returns>
        public RetornoPostInfo Excluir(int pId)
        {
            try
            {
                EmpresaDAL oDal = new EmpresaDAL(Banco.SGA);

                EmpresaInfo voEmpresa = oDal.Selecionar(pId);

                if (voEmpresa.ID == 0)
                {
                    return new RetornoPostInfo()
                    {
                        Mensagem = $"Erro ao localizar a empresa no banco de dados. Id:{pId} não existente.",
                        Dados = pId.ToString()
                    };
                }

                oDal.Excluir(voEmpresa);

                return new RetornoPostInfo()
                {
                    Mensagem = "Exclusão da empresa realizada com sucesso.",
                    Dados = pId.ToString()
                };
            }
        
            catch (Exception ex)
            {

                return new RetornoPostInfo()
                {
                    Mensagem = "Erro ao excluir dados da empresa.",
                    Dados = ex.Message
                };
            }

        }
    }
}

