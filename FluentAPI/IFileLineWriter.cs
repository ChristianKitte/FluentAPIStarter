using FluentAPI.Enum;

namespace FluentAPI
{
    public interface IFileLineWriter
    {
        /// <summary>
        /// Gibt zurück, ob der letzte Aufruf der FUnktion Schreibe erfolgreich war
        /// </summary>
        /// <returns>True, wenn es zu keinem Fehler kam, ansonsten False</returns>
        bool WarDieAktionErfolgreich();

        /// <summary>
        /// Legt die Art der Ausgabe fest. Mögliche Arten sind Anhängen (und ggf Erzeugen) oder Überschreiben 
        /// </summary>
        /// <param name="auswahl">Gibt an, ob die Ausgabe in eine neue Datei ausgegeben werden soll, oder in eine vorhandene</param>
        /// <returns>Eine Instanz von IFileLineWriter</returns>
        IFileLineWriter AusgabeDurch(FileCreation auswahl);

        /// <summary>
        /// Definiert anhand der übergebenen Bezeichnungen einen Pfad für die Ausgabe 
        /// </summary>
        /// <param name="angabeVerzeichnis">Eine kommaseparierte Liste von Namen
        /// Beispiel: ("c:\", "Ausgabe") erzeugt unter Windows c:\Ausgabe</param>
        /// <returns>Eine Instanz von IFileLineWriter</returns>>
        IFileLineWriter SpeicherInVerzeichnis(params string[] angabeVerzeichnis);

        /// <summary>
        /// Legt den zu verwendenen Dateinamen fest
        /// </summary>
        /// <param name="angabeDateiname"Der verwendete Namen</param>
        /// <returns>Eine Instanz von IFileLineWriter</returns>
        IFileLineWriter VerwendeDateinamen(string angabeDateiname);

        /// <summary>
        /// Legt die zu verwendene Kodierung fest
        /// </summary>
        /// <param name="auswahl">Die verwendete Kodierung</param>
        /// <returns>Eine Instanz von IFileLineWriter</returns>
        IFileLineWriter VerwendeZurCodierung(FileEncoding auswahl);

        /// <summary>
        /// Führt den Schreibvorgang aus und gibt den übergebenen Text aus
        /// </summary>
        /// <param name="ausgabetext"Der auszugebene Text</param>
        /// <returns>Eine Instanz von IFileLineWriter</returns>
        IFileLineWriter Schreibe(string ausgabetext);
    }
}