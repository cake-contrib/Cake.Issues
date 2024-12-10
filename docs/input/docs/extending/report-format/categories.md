---
Order: 30
Title: Alias categories
Description: Instructions how to set the alias category.
---
Report format aliases should use the [IssuesAliasConstants.MainCakeAliasCategory] and
[ReportingAliasConstants.ReportingFormatCakeAliasCategory] constants for defining their category:

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

[IssuesAliasConstants.MainCakeAliasCategory]: ../../../api/Cake.Issues/IssuesAliasConstants/41CCADF8
[ReportingAliasConstants.ReportingFormatCakeAliasCategory]: ../../../api/Cake.Issues.Reporting/ReportingAliasConstants/979CDCAF