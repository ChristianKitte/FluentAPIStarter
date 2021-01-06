(Editor: https://stackedit.io/)<br>
(Info Badge TC: https://patriksvensson.se/2014/01/displaying-teamcity-build-status-on-github/)

![Build Status](https://advancedsoftwaretechnik.beta.teamcity.com/app/rest/builds/buildType:id:FluentAPIStarter_Build/statusIcon)

Verfahren und Werkzeuge moderner Softwareentwicklug
Wintersemester 20/21
Einsendeaufgabe  Domain Specific Language (DSL)

## Aufgabe Domain Specific Language

 - Beschäftigung mit dem Themengebiet DSL
 - Erstellen einer eigenen DSL

## Lösung Domain Specific Language
Im Rahmen der Aufgabe habe ich eine kleine interne DSL mit C# erstellt, mit der Text in eine Datei ausgegeben werden kann. Dies definiert auch deren Fachgebiet.
Technisch gesehen handelt es sich um eine Konsolenanwendung mit .NET Core 5 (Visual Studio Projekt)

Zur Umsetzung habe ich eine Fluent API entwickelt, welche eine an die natürlichen Sprache angelehnte Syntax unterstützt. Die Ausgabe zweier Dateien und jeweils 
eines Textes kann hierdurch folgendermaßen ausgegeben werden:

```c#
   var writer = _fileLineWriter
   .AusgabeDurch(FileCreation.Überschreiben)
   .VerwendeDateinamen("Datei 1.txt")
   .VerwendeZurCodierung(FileEncoding.Auto)
   .SpeicherInVerzeichnis("c:", "FluentApi")
   .Schreibe("Ausgabe Datei 1");

   writer.VerwendeDateinamen("Datei 2.txt")
   .Schreibe("Ausgabe Datei 2");

   bool status = writer.WarDieAktionErfolgreich();
```

Der Code erzeugt die Dateien ***c:\FluentAPI\Datei 1.txt*** und ***c:\FluentAPI\Datei 2.txt*** mit den Texten ***Ausgabe Datei 1*** und ***Ausgabe Datei 2***. Wenn 
die Aktion fehlerfrei ausgeführt wurde, hat ***status*** den Wert ***True***, ansonsten ***False***.

Kern der Anwendung ist das [***FileLineWriterObject***](https://github.com/ChristianKitte/FluentAPIStarter/blob/master/FluentAPI/FileLineWriterObject.cs), welches 
als value object für die Ausgabedaten dient und mit Hilfe des Typs ***record*** umgesetzt wurde. Die DSL selbst wird in der Klasse 
[***FileLineWriterObject***](https://github.com/ChristianKitte/FluentAPIStarter/blob/master/FluentAPI/FileLineWriter.cs) definiert und übersetzt. 
Diese muss somit verfügbar sein.
