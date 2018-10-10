---
Order: 10
Title: Basic usage
Description: Basic usage of Cake.Issues.Recipe.
---
The [Cake.Issues.Recipe] package can be used to easily add issue management functionality to your build script.

# Add Cake.Issues.Recipe to your build script

To use Cake.Issues.Recipe in your build script you need to first load the NuGet package:

```csharp
#load nuget:package=Cake.Issues.Recipe
```

:::{.alert .alert-warning}
Please note that you always should pin NuGet packages to a specific version to make sure your builds are deterministic and
won't break due to updates to Cake.Issues.Recipe.

See [pinning addin versions](https://cakebuild.net/docs/tutorials/pinning-cake-version#pinning-addin-version) for details.
:::

# Calling issues tasks

Cake.Issues.Recipe will add a bunch of [tasks] to your build script.

## Calling explicitly

You can call the tasks explicitly while running your Cake build script:

```cmd
.\build.ps1 -target Issues
```

## Integrating in build pipeline

Often you might want to integrate the issues functionality into your existing build pipeline.
For this you can make the `Read-Issues` task dependent on your specific task which generates the log files.
In the following example the `Build` and `Run-InspectCode` task:

```csharp
// Make sure build and linters run before issues task.
IssuesBuildTasks.ReadIssuesTask
    .IsDependentOn("Build")
    .IsDependentOn("Run-InspectCode");
```

At some point you need to call the tasks provided by Cake.Isses.Recipe.
In the following example the `Default` task calls the main `Issues` task:

```csharp
// Run issues task by default.
Task("Default")
    .IsDependentOn("Issues");
```

[Cake.Issues.Recipe]: ../../recipe/
[tasks]: ../../recipe/tasks