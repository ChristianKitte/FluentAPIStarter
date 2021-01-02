using FluentAPI.Enum;

namespace FluentAPI
{
    public interface IFileLineWriter
    {
        IFileLineWriter AusgabeDurch(FileCreation fileCreation);
        IFileLineWriter SpeicherInVerzeichnis(params string[] filePathParts);
        IFileLineWriter VerwendeDateinamen(string fileName);
        IFileLineWriter VerwendeZurCodierung(FileEncoding fileEncoding);
        IFileLineWriter Schreibe(string lineContent);
        bool WarDieAktionErfolgreich();
    }
}