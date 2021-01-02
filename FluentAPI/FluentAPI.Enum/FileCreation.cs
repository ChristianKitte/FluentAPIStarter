namespace FluentAPI.Enum
{
    /// <summary>
    /// Gibt an, ob eine Datei neu erzeugt oder Inhalt angehangen werden soll
    /// </summary>
    public enum FileCreation
    {
        /// <summary>
        /// Existiert keine Datei, so wird eine neue Datei angelegt, ansonsten angehangen
        /// </summary>
        Anhängen,

        /// <summary>
        /// Überschreibt eine vorhandene Datei ohne Vorwarnung oder legt eine neue an
        /// </summary>
        Überschreiben
    }
}