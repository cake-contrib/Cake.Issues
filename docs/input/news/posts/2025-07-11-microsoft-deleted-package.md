---
title: Cake Issues 5.6 has been left-padded by Microsoft
date: 2025-07-11
categories:
  - Announcements
links:
  - documentation/pull-request-systems/azure-devops/index.md
---

`Cake.Frosting.Issues.PullRequests.AzureDevOps` version 5.6.0 has been deleted by Microsoft.

<!-- more -->

We've received the following email from Microsoft:

![Email requesting action](2025-07-11-microsoft-deleted-package.png "Email requesting action")

They had a typo in an XML comment inside `Microsoft.Identity.Client`.
`Microsoft.Identity.Client` is a dependency of `Cake.AzureDevOps`, which is used by `Cake.Frosting.Issues.PullRequests.AzureDevOps`.

When looking at the [release notes of Microsoft.Identity.Client 4.72.1](https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/releases/tag/4.72.1) it not even mentions any CVE or related fixes.
In contrast to what was written in the email it was also not fixed in [Microsoft.Identity.Client 4.72.1](https://www.nuget.org/packages/Microsoft.Identity.Client/4.72.1) based on the warning on nuget.org.

Beside that, it is not someting which can be directly fixed in `Cake.Frosting.Issues.PullRequests.AzureDevOps`, since it is a transitive dependency through `Cake.AzureDevOps`.
It is also not a real security issue, since no user of a build script will likely check XML comments from a transitive dependency and type them in a browser.

Therefore nothing was done.

To be suprised to find out that `Cake.Frosting.Issues.PullRequests.AzureDevOps` 5.6.0 had now been deleted without any further notice.
This means hard deleted, not unlisted.
Meaning the package won't restore and break existing builds.

The issue has been escalated to Microsoft.
But for now `Cake.Frosting.Issues.PullRequests.AzureDevOps` 5.6.0 can't be used and neither `Cake.Frosting.Issues.Recipe` 5.6.0, which has a dependency on it.
Please stick with 5.5.0 in these cases.

There is also an excellent [blog post by Aaron Stannard](https://aaronstannard.com/microsoft-delete-nuget-packages) about this incident and
a [GithHub issue](https://github.com/NuGet/Home/discussions/14413).
