---
Order: 30
Title: Examples
Description: Examples for using the Cake.Issues.Reporting.Generic addin.
---
## Use embedded default template

The following example will create a HTML report for issues logged as warnings by MsBuild.

```csharp
#tool "nuget:?package=MSBuild.Extension.Pack"
#addin "Cake.Issues"
#addin "Cake.Issues.MsBuild"
#addin "Cake.Reporting"
#addin "Cake.Reporting.Generic"

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
        GenericIssueReportFormatFromEmbeddedTemplate(HtmlIssueReportTemplate.HtmlDiagnostic),
        repoRootFolder,
        @"c:\report.html");
});
```

## Use custom template

The following example will create a HTML report for issues logged as warnings by MsBuild using a custom template.

```csharp
#tool "nuget:?package=MSBuild.Extension.Pack"
#addin "Cake.Issues"
#addin "Cake.Issues.MsBuild"
#addin "Cake.Reporting"
#addin "Cake.Reporting.Generic"

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
        GenericIssueReportFormatFromFilePath(@"c:\ReportTemplate.cshtml"),
        repoRootFolder,
        @"c:\report.html");
});
```

`ReportTemplate` looks like this:

```csharp
@model IEnumerable<Cake.Issues.IIssue>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title></title>
</head>
<body>
    <table>
        <thead>
            <tr>
                <th scope="col">AffectedFileRelativePath</th>
                <th scope="col">Line</th>
                <th scope="col">Message</th>
                <th scope="col">Priority</th>
                <th scope="col">Rule</th>
                <th scope="col">RuleUrl</th>
                <th scope="col">ProviderType</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var issue in Model)
            {
                <tr>
                    <td>@issue.AffectedFileRelativePath</td>
                    <td>@issue.Line</td>
                    <td>@issue.Message</td>
                    <td>@issue.Priority</td>
                    <td>@issue.Rule</td>
                    <td>@issue.RuleUrl</td>
                    <td>@issue.ProviderType</td>
                </tr>
            }
        </tbody>
    </table>
</body>
</html>
```

The template retrieves an `IEnumerable<Cake.Issues.IIssue>` as model.

:::{.alert .alert-info}
In custom templates functionality from the following assemblies are available:

* mscorlib.dll
* system.dll
* system.core.dll
* Cake.Core.dll
* Cake.Issues.dll

:::