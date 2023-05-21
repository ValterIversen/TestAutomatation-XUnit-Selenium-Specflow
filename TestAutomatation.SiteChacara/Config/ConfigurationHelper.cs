using Microsoft.Extensions.Configuration;
using System;

namespace TestAutomatation.SiteChacara.Config
{
    public class ConfigurationHelper
    {
        private readonly IConfiguration _config;

        public ConfigurationHelper()
        {
            _config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();
        }

        #region ProjectProperties

        public int ProjectID => Convert.ToInt32(_config.GetValue<string>("ProjectID"));
        public string Version => _config.GetValue<string>("Version");
        private static string FolderPath => Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        public string FolderPicture => $"{FolderPath}{_config.GetSection("FolderPicture").Value}";

        #endregion

        #region ConfiguracoesGerais
        public bool Headless => _config.GetValue<bool>("Selenium:Headless");

        #endregion ConfiguracoesGerais

        #region SiteChacara
        public string SCBaseUrl => _config.GetValue<string>("SiteChacara:SCBaseUrl");
        #endregion
    }
}
