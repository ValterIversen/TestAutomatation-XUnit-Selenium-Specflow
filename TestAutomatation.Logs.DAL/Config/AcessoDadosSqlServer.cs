using System;
using System.Data;
using System.Data.SqlClient;

namespace TestAutomatation.Logs.DAL.Config
{
    internal class AcessoDadosSqlServer
    {
        //Cria conexao
        private static SqlConnection CriarConexao()
        {
            ConfigurationDAL config = new();
            return new(config.StringConnection);
        }

        //Parametros que vão para banco
        // Aqui criamos uma variavel da qual armazenaremos os parametros das tabelas criadas no sql server
        private readonly SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        //Este método limpa os parametros que estão armazenados na variavel sqlParameterCollection
        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }

        //Aqui ele adiciona os parametros à variavel sqlParameterCollection
        public void AdicionarParametros(string nomeParametro, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }

        //Persistência - Inserir, Alterar e Excluir
        public string ExecutarManipulacao(CommandType commandType, string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //Cria a conexao
                SqlConnection sqlConnection = CriarConexao();
                //Abrir conexao
                sqlConnection.Open();
                //Criar o comando que vai levar a informacao para o banco
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando as coisas dentro do comando
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 90; //Em segundos

                //Adiciona os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection) sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));


                return sqlCommand.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Consultar registros do banco de dados

        public DataTable ExecutarConsulta(CommandType commandType, string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //Cria a conexao
                SqlConnection sqlConnection = CriarConexao();
                //Abrir conexao
                sqlConnection.Open();
                //Criar o comando que vai levar a informacao para o banco
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando as coisas dentro do comando
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; //Em segundos

                //Adiciona os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection) sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));

                //Cria o adaptador
                SqlDataAdapter sqlDataAdapter = new(sqlCommand);
                //DataTable - Tabela de Dados vazia onde vou colocar os dados que vem do banco
                DataTable dataTable = new();
                //Manda o comando ir até o banco e buscar os dados e o adaptador preencher o datatable
                sqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }
    }

}
