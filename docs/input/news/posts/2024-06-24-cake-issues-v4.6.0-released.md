---
title: Cake Issues v4.6.0 Released
date: 2024-06-24
categories:
  - Release Notes
search:
  boost: 0.5
links:
  - documentation/issue-providers/sarif/index.md
---

Cake Issues version 4.6.0 has been released with improvements for SARIF reports.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [christianbumann](https://github.com/christianbumann){target="_blank"}
* [eoehen](https://github.com/eoehen){target="_blank"}
* [pascalberger](https://github.com/pascalberger){target="_blank"}
* [Speeedy01](https://github.com/Speeedy01){target="_blank"}

## Improvements for SARIF Reports

A new option [SarifIssueReportFormatSettings.ExistingIssues]{target="_blank"} has been introduced which allows to pass in a list of known
issues not related to current code changes, resulting in the [baselineState property]{target="_blank"} being set in the resulting SARIF report.

This property is for example be used in the [SARIF viewer extension for Azure Pipelines]{target="_blank"} as filter option.

The following new settings options have been added:

| SarifIssueReportFormatSettings property | Output property                                      | Description                              |
|-----------------------------------------|------------------------------------------------------|------------------------------------------|
| [Guid]{target="_blank"}                 | [automationDetails.guid]{target="_blank"}            | Unique, stable identifier for the run    |
| [BaselineGuid]{target="_blank"}         | [run.baselineGuid]{target="_blank"}                  | String equal to Guid of a previous run   |
| [CorrelationGuid]{target="_blank"}      | [automationDetails.correlationGuid]{target="_blank"} | Guid shared by all runs of the same type |

## Updating from previous versions

Cake.Issues 4.6.0 addins are compatible with any 4.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/4.6.0){target="_blank"}

[SarifIssueReportFormatSettings.ExistingIssues]: https://cakebuild.net/api/Cake.Issues.Reporting.Sarif/SarifIssueReportFormatSettings/B37B3648
[baselineState property]: https://docs.oasis-open.org/sarif/sarif/v2.1.0/os/sarif-v2.1.0-os.html#_Toc34317662
[SARIF viewer extension for Azure Pipelines]: https://marketplace.visualstudio.com/items?itemName=sariftools.sarif-viewer-build-tab
[Guid]: https://cakebuild.net/api/Cake.Issues.Reporting.Sarif/SarifIssueReportFormatSettings/F52A2FFC
[automationDetails.guid]: https://docs.oasis-open.org/sarif/sarif/v2.0/csprd02/sarif-v2.0-csprd02.html#_Toc10127718
[BaselineGuid]: https://cakebuild.net/api/Cake.Issues.Reporting.Sarif/SarifIssueReportFormatSettings/54F97E33
[run.baselineGuid]: https://docs.oasis-open.org/sarif/sarif/v2.0/csprd02/sarif-v2.0-csprd02.html#_Toc10127680
[CorrelationGuid]: https://cakebuild.net/api/Cake.Issues.Reporting.Sarif/SarifIssueReportFormatSettings/51613F52
[automationDetails.correlationGuid]: https://docs.oasis-open.org/sarif/sarif/v2.0/csprd02/sarif-v2.0-csprd02.html#_Toc10127719
