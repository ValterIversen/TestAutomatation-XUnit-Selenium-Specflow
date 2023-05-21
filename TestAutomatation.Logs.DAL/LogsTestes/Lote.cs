using System.Collections.Concurrent;

namespace TestAutomatation.Logs.DAL.LogsTestes
{
    public class Lote
    {
        public int Lote_ID { get; set; }
        public int Projeto_ID { get; set; }
        public ConcurrentQueue<Log>? Logs { get; set; }
    }
}
