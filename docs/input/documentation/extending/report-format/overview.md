---
title: Overview
description: Overview how to implement support for a report output format.
---

Report formats need to implement the [IIssueReportFormat](https://cakebuild.net/api/Cake.Issues.Reporting/IIssueReportFormat/) interface.
For simplifying implementation there exists an abstract [IssueReportFormat](https://cakebuild.net/api/Cake.Issues.Reporting/IssueReportFormat/)
base class from which concrete implementation can be inherited.
