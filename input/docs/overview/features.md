---
Order: 20
Description: Overview about core features.
---
The Cake Issues addins for Cake allows you to read issues from any analyzer or linter,
create reports or write issues to comments in pull requests.

# Recipe Functionality

See [Supported Tools] for a list of tools supported by [Cake.Issues.Recipe].

# Supported Core Functionality

The core addins provide a modular architecture, allowing to easily enhance it for supporting additional analyzers, linters,
report formats and code review systems.

## Cake.Issues

Addin for creating and reading issues providing the following functionality:

* [NewIssue] alias for creating issues in the build script.
* [ReadIssues] aliases for reading issues from an issue provider.
* Support for reading issues from multiple issue providers.
* Support for reading issues in multiple formats (Plain text, Markdown, HTML) if supported by issue provider.
* Support for creating links to file & location on source code hosting system (GitHub, Azure Repos, etc).
* Support for passing additional run information to identify specific runs.

## Cake.Issues.Reporting

Addin for creating reports providing the following functionality:

* [CreateIssueReport] aliases for creating reports about issues.
* Support for creating reports with issues from multiple issue providers.

## Cake.Issues.PullRequests

Addin for writing issues as comments to pull requests providing the following functionality:

* [ReportIssuesToPullRequest] aliases for writing issues as comments to pull requests.
* Support for reporting issues from multiple issue providers.
* Support for passing custom issue filter routines in `ReportIssuesToPullRequestSettings.IssueFilters`.
* Advanced support to limit number of maximum issues per run, across multiple runs or per issue provider by setting
  `ReportIssuesToPullRequestSettings.MaxIssuesToPost`, `ReportIssuesToPullRequestSettings.MaxIssuesToPostAcrossRuns`,
  `ReportIssuesToPullRequestSettings.MaxIssuesToPostForEachIssueProvider` and `ReportIssuesToPullRequestSettings.ProviderIssueLimits`.
* Returns all issues as provided by the issue providers and the issues reported to the pull request.

Concrete pull request systems can implement optional capabilities which will provide the following functionality:

* Filter by modified files ([BaseFilteringByModifiedFilesCapability])
  * Filtering issues to only those related to changed files in a pull request.
* Check commit ID ([BaseCheckingCommitIdCapability])
  * Skipping posting of issues if checked source code is outdated by setting `ReportIssuesToPullRequestSettings.CommitId`.
* Support for discussion threads ([BaseDiscussionThreadsCapability])
  * Automatic resolving of issues fixed in subsequent commits.
  * Automatic reopening of still existing issues which are already closed on pull request.
  * Comparing issues by identifier to not rely on message or line numbers.

# Supported Issue Providers

See [Issue Provider Addins] for a list of currently supported analyzers and linters.

# Supported Report Formats

See [Report Format Addins] for a list of currently supported report output formats.

# Supported Pull Request Systems

See [Pull Request System Addins] for a list of currently supported pull request systems.

[Supported Tools]: ../recipe/supported-tools
[Cake.Issues.Recipe]: ../recipe/
[NewIssue]: ../../api/Cake.Issues/Aliases/DC3A3FD7
[ReadIssues]: ../../api/Cake.Issues/Aliases/713F15FD
[CreateIssueReport]: ../../api/Cake.Issues.Reporting/Aliases/C778C70A
[ReportIssuesToPullRequest]: ../../api/Cake.Issues.PullRequests/Aliases/5350C413
[BaseFilteringByModifiedFilesCapability]: ../../api/Cake.Issues.PullRequests/BaseFilteringByModifiedFilesCapability_1
[BaseCheckingCommitIdCapability]: ../../api/Cake.Issues.PullRequests/BaseCheckingCommitIdCapability_1
[BaseDiscussionThreadsCapability]: ../../api/Cake.Issues.PullRequests/BaseDiscussionThreadsCapability_1
[Issue Provider Addins]: ../../addins/issue-provider/
[Report Format Addins]: ../../addins/reporting-format/
[Pull Request System Addins]: ../../addins/pull-request-system/
