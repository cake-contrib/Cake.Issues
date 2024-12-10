---
Order: 30
Title: Alias categories
Description: Instructions how to set the alias category.
---
Issue provider aliases should use the [IssuesAliasConstants.MainCakeAliasCategory] and
[IssuesAliasConstants.IssueProviderCakeAliasCategory] constants for defining their category:

```csharp
[CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
public static class MyIssueProviderAliases
{
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider MyIssueProvider(
        this ICakeContext context)
    {
    }
}
```

[IssuesAliasConstants.MainCakeAliasCategory]: ../../../api/Cake.Issues/IssuesAliasConstants/41CCADF8
[IssuesAliasConstants.IssueProviderCakeAliasCategory]: ../../../api/Cake.Issues/IssuesAliasConstants/D265B28D