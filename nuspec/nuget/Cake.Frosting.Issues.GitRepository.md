# Git repository linting support for the Cake.Issues addin for Cake Frosting

> **NOTE**:
> This is the version of the addin compatible with [Cake Frosting].
> For addin compatible with [Cake .NET Tool] see [Cake.Issues.GitRepository](https://www.nuget.org/packages/Cake.Issues.GitRepository).

The Git repository support for the Cake.Issues addin for Cake allows you to analyze Git repositories.

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

After adding the addin the repository can be analyzed:

```csharp
[TaskName("Analyze-GitRepository")]
public sealed class AnalyzeGitRepositoryTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var repoRootPath = @"c:\repo";

        // Analyze repository.
        var issues =
            context.ReadIssues(
                context.GitRepositoryIssues(
                    new GitRepositoryIssuesSettings
                    {
                        CheckBinaryFilesTrackedByLfs = true
                    }
                ),
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

[modular architecture]: https://cakeissues.net/latest/documentation/how-cake-issues-works/
[set of aliases]: https://cakeissues.net/latest/api/
[Cake Frosting]: https://cakebuild.net/docs/running-builds/runners/cake-frosting
[Cake .NET Tool]: https://cakebuild.net/docs/running-builds/runners/dotnet-tool
