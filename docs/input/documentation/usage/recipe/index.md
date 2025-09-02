---
title: Recipe usage
description: How to obtain, configure, and use Cake Issues recipes.
---

Cake.Issues recipes provide build scripts, delivered as a NuGet package, which can be used inside your projects Cake build to add issue management.

Integration of code analyzing and linting tools into a build pipeline often looks the similar, and differentiates mainly on the used linters, build and pull request systems.
Cake.Issues recipes contain code to do all the parsing, integration with build and pull request systems for you, using the individual Cake.Issues addins.
They support different linters based on the linting log files you pass it and integrate automatically with different build and pull request systems.

There are two flavors available:

<div class="grid cards" markdown>

-   :material-receipt-text: [Cake.Issues.Recipe]

    ---

    For [Cake .NET Tool]

-   :material-receipt-text: [Cake.Frosting.Issues.Recipe]

    ---

    For [Cake Frosting]

</div>

[Cake.Issues.Recipe]: using-cake-issues-recipe.md
[Cake.Frosting.Issues.Recipe]: using-cake-frosting-issues-recipe.md
[Cake .NET Tool]: https://cakebuild.net/docs/running-builds/runners/dotnet-tool
[Cake Frosting]: https://cakebuild.net/docs/running-builds/runners/cake-frosting
