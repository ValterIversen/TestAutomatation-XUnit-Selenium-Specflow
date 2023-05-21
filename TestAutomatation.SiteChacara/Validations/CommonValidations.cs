using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomatation.SiteChacara.Config;
using TestAutomatation.SiteChacara.Pages;
using Xunit;

namespace TestAutomatation.SiteChacara.Validations
{
    public class CommonValidations
    {
        private readonly SCFixture _scFixture;

        public CommonValidations(SCFixture scFixture)
        {
            _scFixture = scFixture;
        }

        public void ValidateBookingUrl(string url)
        {
            string expectedMessage = $"Olá, meu nome {_scFixture.User.Name} - {_scFixture.User.Phone}.%0AEu tenho interesse em reservar a chácara Luz do Sol nos dias {_scFixture.Period.StartDate.ToString("dd/MM/yyyy")} até o dia {_scFixture.Period.FinalDate.ToString("dd/MM/yyyy")}.%0AQuanto fica a reserva?%20";

            Uri uri = new Uri("https://api.whatsapp.com/send?phone=5517991129720&text=" + expectedMessage);
            string expectedUrl = uri.AbsoluteUri;

            Assert.Equal(expectedUrl, url);            
        }

    }
}
