---
title: Using Cake.Issues.Recipe
description: Basic usage of Cake.Issues.Recipe.
---

The [Cake.Issues.Recipe] package can be used to easily add issue management functionality to your build script.

## Add Cake.Issues.Recipe to your build script

To use Cake.Issues.Recipe in your build script you need to first load the NuGet package:

```csharp
#load nuget:package=Cake.Issues.Recipe&version={{ cake_issues_version }}
```

## Configuring Cake.Issues.Recipe

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
    IssuesParameters.InputFiles
        .AddMsBuildBinaryLogFilePath(msBuildLogFilePath);
    IssuesParameters.InputFiles
        .AddInspectCodeLogFilePath(inspectCodeLogFilePath);
}
```

!!! tip
    See [configuration] for a full list of available configuration parameters.

## Calling issues tasks

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

[Cake.Issues.Recipe]: ../../recipe/index.md
[configuration]: ../../recipe/configuration.md
[tasks]: ../../recipe/tasks.md