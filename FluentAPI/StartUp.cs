using FluentAPI.Enum;

namespace FluentAPI
{
    public class StartUp
    {
        private IFileLineWriter _fileLineWriter;

        public StartUp(IFileLineWriter fileLineWriter)
        {
            _fileLineWriter = fileLineWriter;
        }

        public void Run()
        {
            var x = _fileLineWriter
                .AusgabeDurch(FileCreation.Überschreiben)
                .VerwendeDateinamen("Datei 1.txt")
                .VerwendeZurCodierung(FileEncoding.Auto)
                .SpeicherInVerzeichnis("c:", "FluentApiTest")
                .Schreibe("Ausgabe Datei 1");

            x.VerwendeDateinamen("Datei 2.txt")
                .Schreibe("Ausgabe Datei 2");

            bool status = x.WarDieAktionErfolgreich();
        }
    }
}