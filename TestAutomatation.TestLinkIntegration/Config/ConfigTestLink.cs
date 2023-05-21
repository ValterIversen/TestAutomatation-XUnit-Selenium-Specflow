using Microsoft.Extensions.Configuration;

namespace TestAutomatation.TestLinkIntegration.Config
{
    public class ConfigTestLink
    {
        private readonly IConfiguration _config;

        public ConfigTestLink()
        {
            _config = new ConfigurationBuilder().AddJsonFile("testlinksettings.json").Build();
        }

        public bool TestModeTestLink => _config.GetValue<bool>("TestLink:TestMode");
        public string TestLinkUrl => _config.GetValue<string>("TestLink:TestLinkUrl");
        public string ApyKey => _config.GetValue<string>("TestLink:ApyKey");
    }
}
