---
title: Alias categories
description: Instructions how to set the alias category.
---

Report format aliases should use the [IssuesAliasConstants.MainCakeAliasCategory](https://cakebuild.net/api/Cake.Issues/IssuesAliasConstants/41CCADF8)
and [ReportingAliasConstants.ReportingFormatCakeAliasCategory](https://cakebuild.net/api/Cake.Issues.Reporting/ReportingAliasConstants/979CDCAF)
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
