---
Order: 10
Title: Using Cake.Issues.Recipe
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

# Configuring Cake.Issues.Recipe

To make issues available to Cake.Issues.Recipe you need to set the corresponding configuration parameters.

In the following example a new task is introduced which depends on existing tasks which build a MsBuild solution and run JetBrains InspectCode.
It will pass the MsBuild and InspectCode logfile to Cake.Issues.Recipe:

```csharp
// Run issues task by default.
Task("Configure-CakeIssuesRecipe")
    .IsDependentOn("Build")
    .IsDependentOn("Run-InspectCode")
    .Does(() =>
{
    IssuesParameters.InputFiles.MsBuildXmlFileLoggerLogFilePath = msBuildLogFilePath;
    IssuesParameters.InputFiles.InspectCodeLogFilePath = inspectCodeLogFilePath;
}
```

See [configuration] for a full list of available configuration parameters.

# Calling issues tasks

Cake.Issues.Recipe will add a bunch of [tasks] to your build script.

To add the issues functionality into your existing build pipeline you can make
the `Read-Issues` task dependent on the task which configures Cake.Issues.Recipe:

```csharp
// Make sure build and linters run before issues task.
IssuesBuildTasks.ReadIssuesTask
    .IsDependentOn("Configure-CakeIssuesRecipe");
```

At some point you need to call the tasks provided by Cake.Isses.Recipe.
In the following example the `Default` task calls the main `Issues` task:

```csharp
// Run issues task by default.
Task("Default")
    .IsDependentOn("Issues");
```

[Cake.Issues.Recipe]: ../../recipe/
[configuration]: ../../recipe/configuration
[tasks]: ../../recipe/tasks