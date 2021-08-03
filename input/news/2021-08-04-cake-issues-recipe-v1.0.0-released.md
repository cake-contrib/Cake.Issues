---
title: Cake Issues Recipe v1.0.0 Released, bringing support for Cake Frosting
category: Release Notes
---

Hard on the heels of the [announcement for release 1.0 of Cake.Issues addins],
we're happy to announce release 1.0 of the recipe script for Cake.Issues.

This is a major release bringing support for Cake Frosting and other new features.

[announcement for release 1.0 of Cake.Issues addins]: /news/cake-issues-v1.0.0-released

<!--excerpt-->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [pascalberger](https://github.com/pascalberger)

## Support for Cake 1.0

Cake Issues recipe has been updated to use latest 1.x versions of the Cake.Issues addins which support Cake 1.x.

See [announcement for release 1.0 of Cake.Issues addins] for features added in the individual addins.

## Support for Cake Frosting

Additionally to the existing [Cake.Issues.Recipe] package, which works fine for Cake script runners, there's a new
[Cake.Frosting.Issues.Recipe] package suitable for builds using [Cake Frosting].

See [Using Cake.Frosting.Issues.Recipe] for an example.

[Cake Frosting]: https://cakebuild.net/docs/running-builds/runners/cake-frosting
[Cake.Issues.Recipe]: https://www.nuget.org/packages/Cake.Issues.Recipe/
[Cake.Frosting.Issues.Recipe]: https://www.nuget.org/packages/Cake.Frosting.Issues.Recipe/
[Using Cake.Frosting.Issues.Recipe]: /docs/usage/recipe/using-cake-frosting-issues-recipe

## Support for environments not compatible with Cake.Git addin

In previous versions Cake.Issues recipes had used [Cake.Git addin] to determine state of the Git repository.
While this works in most cases, there are some environments where [Cake.Git addin] currently does not work.

Starting with this version it is now possible to define if [Cake.Git addin] or Git CLI should be used.

See [Git repository information configuration](/docs/recipe/configuration#git-repository-information) for details.

[Cake.Git addin]: https://cakebuild.net/extensions/cake-git/

## Define settings for issue providers

Previous versions of Cake.Issues recipes used default settings for reading issues from the passed log files.
This made it impossible to parse log files which for example are created by tools running in containers,
as they have a root path different to the repository root.

This version introduced methods to pass log files to the recipe scripts, which additionally to the log file path
accept a settings object.

The new implementation also supports other use-cases like reading multiple files with the same issues provider,
but different settings (e.g. run information).

## Updating from previous versions

Cake.Issues Recipes 1.0.0 is a breaking release, which means that it probably requires changes to your build script.
This section documents the most common changes which might be required:

* Properties for passing input files to the recipe scripts have been replaced by methods.
  As an example, in previous versions MsBuild XML log files could be passed by setting the
  `IssuesParameters.InputFiles.MsBuildXmlFileLoggerLogFilePath` property:

  ```csharp
  IssuesParameters.InputFiles.MsBuildXmlFileLoggerLogFilePath = @"c:\build\msbuild.log";
  ```

  With 1.0 the `IssuesParameters.InputFiles.AddMsBuildXmlFileLoggerLogFile()` method needs to be called instead:

  ```csharp
  IssuesParameters.InputFiles.AddMsBuildXmlFileLoggerLogFile(@"c:\build\msbuild.log");
  ```

For details see [full release notes].

[full release notes]: https://github.com/cake-contrib/Cake.Issues.Recipe/releases/tag/1.0.0
