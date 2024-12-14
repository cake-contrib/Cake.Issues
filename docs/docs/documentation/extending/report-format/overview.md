---
title: Overview
description: Overview how to implement support for a report output format.
---

Report formats need to implement the [IIssueReportFormat](https://cakebuild.net/api/Cake.Issues.Reporting/IIssueReportFormat/){target="_blank"} interface.
For simplifying implementation there exists an abstract [IssueReportFormat](https://cakebuild.net/api/Cake.Issues.Reporting/IssueReportFormat/){target="_blank"}
base class from which concrete implementation can be inherited.
