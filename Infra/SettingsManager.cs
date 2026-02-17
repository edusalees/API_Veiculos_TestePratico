using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace API_Veiculos.Infra
{
    public class SettingsManager
    {
        public IConfigurationRoot GetConfiguration()
        {
            string path = Directory.GetCurrentDirectory();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appSettings.json").Build();

            return configuration;
        }

        public string GetConnectionStringConfig()
        {
            IConfigurationRoot configuration = GetConfiguration();

            string connectionString = configuration.GetValue<string>("Database:SqlServer_ConnectionString");

            return connectionString;
        }

        public bool GetUseSqlServerConfig()
        {
            IConfigurationRoot configuration = GetConfiguration();

            bool connectionString = configuration.GetValue<bool>("UseSqlServer");

            return connectionString;
        }
    }
}
