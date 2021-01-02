using FluentAPI.Enum;
using System;
using System.IO;
using System.Text;

namespace FluentAPI
{
    /// <summary>
    /// Die FileLineWriter Klasse implementiert die interne DSL zum Schreiben in eine Datei
    /// </summary>
    public class FileLineWriter : IFileLineWriter
    {
        /// <summary>
        /// Der Dateipfad
        /// </summary>
        protected string FilePath { get; set; }

        /// <summary>
        /// Der Dateiname
        /// </summary>
        protected string FileName { get; set; }

        /// <summary>
        /// Die zu verwendene Kodierung
        /// </summary>
        protected FileEncoding FileEncoding { get; set; }

        /// <summary>
        /// Festlegun, ob die Datei neu erzeugt oder erweitert wird
        /// </summary>
        protected FileCreation FileCreation { get; set; }

        /// <summary>
        /// Der Ausgabetext
        /// </summary>
        protected string LineContent { get; set; }

        /// <summary>
        /// Das aktuelle FileLineWriterObject (Value Object)
        /// </summary>
        protected FileLineWriterObject CurrentFileLineWriterObject { get; set; }

        /// <summary>
        /// True, wenn der letzte Schreibvorgang erfolgreich war, sonst False
        /// </summary>
        protected bool CurrentState { get; set; }

        /// <summary>
        /// Eine Instanz von IConfiguration
        /// </summary>
        private readonly FluentAPI.Configuration.IConfiguration _configuration;

        /// <summary>
        /// Der Konstruktor der Klasse
        /// </summary>
        /// <param name="configuration">Eine Instanz von IConfiguration via Dependencies Injection</param>
        public FileLineWriter(FluentAPI.Configuration.IConfiguration configuration)
        {
            this._configuration = configuration;
            SetzeDefaultwerte();
        }

        /// <summary>
        /// Gibt zurück, ob der letzte Aufruf der FUnktion Schreibe erfolgreich war
        /// </summary>
        /// <returns>True, wenn es zu keinem Fehler kam, ansonsten False</returns>
        public bool WarDieAktionErfolgreich()
        {
            return CurrentState;
        }

        /// <summary>
        /// Legt die Art der Ausgabe fest. Mögliche Arten sind Anhängen (und ggf Erzeugen) oder Überschreiben 
        /// </summary>
        /// <param name="auswahl">Gibt an, ob die Ausgabe in eine neue Datei ausgegeben werden soll, oder in eine vorhandene</param>
        /// <returns>Eine Instanz von IFileLineWriter</returns>
        public IFileLineWriter AusgabeDurch(FileCreation auswahl)
        {
            FileCreation = auswahl;
            return AktualisiereEigenschafte(FilePath, FileName, FileEncoding, FileCreation, LineContent);
        }

        /// <summary>
        /// Definiert anhand der übergebenen Bezeichnungen einen Pfad für die Ausgabe 
        /// </summary>
        /// <param name="angabeVerzeichnis">Eine kommaseparierte Liste von Namen
        /// Beispiel: ("c:\", "Ausgabe") erzeugt unter Windows c:\Ausgabe</param>
        /// <returns>Eine Instanz von IFileLineWriter</returns>>
        public IFileLineWriter SpeicherInVerzeichnis(params string[] angabeVerzeichnis)
        {
            String filePath = "";
            foreach (string part in angabeVerzeichnis)
            {
                filePath = System.IO.Path.Join(filePath, part);
            }

            FilePath = filePath;
            return AktualisiereEigenschafte(FilePath, FileName, FileEncoding, FileCreation, LineContent);
        }

        /// <summary>
        /// Legt den zu verwendenen Dateinamen fest
        /// </summary>
        /// <param name="angabeDateiname"Der verwendete Namen</param>
        /// <returns>Eine Instanz von IFileLineWriter</returns>
        public IFileLineWriter VerwendeDateinamen(string angabeDateiname)
        {
            FileName = angabeDateiname;
            return AktualisiereEigenschafte(FilePath, FileName, FileEncoding, FileCreation, LineContent);
        }

        /// <summary>
        /// Legt die zu verwendene Kodierung fest
        /// </summary>
        /// <param name="auswahl">Die verwendete Kodierung</param>
        /// <returns>Eine Instanz von IFileLineWriter</returns>
        public IFileLineWriter VerwendeZurCodierung(FileEncoding auswahl)
        {
            FileEncoding = auswahl;
            return AktualisiereEigenschafte(FilePath, FileName, FileEncoding, FileCreation, LineContent);
        }

        /// <summary>
        /// Führt den Schreibvorgang aus und gibt den übergebenen Text aus
        /// </summary>
        /// <param name="ausgabetext"Der auszugebene Text</param>
        /// <returns>Eine Instanz von IFileLineWriter</returns>
        public IFileLineWriter Schreibe(string ausgabetext)
        {
            LineContent = ausgabetext;
            AktualisiereEigenschafte(FilePath, FileName, FileEncoding, FileCreation, LineContent);

            try
            {
                if (CurrentFileLineWriterObject != null)
                {
                    CurrentState = WriteFile(CurrentFileLineWriterObject);
                }
                else
                {
                    CurrentState = false;
                }
            }
            catch (Exception e)
            {
                CurrentState = false;
            }

            return AktualisiereEigenschafte(FilePath, FileName, FileEncoding, FileCreation, LineContent);
        }

        /// <summary>
        /// Erzeugt eine Datei und gibt den in writerObj hinterlegten Inhalt aus
        /// </summary>
        /// <param name="writerObj">Das aktuelle FileLineWriterObject</param>
        /// <returns>True, wenn die Ausgabe erfolgreich war, sonst False</returns>
        private bool WriteFile(FileLineWriterObject writerObj)
        {
            string vollständigerPfad = System.IO.Path.Join(writerObj.FilePath,
                writerObj.FileName);

            var aktuellerMode = GetAktuellerMode(writerObj);

            var aktuelleCodierung = GetAktuelleCodierung(writerObj);

            if (GetCurrentDirectory(writerObj).Exists)
            {
                using (FileStream fileStream = new FileStream(vollständigerPfad, aktuellerMode))
                {
                    StreamWriter ausgabe = new StreamWriter(fileStream, aktuelleCodierung);
                    ausgabe.WriteLine(CurrentFileLineWriterObject.LineContent);
                    ausgabe.Flush();
                    ausgabe.Close();
                }

                CurrentState = true;
            }
            else
            {
                CurrentState = false;
            }

            return CurrentState;
        }

        /// <summary>
        /// Übersetzt die DSL in Bezug zur Überschreibung auf die zu verwendenen Eigenschaften (FileMode)
        /// </summary>
        /// <param name="writerObj">Das aktuelle FileLineWriterObject</param>
        /// <returns>Gibt die zu verwendene Codierung zurück</returns>
        private FileMode GetAktuellerMode(FileLineWriterObject writerObj)
        {
            FileMode aktuellerMode = FileMode.Append;
            if (writerObj.FileCreation == FileCreation.Überschreiben)
            {
                aktuellerMode = FileMode.Create;
            }

            return aktuellerMode;
        }

        /// <summary>
        /// Übersetzt die DSL in Bezug zur Kodierung auf die zu verwendenen Eigenschaften (Encoding)
        /// </summary>
        /// <param name="writerObj">Das aktuelle FileLineWriterObject</param>
        /// <returns>Gibt die zu verwendene Codierung zurück</returns>
        private Encoding GetAktuelleCodierung(FileLineWriterObject writerObj)
        {
            Encoding aktuelleCodierung = Encoding.Default;
            switch (writerObj.FileEncoding)
            {
                case FileEncoding.Utf8:
                    aktuelleCodierung = Encoding.UTF8;
                    break;
                case FileEncoding.Utf16:
                    aktuelleCodierung = Encoding.Unicode;
                    break;
                case FileEncoding.Utf32:
                    aktuelleCodierung = Encoding.UTF32;
                    break;
            }

            return aktuelleCodierung;
        }

        /// <summary>
        /// Stellt sicher, dass das Zielverzeichnis existiert und legt ggf ein neues Verzeichnis an
        /// </summary>
        /// <param name="writerObj">Das aktuelle FileLineWriterObject</param>
        /// <returns>Ein Instanz von DirectoryInfo für das Zielverzeichnis</returns>
        private DirectoryInfo GetCurrentDirectory(FileLineWriterObject writerObj)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(writerObj.FilePath);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            return dirInfo;
        }

        /// <summary>
        /// Belegt die Eigenschaften der Klasse mit Defaultwerten. Standardmäßig wird ais Ausgabeort das
        /// eigene Verzeichnis verwendet. Die Kodierung richtet sich nach dem Standart des Frameworks und
        /// Dateien werden überschrieben. Der Name und der Ausgabetext richtet sich nach den Vorgaben der
        /// appsettings.json. 
        /// </summary>
        private void SetzeDefaultwerte()
        {
            FilePath = System.Environment.CurrentDirectory;
            FileName = _configuration.Get("defaultname");
            FileEncoding = FileEncoding.Auto;
            FileCreation = FileCreation.Überschreiben;
            LineContent = _configuration.Get("defaulttext");
        }

        /// <summary>
        /// Aktualisiert die Eigenschaften der Klassen und erzeugt ein neues FileLineWriterObject 
        /// </summary>
        /// <param name="filePath">Der Dateipfad</param>
        /// <param name="fileName">Der Dateiname</param>
        /// <param name="fileEncoding">Die zu verwendene Kodierung</param>
        /// <param name="fileCreation">Festlegung ob die Datei neu erzeugt oder erweitert wird</param>
        /// <param name="lineContent">Der Ausgabetext</param>
        /// <returns>Eine Instanz von IFileLineWriter</returns>
        private IFileLineWriter AktualisiereEigenschafte(string filePath, string fileName, FileEncoding fileEncoding,
            FileCreation fileCreation, string lineContent)
        {
            this.FilePath = filePath;
            this.FileName = fileName;
            this.FileEncoding = fileEncoding;
            this.FileCreation = fileCreation;
            this.LineContent = lineContent;

            this.CurrentFileLineWriterObject =
                new FileLineWriterObject(FilePath, FileName, FileEncoding, FileCreation, LineContent);

            return this;
        }
    }
}