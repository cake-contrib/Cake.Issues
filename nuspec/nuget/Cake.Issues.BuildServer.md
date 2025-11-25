# Addin for reporting issues to build servers for the Cake build automation system

The issues addin for Cake allows you to report issues to build servers.

Cake.Issues redefines issue management within the Cake build system by offering a comprehensive, universal, and extensible solution.
The unique capabilities of the addins empower development teams to enforce coding standards, generate insightful reports,
seamlessly incorporate various linting tools, and streamlining the integration with build servers.
With its [modular architecture] and extensive [set of aliases], Cake.Issues provides a future-proof infrastructure for issue management
in Cake builds, fostering a more efficient and adaptable development process.

For more information and extensive documentation see the [Cake.Issues website](https://cakeissues.net).
For general information about the Cake build automation system see the [Cake website](http://cakebuild.net).

## How to use

Integrating Cake.Issues into your Cake build is straightforward.
With minimal setup, teams can enjoy the benefits of enhanced code quality management seamlessly integrated into their existing build pipeline.

> **NOTE**:
> [Cake.Issues.Recipe](https://www.nuget.org/packages/Cake.Issues.Recipe) for Cake .NET Tool and
> [Cake.Frosting.Issues.Recipe](https://www.nuget.org/packages/Cake.Frosting.Issues.Recipe)
> provide a single NuGet package, which can be used inside your projects build to add fully flavored issue management,
> including parsing of linter outputs, integration with build systems and pull requests and creation of different reports.

### Reporting issues to build servers

The addin provides the `ReportIssuesToBuildServer` alias to report issues to build servers.
It needs an additional NuGet package to provide the specific build server implementation:

| Cake .NET Tool Addin | Cake Frosting Addin | Description |
|:--:|-|-|
| [Cake.Issues.PullRequests.AppVeyor](https://www.nuget.org/packages/Cake.Issues.PullRequests.AppVeyor) | [Cake.Frosting.Issues.PullRequests.AppVeyor](https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.AppVeyor) | Integration with AppVeyor builds. |
| [Cake.Issues.PullRequests.GitHubActions](https://www.nuget.org/packages/Cake.Issues.PullRequests.GitHubActions) | [Cake.Frosting.Issues.PullRequests.GitHubActions](https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.GitHubActions) | Integration with GitHub Actions. |

See [Build Servers] for a list of available build server addins and detailed documentation.

### Reading issues

Issues can be read using the [Cake.Issues](https://www.nuget.org/packages/Cake.Issues) addin for Cake .NET Tool or
[Cake.Frosting.Issues](https://www.nuget.org/packages/Cake.Frosting.Issues) addin for Cake Frosting
together with any of the available [Issue Provider] addins.

### Example

The following example will read issues reported as MsBuild warnings and report them to AppVeyor:

```csharp
[TaskName("Report-Issues")]
public sealed class ReportIssuesTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var issueProvider = 
            context.MsBuildIssuesFromFilePath(
                context.MsBuildLogFilePath,
                context.MsBuildBinaryLogFileFormat);

        var result = 
            context.ReportIssuesToBuildServer(
                issueProvider,
                context.AppVeyorBuilds(),
                context.RootDirectoryPath);

        context.Information("{0} issues were reported to AppVeyor", result.PostedIssues.Count());
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

[modular architecture]: https://cakeissues.net/latest/documentation/how-cake-issues-works/
[set of aliases]: https://cakeissues.net/latest/api/
[Issue Provider]: https://cakeissues.net/latest/documentation/issue-providers/
[Build Servers]: https://cakeissues.net/latest/documentation/build-servers/