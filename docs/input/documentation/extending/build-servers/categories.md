---
title: Alias categories
description: Instructions how to set the alias category.
---

Build server aliases should use the [IssuesAliasConstants.MainCakeAliasCategory](https://cakebuild.net/api/Cake.Issues/IssuesAliasConstants/41CCADF8){target="_blank"}
and [PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory](https://cakebuild.net/api/Cake.Issues.PullRequests/PullRequestsAliasConstants/B4C013A1){target="_blank"}
constants for defining their category:

```csharp
[CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
public static class MyBuildServerAliases
{
    [CakeMethodAlias]
    [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
    public static IPullRequestSystem MyBuildServer(
        this ICakeContext context)
    {
    }
}
```
