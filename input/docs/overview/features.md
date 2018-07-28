---
Order: 10
Description: Overview about core features.
---
The Cake Issues addins for Cake allows you to read issues from any analyzer or linter,
create reports or write issues to comments in pull requests.

# Supported Core Functionality

The core addins provide a modular architecture, allowing to easily enhance it for supporting additional analyzers, linters,
report formats and code review systems.

## Cake.Issues

Addin for creating and reading issues providing the following functionality:

* [NewIssue] alias for creating issues in the build script.
* [ReadIssues] aliases for reading issues from an issue provider.
* Support for reading issues from multiple issue providers.
* Support for reading issues in specific format (Plain text, Markdown, HTML) if supported by issue provider.

## Cake.Issues.Reporting

Addin for creating reports providing the following functionality:
  
* [CreateIssueReport] aliases for creating reports about issues.
* Support for creating reports with issues from multiple issue providers.

## Cake.Issues.PullRequests

Addin for writing issues as comments to pull requests providing the following functionality:

* [ReportIssuesToPullRequest] aliases for writing issues as comments to pull requests.
* Support for reporting issues from multiple issue providers.
* Filtering issues to only those related to changed files in a pull request.
* Support for passing custom issue filter routines in `ReportIssuesToPullRequestSettings.IssueFilters`.
* Skipping posting of issues if checked source code is outdated by setting `ReportIssuesToPullRequestSettings.CommitId`.
* Automatic resolving of issues fixed in subsequent commits.
* Automatic reopening of still existing issues which are already closed on pull request.
* Comparing issues by content to not rely on line numbers.
* Limit number of maximum issues to post globally or per issue provider by setting
  `ReportIssuesToPullRequestSettings.MaxIssuesToPostForEachIssueProvider` or `ReportIssuesToPullRequestSettings.MaxIssuesToPost`.
* Returns all issues as provided by the issue providers and the issues reported to the pull request.

# Supported Issue Providers

See [Issue Provider Addins] for a list of currently supported analyzers and linters.

# Supported Report Formats

See [Report Format Addins] for a list of currently supported report output formats.

# Supported Pull Request Systems

See [Pull Request System Addins] for a list of currently supported pull request systems.

[NewIssue]: ../../api/Cake.Issues/Aliases/DC3A3FD7
[ReadIssues]: ../../api/Cake.Issues/Aliases/713F15FD
[CreateIssueReport]: ../../api/Cake.Issues.Reporting/Aliases/C778C70A
[ReportIssuesToPullRequest]: ../../api/Cake.Issues.PullRequests/Aliases/5350C413
[Issue Provider Addins]: ../../addins/issue-provider/
[Report Format Addins]: ../../addins/reporting-format/
[Pull Request System Addins]: ../../addins/pull-request-system/
