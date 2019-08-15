#tool "nuget:?package=JetBrains.ReSharper.CommandLineTools"

Task("Run-InspectCode")
    .Description("Runs JetBrains InspectCode analysis")
    .Does<BuildData>(data =>
{
    var settings = new InspectCodeSettings() {
        OutputFile = data.InspectCodeLogFilePath
    };

    InspectCode(data.SourceFolder.CombineWithFilePath("ClassLibrary1.sln"), settings);
});