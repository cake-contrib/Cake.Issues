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

### Reading issues

The addin provides the `ReadIssues` alias to read issues.
It needs an additional NuGet packages to provide the specific issue provider implementation.

See [Issue Providers] for a list of available issue providers.

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

Issue reports can be created using any of the available [Report Format] addin.

### Reporting issues to pull requests and build systems

Issues can be written as comments to pull requests or reported to build systems using any of the
available [Pull Request System] addin.

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
