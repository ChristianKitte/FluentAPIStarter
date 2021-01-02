using FluentAPI.Enum;

namespace FluentAPI
{
    /// <summary>
    /// Der Startpunkt der Anwendung. Hierbei handelt es sich nicht um die reguläre ASP StartUp Klasse.
    /// </summary>
    public class StartUp
    {
        /// <summary>
        /// Hält eine Instanz von IFileLineWriter
        /// </summary>
        private IFileLineWriter _fileLineWriter;

        /// <summary>
        /// Der Konstruktor der Klasse
        /// </summary>
        /// <param name="fileLineWriter">Eine Instanz von IFileLineWriter via Dependencies Injection</param>
        public StartUp(IFileLineWriter fileLineWriter)
        {
            _fileLineWriter = fileLineWriter;
        }

        /// <summary>
        /// Die Startmethode der Anwendung mit Beipielcode zur Verwendung der DSL
        /// </summary>
        public void Run()
        {
            var writer = _fileLineWriter
                .AusgabeDurch(FileCreation.Überschreiben)
                .VerwendeDateinamen("Datei 1.txt")
                .VerwendeZurCodierung(FileEncoding.Auto)
                .SpeicherInVerzeichnis("c:", "FluentApi")
                .Schreibe("Ausgabe Datei 1");

            writer.VerwendeDateinamen("Datei 2.txt")
                .Schreibe("Ausgabe Datei 2");

            bool status = writer.WarDieAktionErfolgreich();
        }
    }
}