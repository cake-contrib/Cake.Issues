# MsBuild support for the Cake.Issues addin for Cake Frosting

> **NOTE**:
> This is the version of the addin compatible with [Cake Frosting].
> For addin compatible with [Cake .NET Tool] see [Cake.Issues.MsBuild](https://www.nuget.org/packages/Cake.Issues.MsBuild).

The MsBuild support for the Cake.Issues addin for Cake allows you to read issues logged as warnings in a MsBuild log.

Cake.Issues redefines issue management within the Cake build system by offering a comprehensive, universal, and extensible solution.
The unique capabilities of the addins empower development teams to enforce coding standards, generate insightful reports,
seamlessly incorporate various linting tools, and streamlining the integration with pull requests.
With its [modular architecture] and extensive [set of aliases], Cake.Issues provides a future-proof infrastructure for issue management
in Cake builds, fostering a more efficient and adaptable development process.

For more information and extensive documentation see the [Cake.Issues website](https://cakeissues.net).
For general information about the Cake build automation system see the [Cake website](http://cakebuild.net).

## How to use

Integrating Cake.Issues into your Cake build is straightforward.
With minimal setup, teams can enjoy the benefits of enhanced code quality management seamlessly integrated into their existing build pipeline.

After adding the addin the log file of a MsBuild build can be parsed:

```csharp
[TaskName("Build-Solution")]
public sealed class BuildSolutionTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var logPath = @"c:\build\msbuild.binlog";
        var repoRootPath = @"c:\repo";

        // Build solution.
        context.DotNetBuild(
            repoRootPath.CombineWithFilePath("MySolution.sln"),
            new DotNetBuildSettings
            {
                MSBuildSettings =
                    new DotNetMSBuildSettings()
                        // Ensures that binary log version is written in a version compatible with Cake.Issues.MsBuild.
                        .WithLogger(
                            "BinaryLogger," + context.Environment.ApplicationRoot.CombineWithFilePath("StructuredLogger.dll"),
                            "",
                            logPath.FullPath)
            });

        // Read Issues.
        var issues =
            context.ReadIssues(
                context.MsBuildIssuesFromFilePath(logPath, context.MsBuildBinaryLogFileFormat),
                repoRootPath);

        context.Information("{0} issues are found.", issues.Count());
    }
}
```

## Support & Discussion

For questions and to discuss ideas & feature requests, use the [GitHub discussions on the Cake GitHub repository](https://github.com/cake-build/cake/discussions), under the [Extension Q&A](https://github.com/orgs/cake-build/discussions/categories/extension-q-a) category.

## Contributing

Contributions are welcome. See [Contribution Guidelines](https://github.com/cake-contrib/Cake.Issues/blob/develop/CONTRIBUTING.md).

## License

[MIT License - Copyright © Cake Issues contributors](LICENSE)

Binary distributions for some addins contain third-party code which is licensed under its own respective license.
See [LICENSE](https://github.com/cake-contrib/Cake.Issues/blob/develop/LICENSE) for details.

[modular architecture]: https://cakeissues.net/docs/fundamentals/architecture
[set of aliases]: https://cakeissues.net/dsl/
[Cake Frosting]: https://cakebuild.net/docs/running-builds/runners/cake-frosting
[Cake .NET Tool]: https://cakebuild.net/docs/running-builds/runners/dotnet-tool
