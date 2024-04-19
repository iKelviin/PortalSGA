using Entity.Common;
using Entity;
using static Common.Enumeracoes;
using DAL.Departamento;

namespace BUS.Departamento
{
    public class DepartamentoBUS
    {
        /// <summary>
        /// Retorna um departamento de acordo com o ID informado.
        /// </summary>
        /// <param name="pId">ID do departamento.</param>
        /// <returns>Dados do departamento.</returns>
        public DepartamentoInfo Selecionar(int id)
        {
            DepartamentoDAL oDal = new DepartamentoDAL(Banco.SGA);
            return oDal.Selecionar(id);
        }

        /// <summary>
        /// Retorna uma lista de departamentos cadastradas.
        /// </summary>
        /// <returns>Lista de departamentos.</returns>
        public List<DepartamentoInfo> SelecionarLista()
        {
            DepartamentoDAL oDal = new DepartamentoDAL(Banco.SGA);
            return oDal.SelecionarLista();
        }

        /// <summary>
        /// Retorna uma lista de departamentos cadastrados para uma determinada empresa.
        /// </summary>
        /// <param name="pIdEmpresa">Id da empresa.</param>
        /// <returns>Lista de departamentos por empresa.</returns>
        public List<DepartamentoInfo> SelecionarLista(int pIdEmpresa)
        {
            DepartamentoDAL oDal = new DepartamentoDAL(Banco.SGA);
            return oDal.SelecionarListaPorEmpresa(pIdEmpresa);
        }

        /// <summary>
        /// Retorna uma lista de departamentos cadastrados filtrando pelo nome.
        /// </summary>
        /// <param name="pNome">Nome do departamento.</param>
        /// <returns>Lista de departamentos filtrados pelo nome.</returns>
        public List<DepartamentoInfo> SelecionarListaPorNome(string pNome)
        {
            DepartamentoDAL oDal = new DepartamentoDAL(Banco.SGA);
            return oDal.SelecionarListaPorNome(pNome);
        }

        /// <summary>
        /// Grava dados de um novo departamento ou atualiza um ja existente.
        /// </summary>
        /// <param name="pIdEmpresa">Id da empresa.</param>
        /// <param name="pDepartamento">Dados do departamento.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        public RetornoPostInfo Gravar(int pIdEmpresa, DepartamentoInfo pDepartamento)
        {
            try
            {
                pDepartamento.IdEmpresa = pIdEmpresa;

                DepartamentoDAL oDal = new DepartamentoDAL(Banco.SGA);
                int idGravado;

                idGravado = oDal.Gravar(pDepartamento);
                return new RetornoPostInfo()
                {
                    Mensagem = "Gravacao do departamento realizada com sucesso.",
                    Dados = idGravado.ToString()
                };
            }
            catch (Exception ex)
            {

                return new RetornoPostInfo()
                {
                    Mensagem = "Erro ao gravar dados do departamento.",
                    Dados = ex.Message
                };
            }
        }

        /// <summary>
        /// Exclui um departamento de acordo com o ID informado.
        /// </summary>
        /// <param name="pId">Dados do departamento.</param>
        /// <returns>Exclui um departamento.</returns>
        public RetornoPostInfo Excluir(int pId)
        {
            try
            {
                DepartamentoDAL oDal = new DepartamentoDAL(Banco.SGA);

                DepartamentoInfo voDepartamento = oDal.Selecionar(pId);

                if (voDepartamento.Id == 0)
                {
                    return new RetornoPostInfo()
                    {
                        Mensagem = $"Erro ao localizar o departamento no banco de dados. Id:{pId} não existente.",
                        Dados = pId.ToString()
                    };
                }

                oDal.Excluir(voDepartamento);

                return new RetornoPostInfo()
                {
                    Mensagem = "Exclusão do departamento realizada com sucesso.",
                    Dados = pId.ToString()
                };
            }

            catch (Exception ex)
            {

                return new RetornoPostInfo()
                {
                    Mensagem = "Erro ao excluir dados do departamento.",
                    Dados = ex.Message
                };
            }

        }
    }
}
