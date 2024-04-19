using Common;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Departamento
{
    public class DepartamentoDAL : BaseDAL<DepartamentoInfo>
    {
        public DepartamentoDAL(Enumeracoes.Banco pBanco) : base(pBanco) { }

        /// <summary>
        /// Exclui um departamento de acordo com o ID informado.
        /// </summary>
        /// <param name="pDepartamento">Dados do departamento.</param>
        /// <returns>Exclui um departamento.</returns>
        public override void Excluir(DepartamentoInfo pDepartamento)
        {
            try
            {
                using (Connection)
                {
                    Convert.ToInt32(ExecuteScalar("usp_ExcluirDepartamento", pDepartamento));
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Grava dados de umo novo departamento ou atualiza um ja existente.
        /// </summary>
        /// <param name="pDepartamento">Dados do departamento.</param>
        /// <returns>Id do departamento gravado.</returns>
        public override int Gravar(DepartamentoInfo pDepartamento)
        {
            try
            {
                using (Connection)
                {
                    return Convert.ToInt32(ExecuteScalar("usp_GravarDepartamento", pDepartamento));
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna um departamento de acordo com o Id informado.
        /// </summary>
        /// <param name="pId">Id do departamento.</param>
        /// <returns>Dados do departamento.</returns>
        public override DepartamentoInfo Selecionar(int pId)
        {
            try
            {
                DataTable dt;
                DepartamentoInfo ObjRetorno = new DepartamentoInfo();

                DepartamentoInfo dep = new DepartamentoInfo();
                dep.Id = pId;



                using (Connection)
                {
                    dt = ExecuteDataTable("usp_ObterDepartamentos", dep);
                }


                if (dt.Rows.Count > 0)
                {
                    ObjRetorno = PegaItem<DepartamentoInfo>(dt.Rows[0]);
                }

                return ObjRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna uma lista de departamentos cadastrados.
        /// </summary>
        /// <returns>Lista de departamentos.</returns>
        public override List<DepartamentoInfo> SelecionarLista()
        {
            try
            {
                DataTable dt;

                List<DepartamentoInfo> lstDepartamento = new List<DepartamentoInfo>();

                using (Connection)
                {
                    dt = ExecuteDataTable("usp_ObterDepartamentos", CommandType.StoredProcedure);
                }


                if (dt.Rows.Count > 0)
                {
                    lstDepartamento = ConverteParaLista<DepartamentoInfo>(dt);
                }

                return lstDepartamento;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna uma lista de departamentos cadastrados para uma determinada empresa.
        /// </summary>
        /// <param name="pIdEmpresa">Id da empresa.</param>
        /// <returns>Lista de departamentos por empresa.</returns>
        public List<DepartamentoInfo> SelecionarListaPorEmpresa(int pIdEmpresa)
        {
            try
            {
                DataTable dt;

                List<DepartamentoInfo> lstDepartamento = new List<DepartamentoInfo>();
                DepartamentoInfo dep = new DepartamentoInfo();
                dep.IdEmpresa = pIdEmpresa;

                using (Connection)
                {
                    dt = ExecuteDataTable("usp_ObterDepartamentosPorEmpresa", dep);
                }


                if (dt.Rows.Count > 0)
                {
                    lstDepartamento = ConverteParaLista<DepartamentoInfo>(dt);
                }

                return lstDepartamento;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna uma lista de departamentos cadastrados filtrando pelo nome.
        /// </summary>
        /// <param name="pNome">Nome do departamento.</param>
        /// <returns>Lista de departamentos filtrados pelo nome.</returns>
        public List<DepartamentoInfo> SelecionarListaPorNome(string pNome)
        {
            try
            {
                DataTable dt;
                List<DepartamentoInfo> lst = new List<DepartamentoInfo>();

                DepartamentoInfo dep = new DepartamentoInfo();
                dep.Nome = pNome;

                using (Connection)
                {
                    dt = ExecuteDataTable("usp_ObterDepartamentosPorNome", dep);
                }
                if (dt.Rows.Count > 0)
                {
                    lst = ConverteParaLista<DepartamentoInfo>(dt);
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
