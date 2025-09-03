---
title: HTML Diagnostic
description: Template for a HTML report containing a list of all issues with all properties.
---

Template for a HTML report containing a list of all issues with all properties.

![HTML Diagnostic](htmldiagnostic01.png "HTML Diagnostic")

## Features

- [x] Unstyled table listing all properties of [IIssue](https://cakebuild.net/api/Cake.Issues/IIssue/)
- [x] No internet access required for displaying.

## Requirements

* No additional requirements.

## Usage

To create a report using the HTML diagnostic template you can use the
[GenericIssueReportTemplate.HtmlDiagnostic](https://cakebuild.net/api/Cake.Issues.Reporting.Generic/GenericIssueReportTemplate/4F88BD05)
enum value:

=== "Cake .NET Tool"

    ```csharp
    CreateIssueReport(
        issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDiagnostic),
        @"c:\repo",
        @"c:\report.html");
    ```

=== "Cake Frosting"

    ```csharp
    context.CreateIssueReport(
        issues,
        context.GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDiagnostic),
        @"c:\repo",
        @"c:\report.html");
    ```

## Options

This template doesn't support any options.

## Demos

* [Default](htmldiagnostic-demo-default.html)

## Source Code

!!! tip
    You can use the source code as a template for your [custom template].

Source code is available on [GitHub](https://github.com/cake-contrib/Cake.Issues/blob/develop/src/Cake.Issues.Reporting.Generic/Templates/Diagnostic.cshtml).

[custom template]: ../examples/custom-template.md
