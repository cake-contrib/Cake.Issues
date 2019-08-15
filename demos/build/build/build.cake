#tool "nuget:?package=MSBuild.Extension.Pack"

Task("Build")
    .Description("Builds the solution")
    .Does<BuildData>(data =>
{
    var solutionFile = data.SourceFolder.CombineWithFilePath("ClassLibrary1.sln");

    NuGetRestore(solutionFile);

    var settings =
        new MSBuildSettings()
            .WithTarget("Rebuild")
            .WithLogger(
                Context.Tools.Resolve("MSBuild.ExtensionPack.Loggers.dll").FullPath,
                "XmlFileLogger",
                string.Format(
                    "logfile=\"{0}\";verbosity=Detailed;encoding=UTF-8",
                    data.MsBuildLogFilePath)
            );

    MSBuild(solutionFile, settings);
});