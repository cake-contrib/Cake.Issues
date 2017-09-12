---
Order: 30
Title: Examples
Description: Examples for using the Cake.Issues.Reporting.Html addin.
---
The following example will create a HTML report for issues logged as warnings by MsBuild.

```csharp
#tool "nuget:?package=MSBuild.Extension.Pack"
#addin "Cake.Issues"
#addin "Cake.Issues.MsBuild"
#addin "Cake.Reporting"
#addin "Cake.Reporting.Html"

Task("Create-IssueReport").Does(() =>
{
    var repoRootFolder = new DirectoryPath(@"c:\repo");

    // Build MySolution.sln solution in the repository root folder and log issues
    // using XmlFileLogger from MSBuild Extension Pack.
    FilePath msBuildLogFile = @"c:\build\msbuild.log";
    var settings = new MsBuildSettings()
        .WithLogger(
            Context.Tools.Resolve("MSBuild.ExtensionPack.Loggers.dll").FullPath,
            "XmlFileLogger",
            string.Format(
                "logfile=\"{0}\";verbosity=Detailed;encoding=UTF-8",
                msBuildLogFile)
        );
    MSBuild(repoRootFolder.CombineWithFilePath("MySolution.sln"), settings);

    // Create HTML report using Diagnostic template.
    CreateIssueReport(
        new List<IIssueProvider>
        {
            MsBuildIssuesFromFilePath(
                msBuildLogFile,
                MsBuildXmlFileLoggerFormat)
        },
        HtmlIssueReportFormatFromEmbeddedTemplate(HtmlIssueReportTemplate.Diagnostic),
        repoRootFolder,
        @"c:\report.html");
});
```