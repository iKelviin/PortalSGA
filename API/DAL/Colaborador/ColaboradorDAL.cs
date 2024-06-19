using Entity;
using Entity.Solicitacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumeracoes;

namespace DAL.Colaborador
{
    public class ColaboradorDAL : BaseDAL<ColaboradorInfo>
    {
        public ColaboradorDAL(Banco banco) : base(banco) { }
        public override void Excluir(ColaboradorInfo voT)
        {
            throw new NotImplementedException();
        }

        public override int Gravar(ColaboradorInfo pColaborador)
        {
            try
            {
                using (Connection)
                {
                    return Convert.ToInt32(ExecuteScalar<ColaboradorInfo>("usp_InserirColaborador", pColaborador));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public override ColaboradorInfo Selecionar(int id)
        {
            throw new NotImplementedException();
        }

        public override List<ColaboradorInfo> SelecionarLista()
        {
            throw new NotImplementedException();
        }
    }
}
