using FluentAPI.Enum;
using System;
using System.IO;
using System.Text;

namespace FluentAPI
{
    public class FileLineWriter : IFileLineWriter
    {
        protected string FilePath { get; set; }
        protected string FileName { get; set; }
        protected FileEncoding FileEncoding { get; set; }
        protected FileCreation FileCreation { get; set; }
        protected string LineContent { get; set; }
        protected FileLineWriterObject AktuellesFileLineWriterObject { get; set; }
        protected bool CurrentState { get; set; }

        private readonly FluentAPI.Configuration.IConfiguration _configuration;

        public FileLineWriter(FluentAPI.Configuration.IConfiguration configuration)
        {
            this._configuration = configuration;
            SetzeDefaultwerte();
        }

        public bool WarDieAktionErfolgreich()
        {
            return CurrentState;
        }

        public IFileLineWriter AusgabeDurch(FileCreation fileCreation)
        {
            FileCreation = fileCreation;
            return AktualisiereEigenschafte(FilePath, FileName, FileEncoding, FileCreation, LineContent);
        }

        public IFileLineWriter SpeicherInVerzeichnis(params string[] filePathParts)
        {
            String filePath = "";
            foreach (string part in filePathParts)
            {
                filePath = System.IO.Path.Join(filePath, part);
            }

            FilePath = filePath;
            return AktualisiereEigenschafte(FilePath, FileName, FileEncoding, FileCreation, LineContent);
        }

        public IFileLineWriter VerwendeDateinamen(string fileName)
        {
            FileName = fileName;
            return AktualisiereEigenschafte(FilePath, FileName, FileEncoding, FileCreation, LineContent);
        }

        public IFileLineWriter VerwendeZurCodierung(FileEncoding fileEncoding)
        {
            FileEncoding = fileEncoding;
            return AktualisiereEigenschafte(FilePath, FileName, FileEncoding, FileCreation, LineContent);
        }

        public IFileLineWriter Schreibe(string lineContent)
        {
            LineContent = lineContent;
            AktualisiereEigenschafte(FilePath, FileName, FileEncoding, FileCreation, LineContent);

            try
            {
                if (AktuellesFileLineWriterObject != null)
                {
                    string vollständigerPfad = System.IO.Path.Join(AktuellesFileLineWriterObject.FilePath,
                        AktuellesFileLineWriterObject.FileName);

                    FileMode aktuellerMode = FileMode.Append;
                    if (FileCreation == FileCreation.Überschreiben)
                    {
                        aktuellerMode = FileMode.Create;
                    }

                    Encoding aktuelleCodierung = Encoding.Default;
                    switch (AktuellesFileLineWriterObject.FileEncoding)
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

                    if (!System.IO.Directory.Exists(AktuellesFileLineWriterObject.FilePath))
                    {
                        System.IO.Directory.CreateDirectory(AktuellesFileLineWriterObject.FilePath);
                    }

                    if (System.IO.Directory.Exists(AktuellesFileLineWriterObject.FilePath))
                    {
                        using (FileStream fileStream = new FileStream(vollständigerPfad, aktuellerMode))
                        {
                            StreamWriter ausgabe = new StreamWriter(fileStream, aktuelleCodierung);
                            ausgabe.WriteLine(AktuellesFileLineWriterObject.LineContent);
                            ausgabe.Flush();
                            ausgabe.Close();
                        }

                        CurrentState = true;
                    }
                    else
                    {
                        CurrentState = false;
                    }
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

        private void SetzeDefaultwerte()
        {
            FilePath = System.Environment.CurrentDirectory;
            FileName = _configuration.Get("defaultname");
            FileEncoding = FileEncoding.Auto;
            FileCreation = FileCreation.Überschreiben;
            LineContent = _configuration.Get("defaulttext");
        }

        private IFileLineWriter AktualisiereEigenschafte(string filePath, string fileName, FileEncoding fileEncoding,
            FileCreation fileCreation, string lineContent)
        {
            this.FilePath = filePath;
            this.FileName = fileName;
            this.FileEncoding = fileEncoding;
            this.FileCreation = fileCreation;
            this.LineContent = lineContent;

            this.AktuellesFileLineWriterObject =
                new FileLineWriterObject(FilePath, FileName, FileEncoding, FileCreation, LineContent);
            return this;
        }
    }
}