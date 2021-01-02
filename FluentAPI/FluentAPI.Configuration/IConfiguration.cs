namespace FluentAPI.Configuration
{
    /// <summary>
    /// Hilfsklasse für den einfachen Zugriff auf Basis der.NET Unterstützung für JSON basierte Konfigurationen
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Liefert den Wert eines Schlüssels aus der Konfiguration zurück
        /// </summary>
        /// <param name="key">Der Schlüssel, dessen Wert zurück geliefert werden soll</param>
        /// <returns></returns>
        string Get(string key);
    }
}