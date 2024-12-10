---
Order: 30
Title: Alias categories
Description: Instructions how to set the alias category.
---
Pull request system aliases should use the [IssuesAliasConstants.MainCakeAliasCategory] and
[PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory] constants for defining their category:

```csharp
[CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
public static class MyPullRequestSystemAliases
{
    [CakeMethodAlias]
    [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
    public static IPullRequestSystem MyPullRequestSystem(
        this ICakeContext context)
    {
    }
}
```

[IssuesAliasConstants.MainCakeAliasCategory]: ../../../api/Cake.Issues/IssuesAliasConstants/41CCADF8
[PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory]: ../../../api/Cake.Issues.PullRequests/PullRequestsAliasConstants/B4C013A1