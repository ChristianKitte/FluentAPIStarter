using FluentAPI.Enum;

namespace FluentAPI
{
    /// <summary>
    /// Hält die Daten für die Ausgabe in eine Datei in einem unveränderbares Datenobjekt (Value Object) 
    /// </summary>
    public record FileLineWriterObject
    {
        /// <summary>
        /// Der Pfad zur Datei              
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Der Dateiname
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Die genutzte Codierung
        /// </summary>
        public FileEncoding FileEncoding { get; }

        /// <summary>
        /// Legt fest, ob die Ausgabe immer in eine neue Datei erfolgen soll, oder wenn möglich angehangen werden soll.
        /// </summary>
        public FileCreation FileCreation { get; }

        /// <summary>
        /// Der Inhalt der auszugebenen Zeile 
        /// </summary>
        public string LineContent { get; }

        /// <summary>
        /// Der Konstruktor
        /// </summary>
        /// <param name="filePath">Der Pfad zur Datei</param>
        /// <param name="fileName">Der Dateiname</param>
        /// <param name="fileEncoding">Die genutzte Codierung</param>
        /// <param name="fileCreation">Legt fest, ob die Ausgabe immer in eine neue Datei erfolgen soll, oder wenn möglich angehangen werden soll.</param>
        /// <param name="lineContent">Der Inhalt der auszugebenen Zeile</param>
        public FileLineWriterObject(string filePath, string fileName, FileEncoding fileEncoding,
            FileCreation fileCreation, string lineContent) =>
            (FilePath, FileName, FileEncoding, FileCreation, LineContent) =
            (filePath, fileName, fileEncoding, fileCreation, lineContent);
    }
}