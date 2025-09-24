---
title: Alias categories
description: Instructions how to set the alias category.
---

Issue provider aliases should use the [IssuesAliasConstants.MainCakeAliasCategory](https://cakebuild.net/api/Cake.Issues/IssuesAliasConstants/41CCADF8)
and [IssuesAliasConstants.IssueProviderCakeAliasCategory](https://cakebuild.net/api/Cake.Issues/IssuesAliasConstants/D265B28D)
constants for defining their category:

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
