---
title: Additional run information
description: Example how to add run specific information.
---

If a build script needs to parse multiple log files from the same tool, e.g. because multiple MsBuild solutions were built,
this can be done by calling the issue provider multiple times.
If the results are read into the same list and shown on the same report,
individual issues could not be assigned to any of the calls, since issue provider type and name are identical.

Starting with Cake.Issues 0.9.0 it is possible to pass additional run information while reading issues,
which then will be stored with each issues in the `IIssue.Run` property:

=== "Cake .NET Tool"

    ```csharp
    var issues = new List<IIssue>();
    
    // Parse issues from build of solution 1
    issues.AddRange(
        ReadIssues(
            MsBuildIssuesFromFilePath(
                @"C:\build\solution1-msbuild.log",
                MsBuildBinaryLogFileFormat),
            new ReadIssuesSettings(@"c:\repo")
            {
                Run = "Solution 1"
            }
        )
    );
    
    // Parse issues from build of solution 2
    issues.AddRange(
        ReadIssues(
            MsBuildIssuesFromFilePath(
                @"C:\build\solution2-msbuild.log",
                MsBuildBinaryLogFileFormat),
            new ReadIssuesSettings(@"c:\repo")
            {
                Run = "Solution 2"
            }
        )
    );
    ```

=== "Cake Frosting"

    ```csharp
    var issues = new List<IIssue>();
    
    // Parse issues from build of solution 1
    issues.AddRange(
        context.ReadIssues(
            context.MsBuildIssuesFromFilePath(
                @"C:\build\solution1-msbuild.log",
                context.MsBuildBinaryLogFileFormat()),
            new ReadIssuesSettings(@"c:\repo")
            {
                Run = "Solution 1"
            }
        )
    );
    
    // Parse issues from build of solution 2
    issues.AddRange(
        context.ReadIssues(
            context.MsBuildIssuesFromFilePath(
                @"C:\build\solution2-msbuild.log",
                context.MsBuildBinaryLogFileFormat()),
            new ReadIssuesSettings(@"c:\repo")
            {
                Run = "Solution 2"
            }
        )
    );
    ```
