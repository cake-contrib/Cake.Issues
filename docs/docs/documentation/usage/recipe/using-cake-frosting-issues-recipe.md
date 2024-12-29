---
title: Using Cake.Frosting.Issues.Recipe
description: Basic usage of Cake.Frosting.Issues.Recipe.
---

The [Cake.Frosting.Issues.Recipe] package can be used to easily add issue management functionality to your Cake Frosting build.

!!! info
    See [Setting Up A New Frosting Project] for instructions how to create a new Cake Frosting project.

[Setting Up A New Frosting Project]: https://cakebuild.net/docs/getting-started/setting-up-a-new-frosting-project{target="_blank"}

## Add Cake.Frosting.Issues.Recipe to your Cake Frosting build

To use [Cake.Frosting.Issues.Recipe] in your Cake Frosting build you need to first add the NuGet package in your `.csproj` file:

```csharp
<PackageReference Include="Cake.Frosting.Issues.Recipe" Version="1.0.0" />
```

!!! warning
    Replace the version (`1.0.0`) with the version of Cake.Frosting.Issues.Recipe you want to use.

## Register Cake.Issues tasks

To make Cake Issues tasks available to your Cake Frosting build you need to register them.

Add the following line to the bootstrapping code in the `Main` method of your Cake Frosting project:

```csharp
AddAssembly(Assembly.GetAssembly(typeof(IssuesTask)))
```

The following bootstrapping code registers the Cake Issues tasks and also installs JetBrains InspectCode:

```csharp
using System;
using System.Reflection;
using Cake.Frosting;
using Cake.Frosting.Issues.Recipe;

public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<BuildContext>()
            .InstallTool(
                new Uri(
                    "nuget:?package=JetBrains.ReSharper.CommandLineTools")) // (1)
            .AddAssembly(Assembly.GetAssembly(typeof(IssuesTask)))
            .Run(args);
    }
}
```

--8<-- "snippets/pinning.md"

## Create build context

[Cake.Frosting.Issues.Recipe] provides a build context from which you need to inherit your custom build context.
The build context contains configuration parameters, but also the state of the current running build,
like for example all collected issues.

The following example creates a build context and defines that Cake Issues should use Cake.Git addin to determine
state of the Git repository:

```csharp
public class BuildContext : IssuesContext
{
    public BuildContext(ICakeContext context)
        : base(context, RepositoryInfoProviderType.CakeGit)
    {
    }
}
```

## Passing issues to Cake.Frosting.Issues.Recipe

To make issues available to [Cake.Frosting.Issues.Recipe] you need pass the log files through the corresponding methods.
The tasks need to also be a dependency of `ReadIssuesTask` provided by [Cake.Frosting.Issues.Recipe].

In the following example a new task is introduced which runs JetBrains InspectCode and passes the log file to [Cake.Frosting.Issues.Recipe]:

```csharp
[TaskName("Run-InspectCode")]
[IsDependeeOf(typeof(ReadIssuesTask))]
public class RunInspectCodeTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var inspectCodeLogFilePath = context.Parameters.OutputDirectory..CombineWithFilePath("inspectCode.log");

        // Run JetBrains InspectCode
        context.InspectCode(
            context.State.RepositoryRootDirectory.Combine("src").CombineWithFilePath("ClassLibrary1.sln"),
            new InspectCodeSettings() {
                OutputFile = context.InspectCodeLogFilePath
            });

        // Pass path to InspectCode log file to Cake.Frosting.Issues.Recipe
        context.Parameters.InputFiles.AddInspectCodeLogFilePath(context.InspectCodeLogFilePath);
    }
}
```

See [configuration] for a full list of available configuration parameters.

## Calling issues tasks

[Cake.Frosting.Issues.Recipe] will add a bunch of [tasks] to your build script.

To add the issues functionality into your existing build pipeline you need to add
`ReadIssuesTask` to your pipeline.

 In the following example the `Default` task makes sure the main `IssuesTask` is executed:

```csharp
[TaskName("Default")]
[IsDependentOn(typeof(IssuesTask))]
public class DefaultTask : FrostingTask
{
}
```

[Cake.Frosting.Issues.Recipe]: ../../recipe/index.md
[configuration]: ../../recipe/configuration.md
[tasks]: ../../recipe/tasks.md