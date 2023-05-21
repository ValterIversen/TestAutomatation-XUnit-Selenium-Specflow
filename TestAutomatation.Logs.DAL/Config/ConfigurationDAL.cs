using Microsoft.Extensions.Configuration;

namespace TestAutomatation.Logs.DAL.Config
{
    public class ConfigurationDAL
    {

        private readonly IConfiguration _config;

        public ConfigurationDAL()
        {
            _config = new ConfigurationBuilder().AddJsonFile("dalsettings.json").Build();
        }

        #region String de conexao
        public string StringConnection => _config.GetValue<string>("Logs:StringConexao");
        #endregion
    }
}
