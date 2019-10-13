---
Order: 20
Title: Custom template
Description: Example how to create a report using a custom template
---
:::{.alert .alert-info}
If you create a universally usable custom template we're happy to package it with the addin.
To have it included in the addin please [create a pull request] with your contribution.
:::

The following example will create a HTML report for issues logged as warnings by MsBuild using a custom template.

:::{.alert .alert-warning}
Please note that you always should pin addins and tools to a specific version to make sure your builds are deterministic and
won't break due to updates to one of the packages.

See [pinning addin versions](https://cakebuild.net/docs/tutorials/pinning-cake-version#pinning-addin-version) for details.
:::

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
                    <td>@issue.MessageText</td>
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

* System.dll
* System.Core.dll
* netstandard.dll
* Newtonsoft.Json.dll
* Cake.Core.dll
* Cake.Issues.dll

:::

[create a pull request]: https://github.com/cake-contrib/Cake.Issues.Reporting.Generic/blob/develop/CONTRIBUTING.md
