using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TestAutomatation.SiteChacara.Config;
using TestAutomatation.SiteChacara.Pages;
using TestAutomatation.SiteChacara.Screens;
using TestAutomatation.SiteChacara.Validations;

namespace TestAutomatation.SiteChacara.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly SCFixture _scFixture;
        private readonly WhatsappApi _whastappApiPage;
        private readonly CommonValidations _commonValidations;

        public CommonSteps(SCFixture scFixture, CommonValidations commonValidations)
        {
            _scFixture = scFixture;
            _commonValidations = commonValidations;
            _whastappApiPage = new WhatsappApi(scFixture.BrowserHelper);
        }


        #region Then
        [Then(@"the data must be sent via whatsapp")]
        public void ThenTheDataMustBeSentViaWhatsapp()
        {
            try
            {
                string url = _whastappApiPage.GetUrl();

                _commonValidations.ValidateBookingUrl(url);                

                _scFixture.AddLog($"The booking was done successfully.", $"{GetType().Name}.{_scFixture.GetActualMethodName()}", true);
            }
            catch (Exception ex)
            {
                _scFixture.AddLog(ex.Message, $"{GetType().Name}.{_scFixture.GetActualMethodName()}", false);

                throw;
            }
        }
        #endregion
    }
}
