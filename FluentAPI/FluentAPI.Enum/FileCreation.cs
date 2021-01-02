namespace FluentAPI.Enum
{
    /// <summary>
    /// Gibt an, ob eine Datei neu erzeugt oder Inhalt angehangen werden soll
    /// </summary>
    public enum FileCreation
    {
        /// <summary>
        /// Existiert keine Datei, so wird eine neue Datei angelegt, ansonsten wird der Inhalt angehangen
        /// </summary>
        Anhängen,

        /// <summary>
        /// Legt eine neue Datei an. Eine vorhandene Datei wird ohne Vorwarnung überschrieben
        /// </summary>
        Überschreiben
    }
}