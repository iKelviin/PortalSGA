using Common;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Cargo
{
    public class CargoDAL : BaseDAL<CargoInfo>
    {
        public CargoDAL(Enumeracoes.Banco pBanco) : base(pBanco){}

        /// <summary>
        /// Exclui um cargo de acordo com o ID informado.
        /// </summary>
        /// <param name="pCargo">Dados do cargo.</param>
        /// <returns>Exclui um cargo.</returns>
        public override void Excluir(CargoInfo pCargo)
        {
            try
            {
                using (Connection)
                {
                    Convert.ToInt32(ExecuteScalar("usp_ExcluirCargo", pCargo));
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Grava dados de umo novo Cargo ou atualiza um ja existente.
        /// </summary>
        /// <param name="pCargo">Dados do Cargo.</param>
        /// <returns>Id do Cargo gravado.</returns>
        public override int Gravar(CargoInfo pCargo)
        {
            try
            {
                using (Connection)
                {
                    return Convert.ToInt32(ExecuteScalar("usp_GravarCargo", pCargo));
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna um Cargo de acordo com o Id informado.
        /// </summary>
        /// <param name="pId">Id do Cargo.</param>
        /// <returns>Dados do Cargo.</returns>
        public override CargoInfo Selecionar(int pId)
        {
            try
            {
                DataTable dt;
                CargoInfo ObjRetorno = new CargoInfo();

                CargoInfo cargo = new CargoInfo();
                cargo.Id = pId;

                using (Connection)
                {
                    dt = ExecuteDataTable("usp_ObterCargos", cargo);
                }


                if (dt.Rows.Count > 0)
                {
                    ObjRetorno = PegaItem<CargoInfo>(dt.Rows[0]);
                }

                return ObjRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna uma lista de Cargos cadastrados.
        /// </summary>
        /// <returns>Lista de Cargos.</returns>
        public override List<CargoInfo> SelecionarLista()
        {
            try
            {
                DataTable dt;

                List<CargoInfo> lstCargo = new List<CargoInfo>();

                using (Connection)
                {
                    dt = ExecuteDataTable("usp_ObterCargos", CommandType.StoredProcedure);
                }


                if (dt.Rows.Count > 0)
                {
                    lstCargo = ConverteParaLista<CargoInfo>(dt);
                }

                return lstCargo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna uma lista de Cargos cadastrados para um determinado departamento.
        /// </summary>
        /// <param name="pIdDepartamento">Id do departamento.</param>
        /// <returns>Lista de Cargos por departamento.</returns>
        public List<CargoInfo> SelecionarListaPorDepartamento(int pIdDepartamento)
        {
            try
            {
                DataTable dt;

                List<CargoInfo> lstCargo = new List<CargoInfo>();
                CargoInfo cargo = new CargoInfo();
                cargo.IdDepartamento = pIdDepartamento;

                using (Connection)
                {
                    dt = ExecuteDataTable("usp_ObterCargosPorDepartamento", cargo);
                }


                if (dt.Rows.Count > 0)
                {
                    lstCargo = ConverteParaLista<CargoInfo>(dt);
                }

                return lstCargo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna uma lista de Cargos cadastrados filtrando pelo nome.
        /// </summary>
        /// <param name="pNome">Nome do Cargo.</param>
        /// <returns>Lista de Cargos filtrados pelo nome.</returns>
        public List<CargoInfo> SelecionarListaPorNome(string pNome)
        {
            try
            {
                DataTable dt;
                List<CargoInfo> lst = new List<CargoInfo>();

                CargoInfo dep = new CargoInfo();
                dep.Nome = pNome;

                using (Connection)
                {
                    dt = ExecuteDataTable("usp_ObterCargosPorNome", dep);
                }
                if (dt.Rows.Count > 0)
                {
                    lst = ConverteParaLista<CargoInfo>(dt);
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
