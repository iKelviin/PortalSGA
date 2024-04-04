using Common;
using Entity.Common;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using static Common.Enumeracoes;

namespace DAL
{
    /// <summary>
    /// Classe abstrata que serve como base para camada de acesso a dados (DAL - Data Access Layer).
    /// </summary>
    /// <typeparam name="T">Tipo de entidade associada à camada de acesso a dados.</typeparam>
    abstract public class BaseDAL<T>
    {
        #region Propriedades
        /// <summary>
        /// Propriedade de Conexão
        /// </summary>
        private SqlConnection connection;
        /// <summary>
        /// Propriedade de Transação do Banco de Dados.
        /// </summary>
        private SqlTransaction transaction;
        /// <summary>
        /// Parametros utilizados nos comandos SQL das classes DAL.
        /// </summary>
        private SqlParameter[] parameters;
        /// <summary>
        /// Comando SQL que será utilizado pelas classes DAL.
        /// </summary>
        private SqlCommand command;
        /// <summary>
        /// SqlDataReader utilizado nas classes DAL.
        /// </summary>
        private SqlDataReader reader;
        /// <summary>
        /// Banco aonde a conexao sera realizada.
        /// </summary>
        private Banco bancoConexao;

        public SqlConnection Connection
        {
            get
            {
                if (connection == null)
                {

                    connection = new SqlConnection(Funcoes.MontaConexao(bancoConexao));

                    connection.Open();


                }
                else
                {


                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Dispose();

                        connection = new SqlConnection(Funcoes.MontaConexao(bancoConexao));
                        connection.Open();
                    }


                }
                return connection;
            }
            set { connection = value; }
        }

        public Banco BancoConexao
        {
            get { return bancoConexao; }
            set { bancoConexao = value; }
        }

        public SqlTransaction Transaction
        {
            get { return transaction; }
            set { transaction = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlParameter[] Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlCommand Command
        {
            get
            {
                command.CommandTimeout = 30000;
                return command;
            }
            set { command = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlDataReader Reader
        {
            get { return reader; }
            set { reader = value; }
        }

        /// <summary>
        /// Propriedades de TryParse
        /// </summary>
        public DateTime data = new DateTime();
        public int inteiro = new int();
        public float numero = new float();
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor da BaseDAL onde abre a conexão para todas as classes DAL.
        /// </summary>
        public BaseDAL(Banco pBanco)
        {
            try
            {
                this.bancoConexao = pBanco;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Destrutor da BaseDAL, onde verifica o status da Conexão. 
        /// Se estiver aberta, a conexão será fechada.
        /// </summary>
        ~BaseDAL()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Metodos Abstratos CRUD


        /// <summary>
        /// Método abstrato para excluir um objeto do banco.
        /// </summary>
        /// <param name="voT">Objeto que será excluído do banco.</param>
        abstract public void Excluir(T voT);

        /// <summary>
        /// Método abstrato para gravar(alterar) um objeto no banco.
        /// </summary>
        /// <param name="voT">Objeto com as informações que serão gravadas.</param>
        abstract public int Gravar(T voT);

        /// <summary>
        /// Método que obtém os dados do tipo do objeto(T) que estão no banco.
        /// </summary>
        /// <param name="voT">Objeto com informações para filtrar a busca no banco.</param>
        /// <returns>Collection do Tipo do Objeto (T) que será retornada.</returns>
        abstract public List<T> SelecionarLista();

        /// <summary>
        /// Metodo que obtem um objeto T a partir de um ID informado.
        /// </summary>
        /// <param name="id">ID do objeto.</param>
        /// <returns>Objeto (T).</returns>
        abstract public T Selecionar(int id);

        #endregion


        #region Execute NonQuery

        /// <summary>
        /// Comando para executar NonQuery com passagem automatica de parametros, a partir do array de parametros informados.
        /// </summary>
        /// <param name="pProcedureName">Nome da procedure a ser executada.</param>
        /// <param name="pParams">Array de parametros informados.</param>
        /// <returns>Resultado da execucao.</returns>
        public int ExecuteNonQuery(string pProcedureName, object[] pParams)
        {
            List<ParametroSQLInfo> parametrosProcedure = SelecionarParametrosDeEntrada(pProcedureName);
            Parameters = new SqlParameter[parametrosProcedure.Count];
            StackTrace pilhaExecucao = new StackTrace();
            ParameterInfo[] parametrosMetodo = pilhaExecucao.GetFrame(1)!.GetMethod()!.GetParameters();

            int contador = 0;

            foreach (ParametroSQLInfo parametroProcedure in parametrosProcedure)
            {
                for (int i = 0; i < parametrosMetodo.Length; i++)
                {
                    if (parametroProcedure.Nome == string.Concat("@", parametrosMetodo[i].Name!.Remove(0, 1)))
                    {
                        if (parametroProcedure.AceitaNulo && pParams[i] == null)
                        {
                            Parameters[contador] = new SqlParameter(parametroProcedure.Nome, DBNull.Value);
                        }

                        Parameters[contador] = new SqlParameter(parametroProcedure.Nome, pParams[i]);
                        contador++;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return ExecuteNonQuery(pProcedureName, CommandType.StoredProcedure, Parameters);
        }

        /// <summary>
        /// Comando para executar NonQuery com passagem automatica de parametros, a partir dos atributos do objeto informado.
        /// </summary>
        /// <typeparam name="Typ">Tipo do objeto informado.</typeparam>
        /// <param name="pProcedureName">Nome da procedure a ser executada.</param>
        /// <param name="pObj">Objeto cujos atributos serao passados como parametro.</param>
        /// <returns>Resultado da execucao.</returns>
        public int ExecuteNonQuery<Typ>(string pProcedureName, Typ pObj)
        {
            List<ParametroSQLInfo> parametrosProcedure = SelecionarParametrosDeEntrada(pProcedureName);

            Type tipo = typeof(Typ);
            Parameters = new SqlParameter[parametrosProcedure.Count];
            int contador = 0;

            foreach (ParametroSQLInfo parametro in parametrosProcedure)
            {
                foreach (PropertyInfo propriedade in tipo.GetProperties())
                {
                    if (parametro.Nome == string.Concat("@", propriedade.Name))
                    {
                        if (parametro.AceitaNulo && propriedade.GetValue(pObj) == null)
                        {
                            Parameters[contador] = new SqlParameter(parametro.Nome, DBNull.Value);
                        }
                        Parameters[contador] = new SqlParameter(parametro.Nome, propriedade.GetValue(pObj));
                        contador++;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return ExecuteNonQuery(pProcedureName, CommandType.StoredProcedure, Parameters);
        }

        /// <summary>
        /// Executa um comando SQL especificado com controle de conexão.
        /// </summary>
        /// <param name="pSqlCommand">Comando SQL a ser executado.</param>
        /// <param name="pCommandType">Tipo do comando (por exemplo, Texto ou Procedimento Armazenado).</param>
        /// <param name="pParameters">Parâmetros do comando, se houver.</param>
        /// <returns>O número de linhas afetadas ou o valor de identidade.</returns>
        public int ExecuteNonQuery(string pSqlCommand, System.Data.CommandType pCommandType, SqlParameter[] pParameters)
        {
            try
            {
                using (Connection)
                {
                    // Cria um novo comando SQL
                    Command = new SqlCommand(pSqlCommand, connection);

                    Command.CommandType = pCommandType;

                    // Adiciona os parâmetros ao comando, se houver
                    Command.Parameters.AddRange(pParameters);

                    // Executa o comando e retorna o número de linhas afetadas ou o valor de identidade
                    return Command.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                // Captura e relança exceções
                throw Ex;
            }
        }

        /// <summary>
        /// Comando de execução sem parâmetros.
        /// </summary>
        /// <param name="pSqlCommand">Comando SQL</param>
        /// <param name="pCommandType">Tipo do Comando</param>
        /// <returns>Retorna Identity em caso de Insert, ou a quantidade de linhas alteradas.</returns>
        public int ExecuteNonQuery(string pSqlCommand, System.Data.CommandType pCommandType)
        {
            // Inicializa os parâmetros como um array vazio
            parameters = new SqlParameter[0];

            // Chama o método ExecuteReader com os parâmetros fornecidos
            return ExecuteNonQuery(pSqlCommand, pCommandType, parameters);
        }

        #endregion

        #region Execute DataTable


        /// <summary>
        /// Comando para gerar um DataTable com passagem automatica de parametros, a partir do array de parametros informados.
        /// </summary>
        /// <param name="pProcedureName">Nome da procedure a ser executada.</param>
        /// <param name="pParams">Array de parametros informados.</param>
        /// <returns>Resultado da execucao.</returns>
        public DataTable ExecuteDataTable(string pProcedureName, object[] pParams)
        {
            List<ParametroSQLInfo> parametrosProcedure = SelecionarParametrosDeEntrada(pProcedureName);
            Parameters = new SqlParameter[parametrosProcedure.Count];
            StackTrace pilhaExecucao = new StackTrace();
            ParameterInfo[] parametrosMetodo = pilhaExecucao.GetFrame(1)!.GetMethod()!.GetParameters();

            int contador = 0;

            foreach (ParametroSQLInfo parametroProcedure in parametrosProcedure)
            {
                for (int i = 0; i < parametrosMetodo.Length; i++)
                {
                    if (parametroProcedure.Nome == string.Concat("@", parametrosMetodo[i].Name!.Remove(0, 1)))
                    {
                        if (parametroProcedure.AceitaNulo && pParams[i] == null)
                        {
                            Parameters[contador] = new SqlParameter(parametroProcedure.Nome, DBNull.Value);
                        }

                        Parameters[contador] = new SqlParameter(parametroProcedure.Nome, pParams[i]);
                        contador++;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return ExecuteDataTable(pProcedureName, CommandType.StoredProcedure, Parameters);

        }

        /// <summary>
        /// Comando para gerar um DataTable com passagem automatica de parametros, a partir dos atributos do objeto informado.
        /// </summary>
        /// <typeparam name="Typ">Tipo do objeto informado.</typeparam>
        /// <param name="pProcedureName">Nome da procedure a ser executada.</param>
        /// <param name="pObj">Objeto cujos atributos serao passados como parametro.</param>
        /// <returns>Resultado da execucao.</returns>
        public DataTable ExecuteDataTable<Typ>(string pProcedureName, Typ pObj)
        {
            List<ParametroSQLInfo> parametrosProcedure = SelecionarParametrosDeEntrada(pProcedureName);

            Type tipo = typeof(Typ);

            Parameters = new SqlParameter[parametrosProcedure.Count];

            int contador = 0;

            foreach (ParametroSQLInfo parametro in parametrosProcedure)
            {
                foreach (PropertyInfo propriedade in tipo.GetProperties())
                {
                    if (parametro.Nome == string.Concat("@", propriedade.Name))
                    {
                        if (parametro.AceitaNulo && propriedade.GetValue(pObj) == null)
                        {
                            Parameters[contador] = new SqlParameter(parametro.Nome, DBNull.Value);
                        }

                        Parameters[contador] = new SqlParameter(parametro.Nome, propriedade.GetValue(pObj));
                        contador++;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return ExecuteDataTable(pProcedureName, CommandType.StoredProcedure, Parameters);
        }

        /// <summary>
        /// Comando padrão para execução de comandos de Select com controle de conexão.
        /// </summary>
        /// <param name="pSqlCommand">Comando SQL</param>
        /// <param name="pCommandType">Tipo de Comando</param>
        /// <param name="pParameters">Parametros do Comando</param>
        /// <returns>SqlDataReader</returns>
        public DataTable ExecuteDataTable(string pSqlCommand, System.Data.CommandType pCommandType, SqlParameter[] pParameters)
        {
            try
            {
                using (connection)
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    Command = new SqlCommand(pSqlCommand, connection);

                    Command.CommandType = pCommandType;
                    Command.CommandTimeout = 60;

                    if (pParameters != null)
                    {
                        Command.Parameters.AddRange(pParameters);
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(Command);

                    DataTable dt = new DataTable();

                    sda.Fill(dt);

                    return dt;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Comando de execução sem parâmetros.
        /// </summary>
        /// <param name="pSqlCommand">Comando SQL</param>
        /// <param name="pCommandType">Tipo do Comando</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDataTable(string pSqlCommand, System.Data.CommandType pCommandType)
        {
            // Inicializa os parâmetros como um array vazio
            parameters = new SqlParameter[0];

            // Chama o método ExecuteReader com os parâmetros fornecidos
            return ExecuteDataTable(pSqlCommand, pCommandType, parameters);
        }

        #endregion

        #region Execute Scalar

        /// <summary>
        /// Comando para executar query que retorna apenas um valor, com passagem automatica de parametros, a partir do array de parametros informados.
        /// </summary>
        /// <param name="pProcedureName">Nome da procedure a ser executada.</param>
        /// <param name="pParams">Array de parametros informados.</param>
        /// <returns>Resultado da execucao.</returns>
        public object ExecuteScalar(string pProcedureName, object[] pParams)
        {
            List<ParametroSQLInfo> parametrosProcedure = SelecionarParametrosDeEntrada(pProcedureName);
            Parameters = new SqlParameter[parametrosProcedure.Count];
            StackTrace pilhaExecucao = new StackTrace();
            ParameterInfo[] parametrosMetodo = pilhaExecucao.GetFrame(1)!.GetMethod()!.GetParameters();

            int contador = 0;

            foreach (ParametroSQLInfo parametroProcedure in parametrosProcedure)
            {
                for (int i = 0; i < parametrosMetodo.Length; i++)
                {
                    if (parametroProcedure.Nome == string.Concat("@", parametrosMetodo[i].Name!.Remove(0, 1)))
                    {
                        if (parametroProcedure.AceitaNulo && pParams[i] == null)
                        {
                            Parameters[contador] = new SqlParameter(parametroProcedure.Nome, DBNull.Value);
                        }

                        Parameters[contador] = new SqlParameter(parametroProcedure.Nome, pParams[i]);
                        contador++;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return ExecuteScalar(pProcedureName, CommandType.StoredProcedure, Parameters);
        }

        /// <summary>
        /// Comando para executar query que retorna apenas um valor, com passagem automatica de parametros, a partir dos atributos do objeto informado.
        /// </summary>
        /// <typeparam name="Typ">Tipo do objeto informado.</typeparam>
        /// <param name="pProcedureName">Nome da procedure a ser executada.</param>
        /// <param name="pObj">Objeto cujos atributos serao passados como parametro.</param>
        /// <returns>Resultado da execucao.</returns>
        public object ExecuteScalar<Typ>(string pProcedureName, Typ pObj)
        {
            List<ParametroSQLInfo> parametrosProcedure = SelecionarParametrosDeEntrada(pProcedureName);

            Type tipo = typeof(Typ);

            Parameters = new SqlParameter[parametrosProcedure.Count];

            int contador = 0;

            foreach (ParametroSQLInfo parametro in parametrosProcedure)
            {
                foreach (PropertyInfo propriedade in tipo.GetProperties())
                {
                    if (parametro.Nome == string.Concat("@", propriedade.Name))
                    {
                        if (parametro.AceitaNulo && propriedade.GetValue(pObj) == null)
                        {
                            Parameters[contador] = new SqlParameter(parametro.Nome, DBNull.Value);
                        }

                        Parameters[contador] = new SqlParameter(parametro.Nome, propriedade.GetValue(pObj));
                        contador++;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return ExecuteScalar(pProcedureName, CommandType.StoredProcedure, Parameters);
        }

        /// <summary>
        /// Comando padrão para execução de um comando SQL retornando Objeto.
        /// </summary>
        /// <param name="pSqlCommand">Comando SQL</param>
        /// <param name="pCommandType">Tipo do Comando</param>
        /// <param name="pParameters">Parametros</param>
        /// <returns>Objeto</returns>
        public object ExecuteScalar(string pSqlCommand, System.Data.CommandType pCommandType, SqlParameter[] pParameters)
        {
            try
            {
                using (connection)
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    Command = new SqlCommand(pSqlCommand, Connection);
                    Command.CommandType = pCommandType;
                    Command.Parameters.AddRange(pParameters);

                    return Command.ExecuteScalar();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Comando de execução sem parâmetros.
        /// </summary>
        /// <param name="pSqlCommand">Comando SQL</param>
        /// <param name="pCommandType">Tipo do Comando</param>
        /// <returns></returns>
        public object ExecuteScalar(string pSqlCommand, System.Data.CommandType pCommandType)
        {
            // Inicializa os parâmetros como um array vazio
            parameters = new SqlParameter[0];

            // Chama o método ExecuteReader com os parâmetros fornecidos
            return ExecuteScalar(pSqlCommand, pCommandType, parameters);
        }

        #endregion

        #region Execute Reader

        /// <summary>
        /// Comando para gerar um Reader com passagem automatica de parametros, a partir do array de parametros informados.
        /// </summary>
        /// <param name="pProcedureName">Nome da procedure a ser executada.</param>
        /// <param name="pParams">Array de parametros informados.</param>
        /// <returns>Resultado da execucao.</returns>
        public DataSet ExecuteReader(string pProcedureName, object[] pParams)
        {
            List<ParametroSQLInfo> parametrosProcedure = SelecionarParametrosDeEntrada(pProcedureName);
            Parameters = new SqlParameter[parametrosProcedure.Count];
            StackTrace pilhaExecucao = new StackTrace();
            ParameterInfo[] parametrosMetodo = pilhaExecucao.GetFrame(1)!.GetMethod()!.GetParameters();

            int contador = 0;

            foreach (ParametroSQLInfo parametroProcedure in parametrosProcedure)
            {
                for (int i = 0; i < parametrosMetodo.Length; i++)
                {
                    if (parametroProcedure.Nome == string.Concat("@", parametrosMetodo[i].Name!.Remove(0, 1)))
                    {
                        if (parametroProcedure.AceitaNulo && pParams[i] == null)
                        {
                            Parameters[contador] = new SqlParameter(parametroProcedure.Nome, DBNull.Value);
                        }

                        Parameters[contador] = new SqlParameter(parametroProcedure.Nome, pParams[i]);
                        contador++;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return ExecuteReader(pProcedureName, CommandType.StoredProcedure, Parameters);
        }

        /// <summary>
        /// Comando para gerar um Reader com passagem automatica de parametros, a partir dos atributos do objeto informado.
        /// </summary>
        /// <typeparam name="Typ">Tipo do objeto informado.</typeparam>
        /// <param name="pProcedureName">Nome da procedure a ser executada.</param>
        /// <param name="pObj">Objeto cujos atributos serao passados como parametro.</param>
        /// <returns>Resultado da execucao.</returns>
        public DataSet ExecuteReader<Typ>(string pProcedureName, Typ pObj)
        {
            List<ParametroSQLInfo> parametrosProcedure = SelecionarParametrosDeEntrada(pProcedureName);

            Type tipo = typeof(Typ);

            Parameters = new SqlParameter[parametrosProcedure.Count];

            int contador = 0;

            foreach (ParametroSQLInfo parametro in parametrosProcedure)
            {
                foreach (PropertyInfo propriedade in tipo.GetProperties())
                {
                    if (parametro.Nome == string.Concat("@", propriedade.Name))
                    {
                        if (parametro.AceitaNulo && propriedade.GetValue(pObj) == null)
                        {
                            Parameters[contador] = new SqlParameter(parametro.Nome, DBNull.Value);
                        }

                        Parameters[contador] = new SqlParameter(parametro.Nome, propriedade.GetValue(pObj));
                        contador++;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return ExecuteReader(pProcedureName, CommandType.StoredProcedure, Parameters);
        }

        /// <summary>
        /// Comando padrão para execução de comandos de Select com controle de conexão.
        /// </summary>
        /// <param name="pSqlCommand">Comando SQL</param>
        /// <param name="pCommandType">Tipo de Comando</param>
        /// <param name="pParameters">Parametros do Comando</param>
        /// <returns>SqlDataReader</returns>
        public DataSet ExecuteReader(string pSqlCommand, System.Data.CommandType pCommandType, SqlParameter[] pParameters)
        {
            try
            {
                using (connection)
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    Command = new SqlCommand(pSqlCommand, connection);
                    Command.CommandType = pCommandType;
                    Command.CommandTimeout = 60;

                    if (pParameters != null)
                    {
                        Command.Parameters.AddRange(pParameters);
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(Command);

                    DataSet ds = new DataSet();

                    sda.Fill(ds);

                    return ds;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// Comando de execução sem parâmetros.
        /// </summary>
        /// <param name="pSqlCommand">A instrução SQL a ser executada.</param>
        /// <param name="pCommandType">O tipo de comando.</param>
        /// <returns> Um DataSet contendo os resultados da consulta.</returns>
        public DataSet ExecuteReader(string pSqlCommand, System.Data.CommandType pCommandType)
        {
            // Inicializa os parâmetros como um array vazio
            parameters = new SqlParameter[0];

            // Chama o método ExecuteReader com os parâmetros fornecidos
            return ExecuteReader(pSqlCommand, pCommandType, parameters);
        }

        #endregion

        #region Métodos para conversão de dados do banco

        /// <summary>
        /// Converte os resultados de um DataTable em uma lista de objetos do tipo especificado.
        /// </summary>
        /// <typeparam name="Typ">Tipo do objeto a ser convertido.</typeparam>
        /// <param name="dt">DataTable contendo os resultados a serem convertidos.</param>
        /// <returns> Uma lista de objetos do tipo especificado, representando os dados do DataTable.</returns>
        protected List<Typ> ConverteParaLista<Typ>(DataTable dt)
        {
            List<Typ> lst = new List<Typ>();

            // Percorre através das linhas do DataTable
            foreach (DataRow row in dt.Rows)
            {
                // Converte cada linha para um objeto do tipo especificado
                Typ item = PegaItem<Typ>(row);

                // Adiciona o objeto à lista
                lst.Add(item);
            }

            return lst;
        }

        /// <summary>
        /// Converte uma DataRow em um objeto do tipo especificado.
        /// </summary>
        /// <typeparam name="Typ">Tipo do objeto a ser criado.</typeparam>
        /// <param name="registro">DataRow contendo os dados a serem convertidos.</param>
        /// <returns>Um objeto do tipo especificado, representando os dados da DataRow.</returns>
        protected Typ PegaItem<Typ>(DataRow registro)
        {
            Type temp = typeof(Typ);

            Typ obj = Activator.CreateInstance<Typ>();

            // Percorre sobre as colunas da DataRow e as propriedades do objeto
            foreach (DataColumn coluna in registro.Table.Columns)
            {
                foreach (PropertyInfo propriedade in temp.GetProperties())
                {
                    // Verifica se o nome da propriedade corresponde ao nome da coluna
                    if (propriedade.Name == coluna.ColumnName)
                    {
                        // Verifica se o valor na coluna não é DBNull.Value antes de atribuir à propriedade
                        if (registro[coluna.ColumnName] != DBNull.Value)
                        {
                            propriedade.SetValue(obj, registro[coluna.ColumnName], null);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// Retorna os parametros de entrada associados a uma stored procedure específica do banco de dados.
        /// </summary>
        /// <param name="pNomeProcedure">Nome da stored procedure para a qual os parâmetros de entrada serão obtidos.</param>
        /// <returns>Uma lista de objetos ParametroSQLInfo que representa os parâmetros de entrada da stored procedure.</returns>
        private List<ParametroSQLInfo> SelecionarParametrosDeEntrada(string pNomeProcedure)
        {
            try
            {
                List<ParametroSQLInfo> parametros = new List<ParametroSQLInfo>();
                DataTable dt = new DataTable();

                using (Connection)
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    Command = new SqlCommand("usp_SelecionarParametrosProcedure", connection);
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 60;

                    Command.Parameters.Add(new SqlParameter() { ParameterName = "@NomeProcedure", Value = pNomeProcedure });

                    SqlDataAdapter sda = new SqlDataAdapter(Command);

                    sda.Fill(dt);
                }

                parametros = ConverteParaLista<ParametroSQLInfo>(dt);

                return parametros;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion  
    }
}
