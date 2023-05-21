using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using TestAutomatation.Logs.DAL.Config;

namespace TestAutomatation.Logs.DAL.LogsTestes
{
    public static class ManipulacaoLogs
    {
        //Lista com thread segura, não deixa sobrepor ou ocorrer erros ao adicionar itens paralelamente
        //Funciona como FIFO, temo outras opções como o ConcurrentBag que é sem ordem

        private static Lote lote;

        public static void AdicionarLog(string Descricao, string Metodo, string Cenario, string TempoExecucao, bool Sucesso, string Versao)
        {
            try
            {
                Log log = new()
                {
                    Descricao = Descricao,
                    Metodo = Metodo,
                    DataExecucao = DateTime.Now,
                    Cenario = Cenario,
                    TempoExecucao = TempoExecucao,
                    Sucesso = Sucesso,
                    //A versão a gente pode pegar de um arquivo de configuração
                    Versao = Versao
                };

                lote.Logs.Enqueue(log);
            }
            catch (Exception)
            {
            }
        }

        //Armazena vários logs de uma só vez
        public static bool GravarLogs()
        {
            try
            {
                //Transformo a FIFO em lista normal
                List<Log> listaLogs = lote.Logs.ToList();
                if (listaLogs == null || listaLogs.Count == 0)
                    return false;
                AcessoDadosSqlServer acessoDadosSqlServer = new();

                //Separar os logs de 100 em 100 e gravar de 100 em 100
                //Como o .net core 5.0 não tem o .CHUNK, foi criado uma função para isso
                IEnumerable<List<Log>> chunks = listaLogs.ChunkBy(100);

                //
                foreach (List<Log> itens in chunks)
                {

                    //Inicio os valores que serão inseridos
                    var listaDeStrings = itens.Select(log => $"({lote.Lote_ID}, '{log.Descricao.Replace(@"'", "\"").WithMaxLength(200)}', '{log.Metodo.WithMaxLength(100)}', '{log.Cenario.WithMaxLength(100)}', '{log.DataExecucao.ToString(CultureInfo.CreateSpecificCulture("en-US"))}', '{log.Versao.WithMaxLength(10)}', {BoolToBit(log.Sucesso)}, '{log.TempoExecucao.WithMaxLength(8)}')");

                    var values = string.Join(",", listaDeStrings);

                    //Crio um query para inserir em batch por motivos de desempenho
                    string sqlQuery =
                    "BEGIN TRY " +
                    "BEGIN TRAN " +
                        "INSERT INTO LOGS " +
                            "(LOTE_ID, DESCRICAO, METODO, CENARIO, DATAEXECUCAO, VERSAO, SUCESSO, TEMPOEXECUCAO) " +
                        "OUTPUT INSERTED.LOG_ID  " +
                        "VALUES " +
                            values +
                        "COMMIT " +
                        "END TRY  " +
                    "BEGIN CATCH  " +
                        "ROLLBACK  " +
                        "SELECT ERROR_MESSAGE() AS Retorno  " +
                   "END CATCH";

                    //Envio para o banco e executo
                    acessoDadosSqlServer.LimparParametros();
                    string retorno = acessoDadosSqlServer.ExecutarManipulacao(CommandType.Text, sqlQuery);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool CriarLote(int ProjetoID)
        {
            try
            {
                lote = new()
                {
                    Projeto_ID = ProjetoID,
                    Logs = new ConcurrentQueue<Log>()
                };

                AcessoDadosSqlServer acessoDadosSqlServer = new();
                string HoraAtual = DateTime.Now.ToString(CultureInfo.CreateSpecificCulture("en-US"));
                string values = $"({lote.Projeto_ID}, '{HoraAtual}')";
                string sqlQuery =
                    "BEGIN TRY " +
                    "BEGIN TRAN " +
                        "INSERT INTO LOTE_LOGS " +
                            "(PROJETO_ID, HORAINICIO) " +
                        "OUTPUT INSERTED.LOTE_ID  " +
                        "VALUES " +
                            values +
                        "COMMIT " +
                        "END TRY  " +
                    "BEGIN CATCH  " +
                        "ROLLBACK  " +
                        "SELECT ERROR_MESSAGE() AS Retorno  " +
                   "END CATCH";

                //Envio para o banco e executo
                acessoDadosSqlServer.LimparParametros();
                lote.Lote_ID = int.Parse(acessoDadosSqlServer.ExecutarManipulacao(CommandType.Text, sqlQuery));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string WithMaxLength(this string value, int maxLength)
        {
            return value?[..Math.Min(value.Length, maxLength)];
        }

        private static int BoolToBit(bool valor)
        {
            return valor ? 1 : 0;
        }

        private static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }
}
