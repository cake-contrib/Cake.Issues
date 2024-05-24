# Addin for reading code analyzer or linter issues for the Cake build automation system

The issues addin for Cake allows you to read issue from any code analyzer or linter.

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

> **NOTE**:
> [Cake.Issues.Recipe](https://www.nuget.org/packages/Cake.Issues.Recipe) for Cake .NET Tool and
> [Cake.Frosting.Issues.Recipe](https://www.nuget.org/packages/Cake.Frosting.Issues.Recipe)
> provide a single NuGet package, which can be used inside your projects build to add fully flavored issue management,
> including parsing of linter outputs, integration with build systems and pull requests and creation of different reports.

### Reading issues

The addin provides the `ReadIssues` alias to read issues.
It needs an additional NuGet package to provide the specific issue provider implementation:

| Cake .NET Tool Addin | Cake Frosting Addin | Description |
|:--:|-|-|
| [Cake.Issues.MsBuild](https://www.nuget.org/packages/Cake.Issues.MsBuild) | [Cake.Frosting.Issues.MsBuild](https://www.nuget.org/packages/Cake.Frosting.Issues.MsBuild) | Issue provider for reading MsBuild errors and warnings. |
| [Cake.Issues.DocFx](https://www.nuget.org/packages/Cake.Issues.DocFx) | [Cake.Frosting.Issues.DocFx](https://www.nuget.org/packages/Cake.Frosting.Issues.DocFx) | Issue provider for reading DocFx warnings. |
| [Cake.Issues.EsLint](https://www.nuget.org/packages/Cake.Issues.EsLint) | [Cake.Frosting.Issues.EsLint](https://www.nuget.org/packages/Cake.Frosting.Issues.EsLint) | Issue provider for reading ESLint issues. |
| [Cake.Issues.GitRepository](https://www.nuget.org/packages/Cake.Issues.GitRepository) | [Cake.Frosting.Issues.GitRepository](https://www.nuget.org/packages/Cake.Frosting.Issues.GitRepository) | Issue provider for analyzing Git repositories. |
| [Cake.Issues.InspectCode](https://www.nuget.org/packages/Cake.Issues.InspectCode) | [Cake.Frosting.Issues.InspectCode](https://www.nuget.org/packages/Cake.Frosting.Issues.InspectCode) | Issue provider for reading JetBrains Inspect Code issues. |
| [Cake.Issues.Markdownlint](https://www.nuget.org/packages/Cake.Issues.Markdownlint) | [Cake.Frosting.Issues.Markdownlint](https://www.nuget.org/packages/Cake.Frosting.Issues.Markdownlint) | Issue provider for reading issues from markdownlint. |
| [Cake.Issues.Sarif](https://www.nuget.org/packages/Cake.Issues.Sarif) | [Cake.Frosting.Issues.Sarif](https://www.nuget.org/packages/Cake.Frosting.Issues.Sarif) | Issue provider for reading SARIF reports. |
| [Cake.Issues.Terraform](https://www.nuget.org/packages/Cake.Issues.Terraform) | [Cake.Frosting.Issues.Terraform](https://www.nuget.org/packages/Cake.Frosting.Issues.Terraform) | Issue provider for reading Terraform validation output. |

See [Issue Providers] for a list of available issue providers and detailed documentation.

### Creating issues

To create issues directly in the build script the `NewIssue` alias can be used:

```csharp
[TaskName("Create-Issue")]
public sealed class CreateIssueTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var issue =
            context
                .NewIssue(
                    "Something went wrong",
                    "MyCakeScript",
                    "My Cake Script")
                .WithMessageInHtmlFormat("Something went <b>wrong</b>")
                .WithMessageInMarkdownFormat("Something went **wrong**")
                .InFile("myfile.txt", 42)
                .WithPriority(IssuePriority.Warning)
                .Create();

        context.Information("Issue created with message: {0}", issues.MessageText);
    }
}
```

### Creating reports

Issue reports can be created using any of the available [Report Format] addin
together with the [Cake.Issues.Reporting](https://www.nuget.org/packages/Cake.Issues.Reporting) addin for Cake .NET Tool or
[Cake.Frosting.Issues.Reporting](https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting) addin for Cake Frosting.

| Cake .NET Tool Addin | Cake Frosting Addin | Description |
|:--:|-|-|
| [Cake.Issues.Reporting.Console](https://www.nuget.org/packages/Cake.Issues.Reporting.Console) | [Cake.Frosting.Issues.Reporting.Console](https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting.Console) | Support for reporting issues to the console. |
| [Cake.Issues.Reporting.Generic](https://www.nuget.org/packages/Cake.Issues.Reporting.Generic) | [Cake.Frosting.Issues.Reporting.Generic](https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting.Generic) | Support for creating reports in any text based format (HTML, Markdown, ...). |
| [Cake.Issues.Reporting.Sarif](https://www.nuget.org/packages/Cake.Issues.Reporting.Sarif) | [Cake.Frosting.Issues.Reporting.Sarif](https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting.Sarif) | Support for creating reports in SARIF format. |

### Reporting issues to pull requests and build systems

Issues can be written as comments to pull requests or reported to build systems using any of the
available [Pull Request System] addin together with the
[Cake.Issues.PullRequests](https://www.nuget.org/packages/Cake.Issues.PullRequests) addin for Cake .NET Tool or
[Cake.Frosting.Issues.PullRequests](https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests) addin for Cake Frosting.


| Cake .NET Addin | Cake Frosting Addin | Description |
|:--:|-|-|
| [Cake.Issues.PullRequests.AppVeyor](https://www.nuget.org/packages/Cake.Issues.PullRequests.AppVeyor) | [Cake.Frosting.Issues.PullRequests.AppVeyor](https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.AppVeyor) | Integration with AppVeyor builds. |
| [Cake.Issues.PullRequests.AzureDevOps](https://www.nuget.org/packages/Cake.Issues.PullRequests.AzureDevOps) | [Cake.Frosting.Issues.PullRequests.AzureDevOps](https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.AzureDevOps) | Integration with Azure DevOps pull requests. |
| [Cake.Issues.PullRequests.GitHubActions](https://www.nuget.org/packages/Cake.Issues.PullRequests.GitHubActions) | [Cake.Frosting.Issues.PullRequests.GitHubActions](https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.GitHubActions) | Integration with GitHub Actions. |

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
[Issue Providers]: https://cakeissues.net/addins/issue-provider/
[Report Format]: https://cakeissues.net/docs/report-formats/
[Pull Request System]: https://cakeissues.net/docs/pull-request-systems/
