using System;

namespace TestAutomatation.Logs.DAL.LogsTestes
{
    public class Log
    {
        public int Log_ID { get; set; }
        public string Descricao { get; set; }
        public string Metodo { get; set; }
        public string Cenario { get; set; }
        public DateTime DataExecucao { get; set; }
        public string Versao { get; set; }
        public bool Sucesso { get; set; }
        public string TempoExecucao { get; set; }
    }
}
