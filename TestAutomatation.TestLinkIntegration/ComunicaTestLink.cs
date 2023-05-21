using System;
using System.Collections.Generic;
using TestAutomatation.TestLinkIntegration.Config;
using TestLinkApi;
using Xunit;

namespace TestAutomatation.TestLinkIntegration
{
    public class ComunicaTestLink
    {
        private readonly ConfigTestLink Configuration;
        private readonly TestLink TestLinkConecta;
        private readonly bool TestMode;

        private TestPlan _planoTeste;
        private List<TestCaseId> _listTestCases;
        private string _nomeProjeto, _nomePlanoTeste, _casoTesteNome, _resultadoTeste;
        private int _casoTesteID;

        public ComunicaTestLink()
        {
            Configuration = new ConfigTestLink();
            TestLinkConecta = new(Configuration.ApyKey, Configuration.TestLinkUrl);
            TestMode = Configuration.TestModeTestLink;
        }

        public Tuple<string, bool> GravarTestLink(string versaoModulo, string nomeCenario, bool sucesso, string resultadoTeste)
        {
            try
            {
                if (nomeCenario.Length > 100)
                    throw new Exception();

                _resultadoTeste = resultadoTeste;

                if (!TestMode)
                {
                    _nomePlanoTeste = versaoModulo;
                    _casoTesteNome = nomeCenario;
                }
                else
                {
                    _nomeProjeto = "Administrativo";
                    _nomePlanoTeste = "zTestModeActivated (N/A)";
                    _casoTesteNome = "TestModeOn (N/A)";
                }

                _planoTeste = TestLinkConecta.getTestPlanByName(_nomeProjeto, _nomePlanoTeste);

                _listTestCases = TestLinkConecta.GetTestCaseIDByName(_casoTesteNome);

                //IF necessário, registra um log mais legível
                if (_listTestCases.Count > 0)
                    _casoTesteID = _listTestCases[0].id;

                MudarStatusCenario(sucesso);

                var resultIntegration = new Tuple<string, bool>("Gravado com sucesso", true);

                return resultIntegration;
            }
            catch (Exception ex)
            {
                string exception = ex.Message;

                var resultIntegration = new Tuple<string, bool>(exception, false);

                return resultIntegration;
            }
        }

        private void MudarStatusCenario(bool sucesso)
        {
            if (sucesso)
            {
                TestLinkConecta.ReportTCResult(_casoTesteID, _planoTeste.id, TestCaseStatus.Passed, 0, "", false, true, _resultadoTeste);

                if (!TestMode)
                    Assert.Equal("p", TestLinkConecta.GetLastExecutionResult(_planoTeste.id, _casoTesteID).status);
            }
            else
            {
                TestLinkConecta.ReportTCResult(_casoTesteID, _planoTeste.id, TestCaseStatus.Failed, 0, "", false, true, _resultadoTeste);

                if (!TestMode)
                    Assert.Equal("f", TestLinkConecta.GetLastExecutionResult(_planoTeste.id, _casoTesteID).status);
            }
        }
    }
}