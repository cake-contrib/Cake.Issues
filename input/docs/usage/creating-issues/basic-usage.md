---
Order: 10
Title: Basic usage
Description: Usage instructions how to create issues.
---
The `Cake.Issues` addin can be used to create issues directly in the build script.
This issues can for example be used to create reports.

To create issues you need to import the following core addin:

```csharp
#addin "Cake.Issues"
```

:::{.alert .alert-warning}
Please note that you always should pin addins to a specific version to make sure your builds are deterministic and
won't break due to updates to one of the addins.

See [pinning addin versions](https://cakebuild.net/docs/tutorials/pinning-cake-version#pinning-addin-version) for details.
:::

In the following taks a new warning for the myfile.txt file on line 42 is created:

```csharp
Task("Create-Issue").Does(() =>
{
    var issue =
        NewIssue(
            "Something went wrong",
            "MyCakeScript",
            "My Cake Script")
            .InFile("myfile.txt", 42)
            .WithPriority(IssuePriority.Warning)
            .Create();

    Information("{0} issues are found.", issues.Count());
});
```