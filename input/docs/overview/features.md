---
Order: 10
Description: Overview about core features.
---
The Cake Issues addins for Cake allows you to read issues from any analyzer or linter,
create reports or write issues to comments in pull requests.

## Supported Core Functionality

The core addins provide the following functionality:

* Modular architecture, allowing to easily enhance it for supporting additional analyzers, linters,
  report formarts and code review systems.
* `Cake.Issues` addin for reading issues
  * `ReadIssues` alias for reading issues.
    This can for example be used to fail builds on certain conditions.
  * Support for reporting issues from multiple issue providers.
* `Cake.Issues.Reporting` addin for creating reports
  * `CreateIssueReport` alias for creating reports about issues.
* `Cake.Issues.PullRequests` addin for writing issues as comments to pull requests
  * `ReportIssuesToPullRequest` alias for writing issues as comments to pull requests.
  * Support for reporting issues from multiple issue providers.
  * Filtering issues to only those related to changes in a pull request.
  * Automatic resolving of issues fixed in subsequent commits.
  * Comparing issues by content to not rely on line numbers.
  * Limit number of maximum issues to post.
  * Returns all issues as provided by the issue providers and the issues reported to the pull request.

## Supported Issue Providers

See [Issue Provider Addins] for a list of currently supported analyzers and linters.

## Supported Report Formats

See [Report Format Addins] for a list of currently supported report output formats.

## Supported Pull Request Systems

See [Pull Request System Addins] for a list of currently supported pull request systems.

[Issue Provider Addins]: ../../addins/issue-provider/
[Report Format Addins]: ../../addins/report-format/
[Pull Request System Addins]: ../../addins/pull-request-system/
