using Entity;
using Entity.AcessoSistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumeracoes;

namespace DAL.Sistema
{
    public class AcessoSistemaDAL : BaseDAL<AcessoSistemaInfo>
    {
        public AcessoSistemaDAL(Banco banco) : base(banco) { }
        public override void Excluir(AcessoSistemaInfo voT)
        {
            throw new NotImplementedException();
        }

        public override int Gravar(AcessoSistemaInfo pAcessoSistema)
        {
            try
            {
                using (Connection)
                {
                    return Convert.ToInt32(ExecuteScalar<AcessoSistemaInfo>("usp_InserirSistemaColaborador", pAcessoSistema));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public override AcessoSistemaInfo Selecionar(int id)
        {
            throw new NotImplementedException();
        }

        public override List<AcessoSistemaInfo> SelecionarLista()
        {
            throw new NotImplementedException();
        }

        public List<AcessoSistemaInfo> SelecionarByCodigo(int pCodigo)
        {
            try
            {
                DataTable dt;
                List<AcessoSistemaInfo> ObjRetorno = new List<AcessoSistemaInfo>();

                AcessoSistemaInfo ac = new AcessoSistemaInfo();
                ac.Codigo = pCodigo;



                using (Connection)
                {
                    dt = ExecuteDataTable("usp_ObterAcessosSistemas", ac);
                }


                if (dt.Rows.Count > 0)
                {
                    ObjRetorno = ConverteParaLista<AcessoSistemaInfo>(dt);
                }

                return ObjRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
