---
title: File linking
description: Example how to define file link settings.
---

File link settings can be defined while reading issues and are passed through the `IIssue.FileLink` property to
reporting formats, pull request systems and build server implementations:

??? Tip "List of all aliases for file linking"
    See all available [Aliases for file linking](https://cakebuild.net/extensions/cake-issues/#File-Linking){target="_blank"}

=== "Cake .NET Tool"

    ```csharp
    var settings =
        new ReadIssuesSettings(@"c:\repo")
        {
            FileLinkSettings =
                IssueFileLinkSettingsForGitHubCommit(
                    "https://github.com/cake-contrib/Cake.Issues",
                    "76a7cacef7ad4295a6766646d45c9b56")
        };    

        var issues =
            ReadIssues(
                InspectCodeIssuesFromFilePath(
                    @"C:\build\inspectcode.log"),
                settings));
    ```

=== "Cake SDK"

    ```csharp
    var settings =
        new ReadIssuesSettings(@"c:\repo")
        {
            FileLinkSettings =
                IssueFileLinkSettingsForGitHubCommit(
                    "https://github.com/cake-contrib/Cake.Issues",
                    "76a7cacef7ad4295a6766646d45c9b56")
        };    

        var issues =
            ReadIssues(
                InspectCodeIssuesFromFilePath(
                    @"C:\build\inspectcode.log"),
                settings));
    ```

=== "Cake Frosting"

    ```csharp
    var settings =
        new ReadIssuesSettings(@"c:\repo")
        {
            FileLinkSettings =
                IssueFileLinkSettingsForGitHubCommit(
                    "https://github.com/cake-contrib/Cake.Issues",
                    "76a7cacef7ad4295a6766646d45c9b56")
        };    

        var issues =
            context.ReadIssues(
                context.InspectCodeIssuesFromFilePath(
                    @"C:\build\inspectcode.log"),
                settings));
    ```

Cake.Issues comes with out-of-the-box support for linking to files hosted on GitHub and Azure Repos,
either for a specific branch or commit. Additionally there are aliases which can be used to define any custom pattern.
