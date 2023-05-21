using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomatation.SiteChacara.Screens;
using TestAutomatation.SiteChacara.Support;

namespace TestAutomatation.SiteChacara.Pages
{
    public class WhatsappApi : PageObjectModel
    {
        public WhatsappApi(SeleniumHelper helper) : base(helper) { }

        private const string classNameMessage = "_9vd5";
        
        public string GetMessage()
        {
            return Helper.GetTextElementById(classNameMessage);
        }

        internal void CloseAlert()
        {
            Helper.DismissChormeAlert();
        }
    }
}
