using Entity.Solicitacao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumeracoes;

namespace DAL.Solicitacao
{
    public class SolicitacaoDAL : BaseDAL<SolicitacaoDTOInfo>
    {
        public SolicitacaoDAL(Banco banco) : base(banco) { }

        public override void Excluir(SolicitacaoDTOInfo voT)
        {
            throw new NotImplementedException();
        }
        public void Cancelar(SolicitacaoDTOInfo solicitacao)
        {
            try
            {
                using (Connection)
                {
                    Parameters = new SqlParameter[1];
                    Parameters[0] = new SqlParameter("@Codigo", solicitacao.Codigo);

                    ExecuteNonQuery("usp_CancelarSolicitacao", System.Data.CommandType.StoredProcedure, Parameters);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public override int Gravar(SolicitacaoDTOInfo solicitacao)
        {
            try
            {
                using (Connection)
                {
                    return Convert.ToInt32(ExecuteScalar<SolicitacaoDTOInfo>("usp_InserirSolicitacao", solicitacao));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public override SolicitacaoDTOInfo Selecionar(int id)
        {
            try
            {
                DataTable dt;
                SolicitacaoDTOInfo obj = new SolicitacaoDTOInfo();
                using (Connection)
                {
                    Parameters = new SqlParameter[1];
                    Parameters[0] = new SqlParameter("@ID", id);
                    dt = ExecuteDataTable("usp_ObterSolicitacao", System.Data.CommandType.StoredProcedure, Parameters);
                }

                if (dt.Rows.Count > 0)
                {
                    obj = PegaItem<SolicitacaoDTOInfo>(dt.Rows[0]);
                }
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public override List<SolicitacaoDTOInfo> SelecionarLista()
        {
            List<SolicitacaoDTOInfo> lst = new List<SolicitacaoDTOInfo>();
            try
            {
                DataTable dt;

                using (Connection)
                {
                    dt = ExecuteDataTable("usp_ObterSolicitacoes", CommandType.StoredProcedure);
                }
                if (dt.Rows.Count > 0)
                {
                    lst = ConverteParaLista<SolicitacaoDTOInfo>(dt);
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
