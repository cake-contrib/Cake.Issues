---
title: New addin for printing issues to console
category: Release Notes
---

A new [Cake.Issues.Reporting.Console addin] has been released which allows to output issues to the console.

<!--excerpt-->

[Cake.Issues.Reporting.Console addin] can be used to print issues to the console and is built on top
of the excellent [Errata library] by Patrik Svensson.

Its main focus is to annotate source code with issues:

![Source annotation](2021-08-29-diagnostics.png "Source annotation")

It currently only supports issues containing line and column information.
Output can be grouped by rule, like in the image above, or individual entries for every issue.

Beside printing issues it can also show summary tables.

There's one summary which shows the number of issues for everyprovider and run:

![Summary by provider & rule](2021-08-29-summary-by-provider.png "Summary by provider & rule")

Another summary shows the number of issues by priority for every provider and run:

![Summary of priorities](2021-08-29-summary-of-priorities.png "Summary of priorities")

[Cake.Issues.Reporting.Console addin]: /docs/report-formats/console/
[Errata library]: https://github.com/spectreconsole/errata
