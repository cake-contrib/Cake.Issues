---
Order: 30
Title: File linking
Description: Example how to define file link settings.
---
File link settings can be defined while reading issues and are passed through the `IIssue.FileLink` property to
reporting formats, pull request systems and build server implementations:

```csharp
var settings =
    new ReadIssuesSettings(@"c:\repo")
    {
        FileLinkSettings =
            IssueFileLinkSettingsForGitHubCommit(
                "https://github.com/cake-contrib/Cake.Issues.Reporting.Generic",
                "76a7cacef7ad4295a6766646d45c9b56")
    };

    var issues =
        ReadIssues(
            InspectCodeIssuesFromFilePath(
                @"C:\build\inspectcode.log"),
            settings));
```

Cake.Issues comes with out-of-the-box support for linking to files hosted on GitHub and Azure Repos,
either for a specific branch or commit. Additionally there are aliases which can be used to define any custom pattern.
