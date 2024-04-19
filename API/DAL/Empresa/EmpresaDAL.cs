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
                    ExecuteNonQuery("usp_ExcluirEmpresa", pEmpresa);
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
                    return Convert.ToInt32(ExecuteScalar("usp_GravarEmpresa", pEmpresa));
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
        public override EmpresaInfo Selecionar(int pId)
        {
            try
            {
                DataTable dt;
                EmpresaInfo empresa = new EmpresaInfo();
                empresa.Id = pId;

                using (Connection)
                {                   
                    dt = ExecuteDataTable("usp_ObterEmpresas", empresa);
                }

                if (dt.Rows.Count > 0)
                {
                    empresa = PegaItem<EmpresaInfo>(dt.Rows[0]);
                }
                return empresa;
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
        /// <param name="pNome">Nome da empresa.</param>
        /// <returns>Lista de empresas filtrada pelo nome.</returns>
        public List<EmpresaInfo> SelecionarListaPorNome(string pNome)
        {
            try
            {
                DataTable dt;
                List<EmpresaInfo> lst = new List<EmpresaInfo>();

                using (Connection)
                {
                    dt = ExecuteDataTable("usp_ObterEmpresasPorNome", new object[] { pNome });
                }
                if (dt.Rows.Count > 0)
                {
                    lst = ConverteParaLista<EmpresaInfo>(dt);
                }

                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
