using Bogus;
using Bogus.Extensions.Brazil;
using System;
using System.Diagnostics;
using TechTalk.SpecFlow;
using TestAutomatation.Logs.DAL.LogsTestes;
using TestAutomatation.SiteChacara.Entities;
using TestAutomatation.SiteChacara.Support;
using TestAutomatation.TestLinkIntegration;
using Xunit;

namespace TestAutomatation.SiteChacara.Config
{
    [CollectionDefinition(nameof(SCFixtureCollection))]
    public class SCFixtureCollection : ICollectionFixture<SCFixture> { }

    public class SCFixture
    {
        public SeleniumHelper BrowserHelper;
        private readonly string scenarioTitle;
        public Stopwatch TempoTeste;
        public readonly ConfigurationHelper Configuration;

        public SCFixture(ScenarioContext scenarioContext)
        {
            BrowserHelper = new SeleniumHelper(Browser.Firefox);

            TempoTeste = new Stopwatch();
            TempoTeste.Start();
            scenarioTitle = scenarioContext.ScenarioInfo.Title;
            Configuration = new ConfigurationHelper();
        }

        #region Properties
        public User User;
        public Period Period;
        public Book Book;
        #endregion

        #region Gerenciamento de Logs

        /// <summary>
        /// Retorna o nome do método que o chamou. Utilizado para a gravação de Logs
        /// </summary>
        public string GetActualMethodName([System.Runtime.CompilerServices.CallerMemberName] string name = null) => name;

        public void AddLog(string Descricao, string Metodo, bool Sucesso)
        {
            TempoTeste.Stop();
            TimeSpan ts = TempoTeste.Elapsed;

            string tempoTeste = string.Format("{0:00}:{1:00}.{2:00}",
            ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            /*
            ManipulacaoLogs.AdicionarLog(Descricao, Metodo, scenarioTitle, tempoTeste, Sucesso, Configuration.Version);
            

            CommunicateWithTestlink(Configuration.Version, Sucesso, tempoTeste, Descricao);
            */
        }
        #endregion

        #region TestLink
        private void CommunicateWithTestlink(string versaoModulo, bool sucesso, string tempoTeste, string descricao)
        {
            /*
            ComunicaTestLink comunicaTestLink = new();

            Tuple<string, bool> resultTestLink = comunicaTestLink.GravarTestLink(versaoModulo, scenarioTitle, sucesso, descricao);

            if (!resultTestLink.Item2)
                ManipulacaoLogs.AdicionarLog(resultTestLink.Item1, "ComunicarTestLink", scenarioTitle, tempoTeste, resultTestLink.Item2, versaoModulo);

            try
            {
                Assert.True(resultTestLink.Item2);
            }
            catch
            {
                throw new Exception(message: "Ocorreu um erro ao tentar comunicar com o TestLink.");
            }
            */

        }
        #endregion
    }
}
