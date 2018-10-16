---
Order: 10
Title: Overview
Description: Overview about Cake.Issues.Recipe.
---
[Cake.Issues.Recipe] provides a build script, delivered as a NuGet package, which can be used inside your projects Cake build script to add issue management.

Integration of code analyzing and linting tools into a build pipeline often looks the similar, and differentiates mainly on the used linters, build and pull request systems.
Cake.Issues.Recipe contains code to do all the parsing, integration with build and pull request systems for you, using the individual Cake.Issues addins.
It supports different linters based on the linting log files you pass it and integrates automatically with different build and pull request systems.

See [supported tools] for a list of supported linters, build servers and pull request systems.

[Cake.Issues.Recipe]: https://www.nuget.org/packages/Cake.Issues.Recipe
[supported tools]: supported-tools