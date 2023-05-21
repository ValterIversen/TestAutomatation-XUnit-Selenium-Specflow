
using System;
using TechTalk.SpecFlow;
using TestAutomatation.Logs.DAL.LogsTestes;
using TestAutomatation.SiteChacara.Config;
using Xunit;

namespace TestAutomatation.SiteChacara.Steps
{
    [Binding]
    public class BeforeAfter
    {
        [AfterTestRun]
        public static void WriteLogs()
        {
            
            /*
            Assert.True(ManipulacaoLogs.GravarLogs());
            */
        }

        [BeforeTestRun]
        public static void CreateLogsBatch()
        {
            /*
            ConfigurationHelper Configuration = new();

            try
            {
                Assert.True(ManipulacaoLogs.CriarLote(Configuration.ProjectID));
            }
            catch (Exception)
            {
                throw new Exception(message: "Error creating logs batch");
            }
            */
        }
    }
}
