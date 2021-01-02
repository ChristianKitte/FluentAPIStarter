using Microsoft.Extensions.Configuration;

namespace FluentAPI.Configuration
{
    /// <summary>
    /// Hilfsklasse für den einfachen Zugriff auf Basis der.NET Unterstützung für JSON basierte Konfigurationen
    /// </summary>
    public class AppConfiguration : IConfiguration
    {
        private Microsoft.Extensions.Configuration.IConfiguration _Configuration;

        /// <summary>
        /// Konstruktor der Klasse
        /// </summary>
        public AppConfiguration()
        {
            _Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
        }

        /// <summary>
        /// Liefert den Wert eines Schlüssels aus der Konfiguration zurück
        /// </summary>
        /// <param name="key">Der Schlüssel, dessen Wert zurück geliefert werden soll</param>
        /// <returns></returns>
        public string Get(string key)
        {
            return _Configuration[key];
        }
    }
}