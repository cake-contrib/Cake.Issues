# Cake Issues Addin

[![License](https://img.shields.io/:license-mit-blue.svg)](https://github.com/cake-contrib/Cake.Issues/blob/develop/LICENSE)
[![Documentation](https://img.shields.io/badge/Documentation-blue)](https://cakeissues.net)
[![Join in the discussion on the Cake repository](https://img.shields.io/badge/GitHub-Discussions-green?logo=github)](https://github.com/orgs/cake-build/discussions/categories/extension-q-a)
![Stable Release](https://img.shields.io/nuget/v/Cake.Issues?label=Stable)
![Pre-Release)](https://img.shields.io/nuget/vpre/Cake.Issues?label=Pre-Release)

The Cake.Issues addins for the [Cake build automation system](https://cakebuild.net) offer an extensive and flexible solution for reading linting issues.

Cake.Issues redefines issue management within the Cake build system by offering a comprehensive, universal, and extensible solution.
The unique capabilities of the addins empower development teams to enforce coding standards, generate insightful reports,
seamlessly incorporate various linting tools, and streamlining the integration with pull requests.
With its [modular architecture] and extensive [set of aliases], Cake.Issues provides a future-proof infrastructure for issue management
in Cake builds, fostering a more efficient and adaptable development process.

For more information about the addins see the [Cake.Issues website](https://cakeissues.net).
For general information about the Cake build automation system see the [Cake website](http://cakebuild.net).

## Table of Contents

- [Background](#background)
- [Install](#install)
- [Usage](#usage)
- [Support & Discussion](#support--discussion)
- [Addins](#addins)
- [API](#api)
- [Contributing](#contributing)
- [License](#license)

## Background

### Unique Problem Solving

Some examples how Cake.Issues can help development teams to improve code quality.

**Break build on linting issues:**

Cake.Issues provides a seamless integration, allowing you to enforce coding standards by breaking builds when linting issues are detected.

**Reports:**

Craft detailed and visually appealing reports for linting issues directly within your Cake build.
The addins facilitates easy identification and resolution of linting concerns, enhancing the overall code quality.

**Pull Requests integration:**

Ensure linting issues are promptly addressed by having them reported as comments on pull requests.
Cake.Issues bridges the gap between linting tools and version control systems, fostering efficient collaboration during code reviews.

### Universal Compatibility

**Build System Agnosticism:**

Embrace the freedom to choose the build system that best suit your needs.
If your current build system lacks tasks for reporting issues in pull requests, Cake.Issues steps in to fill that void seamlessly.
In the case of using multiple CI services, Cake.Issues guarantees a consistent feature set across all of them.

**Diverse Linting Tool Support:**

Regardless of the linting tools you use, Cake.Issues ensures that you're not left out.
Cake.Issues supports a [variety of analyzers and linters], allowing you to incorporate new tools effortlessly
while maintaining integration with existing ones.

### Unprecedented Extensibility

**Modular Architecture:**

The Cake.Issues addin breaks away from the norm by offering a [modular architecture].
Comprising [over 15 distinct addins], it presents a cohesive solution through more than [75 aliases] for Cake builds,
providing unparalleled flexibility.

**Extensible Infrastructure:**

Designed with extensibility in mind, Cake.Issues provides [extension points] for supporting additional analyzers, linters,
report formats, and code review systems.
This adaptability ensures that your build scripts can evolve with the ever-changing landscape of development tools.

## Install

Integrating Cake.Issues into your Cake build is straightforward.
Developers can quickly configure the addins to work with their preferred linting tools and version control system.
With minimal setup, teams can enjoy the benefits of enhanced code quality management seamlessly integrated into their existing build pipeline.

The Cake Issues addins are deployed as NuGet packages.

In Cake Scripting running they can be added using the [addin preprocessor directive](https://cakebuild.net/docs/writing-builds/preprocessor-directives/addin):

```csharp
#addin nuget:?package=Cake.Issues&version=x.x.x
```

In Cake Frosting they can be added using package references:

```csharp
<PackageReference Include="Cake.Issues" Version="x.x.x" />
```

See [list of available addins](https://cakeissues.net/addins/).

### Compatibility

See [Release Notes](https://cakeissues.net/docs/overview/release-notes/Cake.Issues) for requirements for each specific version.

## Usage

See [Website](https://cakeissues.net/docs/usage/) for detailed usage instructions.

## Support & Discussion

For questions and to discuss ideas & feature requests, use the [GitHub discussions on the Cake GitHub repository](https://github.com/cake-build/cake/discussions), under the [Extension Q&A](https://github.com/orgs/cake-build/discussions/categories/extension-q-a) category.

## Addins

| Cake Scripting Addin | Cake Frosting Addin | Description |
|:--:|-|-|
| [Cake.Issues](https://www.nuget.org/packages/Cake.Issues) | [Cake.Issues](https://www.nuget.org/packages/Cake.Issues) | Addin providing the aliases for creating and reading of issues. |
| [Cake.Issues.MsBuild](https://www.nuget.org/packages/Cake.Issues.MsBuild) | [Cake.Frosting.Issues.MsBuild](https://www.nuget.org/packages/Cake.Frosting.Issues.MsBuild) | Issue provider for reading MsBuild errors and warnings. |
| [Cake.Issues.DocFx](https://www.nuget.org/packages/Cake.Issues.DocFx) | [Cake.Issues.DocFx](https://www.nuget.org/packages/Cake.Issues.DocFx) | Issue provider for reading DocFx warnings. |
| [Cake.Issues.EsLint](https://www.nuget.org/packages/Cake.Issues.EsLint) | [Cake.Issues.EsLint](https://www.nuget.org/packages/Cake.Issues.EsLint) | Issue provider for reading ESLint issues. |
| [Cake.Issues.GitRepository](https://www.nuget.org/packages/Cake.Issues.GitRepository) | [Cake.Issues.GitRepository](https://www.nuget.org/packages/Cake.Issues.GitRepository) | Issue provider for analyzing Git repositories. |
| [Cake.Issues.InspectCode](https://www.nuget.org/packages/Cake.Issues.InspectCode) | [Cake.Issues.InspectCode](https://www.nuget.org/packages/Cake.Issues.InspectCode) | Issue provider for reading JetBrains Inspect Code issues. |
| [Cake.Issues.Markdownlint](https://www.nuget.org/packages/Cake.Issues.Markdownlint) | [Cake.Issues.Markdownlint](https://www.nuget.org/packages/Cake.Issues.Markdownlint) | Issue provider for reading issues from markdownlint. |
| [Cake.Issues.Sarif](https://www.nuget.org/packages/Cake.Issues.Sarif) | [Cake.Frosting.Issues.Sarif](https://www.nuget.org/packages/Cake.Frosting.Issues.Sarif) | Issue provider for reading SARIF reports. |
| [Cake.Issues.Terraform](https://www.nuget.org/packages/Cake.Issues.Terraform) | [Cake.Issues.Sarif](https://www.nuget.org/packages/Cake.Issues.Terraform) | Issue provider for reading Terraform validation output. |
| [Cake.Issues.PullRequests](https://www.nuget.org/packages/Cake.Issues.PullRequests) | [Cake.Frosting.Issues.PullRequests](https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests) | Addin providing the aliases for writing issues to pull requests and build servers. |
| [Cake.Issues.PullRequests.AppVeyor](https://www.nuget.org/packages/Cake.Issues.PullRequests.AppVeyor) | [Cake.Frosting.Issues.PullRequests.AppVeyor](https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.AppVeyor) | Integration with AppVeyor builds. |
| [Cake.Issues.PullRequests.AzureDevOps](https://www.nuget.org/packages/Cake.Issues.PullRequests.AzureDevOps) | [Cake.Frosting.Issues.PullRequests.AzureDevOps](https://www.nuget.org/packages/Cake.Frosting.Issues.PullRequests.AzureDevOps) | Integration with Azure DevOps pull requests. |
| [Cake.Issues.PullRequests.GitHubActions](https://www.nuget.org/packages/Cake.Issues.PullRequests.GitHubActions) | [Cake.Issues.PullRequests.AzureDevOps](https://www.nuget.org/packages/Cake.Issues.PullRequests.AzureDevOps) | Integration with GitHub Actions. |
| [Cake.Issues.Reporting](https://www.nuget.org/packages/Cake.Issues.Reporting) | [Cake.Frosting.Issues.Reporting](https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting) | Addin providing the aliases for creating reports. |
| [Cake.Issues.Reporting.Console](https://www.nuget.org/packages/Cake.Issues.Reporting.Console) | [Cake.Frosting.Issues.Reporting.Console](https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting.Console) | Support for reporting issues to the console. |
| [Cake.Issues.Reporting.Generic](https://www.nuget.org/packages/Cake.Issues.Reporting.Generic) | [Cake.Frosting.Issues.Reporting.Generic](https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting.Generic) | Support for creating reports in any text based format (HTML, Markdown, ...). |
| [Cake.Issues.Reporting.Sarif](https://www.nuget.org/packages/Cake.Issues.Reporting.Sarif) | [Cake.Frosting.Issues.Reporting.Sarif](https://www.nuget.org/packages/Cake.Frosting.Issues.Reporting.Sarif) | Support for creating reports in SARIF format. |

## API

The Cake Issues addins provide a wide range of additional aliases which can be used in Cake builds.
See [API reference](https://cakeissues.net/dsl/) for an overview.

## Contributing

Contributions are welcome. See [Contribution Guidelines](CONTRIBUTING.md).

## License

[MIT License - Copyright © Cake Issues contributors](LICENSE)

Binary distributions for some addins contain third-party code which is licensed under its own respective license.
See [LICENSE](LICENSE) for details.

[modular architecture]: https://cakeissues.net/docs/fundamentals/architecture
[extension points]: https://cakeissues.net/docs/extending/
[set of aliases]: https://cakeissues.net/dsl/
[variety of analyzers and linters]: https://cakeissues.net/addins/issue-provider/
[over 15 distinct addins]: https://cakeissues.net/addins/
[75 aliases]: https://cakeissues.net/dsl/
