using Microsoft.Extensions.Configuration;

namespace FluentAPI.Configuration
{
    public class AppConfiguration : IConfiguration
    {
        private Microsoft.Extensions.Configuration.IConfiguration _Configuration;

        public AppConfiguration()
        {
            _Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
        }

        public string Get(string key)
        {
            return _Configuration[key];
        }
    }
}