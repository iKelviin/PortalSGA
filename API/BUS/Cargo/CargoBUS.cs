using Entity.Common;
using Entity;
using static Common.Enumeracoes;
using DAL.Cargo;

namespace BUS.Cargo
{
    public class CargoBUS
    {

        /// <summary>
        /// Retorna um cargo de acordo com o ID informado.
        /// </summary>
        /// <param name="pId">ID do cargo.</param>
        /// <returns>Dados do cargo.</returns>
        public CargoInfo Selecionar(int id)
        {
            CargoDAL oDal = new CargoDAL(Banco.SGA);
            return oDal.Selecionar(id);
        }

        /// <summary>
        /// Retorna uma lista de cargos cadastrados.
        /// </summary>
        /// <returns>Lista de cargos.</returns>
        public List<CargoInfo> SelecionarLista()
        {
            CargoDAL oDal = new CargoDAL(Banco.SGA);
            return oDal.SelecionarLista();
        }

        /// <summary>
        /// Retorna uma lista de cargos cadastrados para um determinado departamento.
        /// </summary>
        /// <param name="pIdDepartamento">Id do departamento.</param>
        /// <returns>Lista de cargos por departamento.</returns>
        public List<CargoInfo> SelecionarLista(int pIdDepartamento)
        {
            CargoDAL oDal = new CargoDAL(Banco.SGA);
            return oDal.SelecionarListaPorDepartamento(pIdDepartamento);
        }

        /// <summary>
        /// Retorna uma lista de cargos cadastrados filtrando pelo nome.
        /// </summary>
        /// <param name="pNome">Nome do cargo.</param>
        /// <returns>Lista de cargos filtrados pelo nome.</returns>
        public List<CargoInfo> SelecionarListaPorNome(string pNome)
        {
            CargoDAL oDal = new CargoDAL(Banco.SGA);
            return oDal.SelecionarListaPorNome(pNome);
        }

        /// <summary>
        /// Grava dados de um novo cargo ou atualiza um ja existente.
        /// </summary>
        /// <param name="pIdDepartamento">Id do departamento.</param>
        /// <param name="pcargo">Dados do cargo.</param>
        /// <returns>Resultado da operacao de gravacao.</returns>
        public RetornoPostInfo Gravar(int pIdDepartamento, CargoInfo pcargo)
        {
            try
            {
                pcargo.IdDepartamento = pIdDepartamento;

                CargoDAL oDal = new CargoDAL(Banco.SGA);
                int idGravado;

                idGravado = oDal.Gravar(pcargo);
                return new RetornoPostInfo()
                {
                    Mensagem = "Gravacao do cargo realizada com sucesso.",
                    Dados = idGravado.ToString()
                };
            }
            catch (Exception ex)
            {

                return new RetornoPostInfo()
                {
                    Mensagem = "Erro ao gravar dados do cargo.",
                    Dados = ex.Message
                };
            }
        }

        /// <summary>
        /// Exclui um cargo de acordo com o ID informado.
        /// </summary>
        /// <param name="pId">Dados do cargo.</param>
        /// <returns>Exclui um cargo.</returns>
        public RetornoPostInfo Excluir(int pId)
        {
            try
            {
                CargoDAL oDal = new CargoDAL(Banco.SGA);

                CargoInfo voCargo = oDal.Selecionar(pId);

                if (voCargo.Id == 0)
                {
                    return new RetornoPostInfo()
                    {
                        Mensagem = $"Erro ao localizar o cargo no banco de dados. Id:{pId} não existente.",
                        Dados = pId.ToString()
                    };
                }

                oDal.Excluir(voCargo);

                return new RetornoPostInfo()
                {
                    Mensagem = "Exclusão do cargo realizada com sucesso.",
                    Dados = pId.ToString()
                };
            }

            catch (Exception ex)
            {

                return new RetornoPostInfo()
                {
                    Mensagem = "Erro ao excluir dados do cargo.",
                    Dados = ex.Message
                };
            }

        }
    }
}
