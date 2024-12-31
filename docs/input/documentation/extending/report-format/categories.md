---
title: Alias categories
description: Instructions how to set the alias category.
---

Report format aliases should use the [IssuesAliasConstants.MainCakeAliasCategory](https://cakebuild.net/api/Cake.Issues/IssuesAliasConstants/41CCADF8){target="_blank"}
and [ReportingAliasConstants.ReportingFormatCakeAliasCategory](https://cakebuild.net/api/Cake.Issues.Reporting/ReportingAliasConstants/979CDCAF){target="_blank"}
constants for defining their category:

```csharp
[CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
public static class MyReportFormatAliases
{
    [CakeMethodAlias]
    [CakeAliasCategory(ReportingAliasConstants.ReportingFormatCakeAliasCategory)]
    public static IIssueReportFormat MyReportFormat(
        this ICakeContext context)
    {
    }
}
```
