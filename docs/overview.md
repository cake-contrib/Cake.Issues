---
Order: 10
Title: Overview
Description: Overview about Cake.Issues recipes.
---
Cake.Issues recipes provide build scripts, delivered as a NuGet package, which can be used inside your projects Cake build to add issue management.

Integration of code analyzing and linting tools into a build pipeline often looks the similar, and differentiates mainly on the used linters, build and pull request systems.
Cake.Issues recipes contain code to do all the parsing, integration with build and pull request systems for you, using the individual Cake.Issues addins.
They support different linters based on the linting log files you pass it and integrate automatically with different build and pull request systems.

There are two flavors available:

* [Cake.Issues.Recipe]: For [Cake .NET Tool], [Cake runner for .NET Framework] or [Cake runner for .NET Core]
* [Cake.Frosting.Issues.Recipe]: For [Cake Frosting]

See [supported tools] for a list of supported linters, build servers and pull request systems.

[Cake.Issues.Recipe]: https://www.nuget.org/packages/Cake.Issues.Recipe
[Cake.Frosting.Issues.Recipe]: https://www.nuget.org/packages/Cake.Frosting.Issues.Recipe
[Cake .NET Tool]: https://cakebuild.net/docs/running-builds/runners/dotnet-tool
[Cake runner for .NET Framework]: https://cakebuild.net/docs/running-builds/runners/cake-runner-for-dotnet-framework
[Cake runner for .NET Core]: https://cakebuild.net/docs/running-builds/runners/cake-runner-for-dotnet-core
[Cake Frosting]: https://cakebuild.net/docs/running-builds/runners/cake-frosting
[supported tools]: supported-tools