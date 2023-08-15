using TestAutomatation.SiteChacara.Support;

namespace TestAutomatation.SiteChacara.Screens
{
    public abstract class PageObjectModel
    {
        protected readonly SeleniumHelper Helper;

        protected PageObjectModel(SeleniumHelper helper)
        {
            Helper = helper;
        }

        public string GetUrl()
        {
            return Helper.GetUrl();
        }

        public void GoToUrl(string url)
        {
            Helper.GoToUrl(url);
        }

        public void CloseBrowser()
        {
            Helper.Dispose();
        }
    }
}
