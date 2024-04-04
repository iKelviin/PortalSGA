using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumeracoes;

namespace DAL.Empresa
{
    public class EmpresaDAL : BaseDAL<EmpresaInfo>
    {
        public EmpresaDAL(Banco pBanco) : base(pBanco) { }

        /// <summary>
        /// Exclui uma empresa de acordo com o ID informado.
        /// </summary>
        /// <param name="pEmpresa">Dados da empresa.</param>
        /// <returns>Exclui uma empresa.</returns>
        public override void Excluir(EmpresaInfo pEmpresa)
        {
            try
            {
                using (Connection)
                {
                    Parameters = new SqlParameter[1];
                    Parameters[0] = new SqlParameter("@ID", pEmpresa.ID);

                    ExecuteNonQuery("usp_ExcluirEmpresa", System.Data.CommandType.StoredProcedure, Parameters);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Grava dados de uma nova empresa ou atualiza uma ja existente.
        /// </summary>
        /// <param name="pEmpresa">Dados da empresa.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        public override int Gravar(EmpresaInfo pEmpresa)
        {
            try
            {
                using (Connection)
                {
                    return Convert.ToInt32(ExecuteScalar("usp_InserirEmpresa", pEmpresa));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Retorna uma empresa de acordo com o ID informado.
        /// </summary>
        /// <param name="pId">ID da empresa.</param>
        /// <returns>Dados da empresa.</returns>
        public override EmpresaInfo Selecionar(int id)
        {
            try
            {
                DataTable dt;
                EmpresaInfo obj = new EmpresaInfo();
                using (Connection)
                {
                    Parameters = new SqlParameter[1];
                    Parameters[0] = new SqlParameter("@ID", id);
                    dt = ExecuteDataTable("usp_ObterEmpresa", System.Data.CommandType.StoredProcedure, Parameters);
                }

                if (dt.Rows.Count > 0)
                {
                    obj = PegaItem<EmpresaInfo>(dt.Rows[0]);
                }
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Retorna uma lista de empresas cadastradas.
        /// </summary>
        /// <returns>Lista de empresas.</returns>
        public override List<EmpresaInfo> SelecionarLista()
        {

            List<EmpresaInfo> lst = new List<EmpresaInfo>();
            try
            {
                DataTable dt;

                using (Connection)
                {
                    dt = ExecuteDataTable("usp_ObterEmpresas", CommandType.StoredProcedure);
                }
                if (dt.Rows.Count > 0)
                {
                    lst = ConverteParaLista<EmpresaInfo>(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lst;

        }

        /// <summary>
        /// Retorna uma lista de empresas cadastradas filtrando pelo nome.
        /// </summary>
        /// <param name="nome">Nome da empresa.</param>
        /// <returns>Lista de empresas filtrada pelo nome.</returns>
        public List<EmpresaInfo> SelecionarListaPorNome(string nome)
        {

            List<EmpresaInfo> lst = new List<EmpresaInfo>();
            try
            {
                DataTable dt;

                using (Connection)
                {
                    Parameters = new SqlParameter[1];
                    Parameters[0] = new SqlParameter("@Nome", nome);
                    dt = ExecuteDataTable("usp_ObterEmpresas", CommandType.StoredProcedure, Parameters);
                }
                if (dt.Rows.Count > 0)
                {
                    lst = ConverteParaLista<EmpresaInfo>(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lst;

        }
    }
}
