---
title: HTML Data Table
description: Template for a HTML report containing a rich data table view with sorting and search functionality.
---

Template for a HTML report containing a rich data table view with sorting and search functionality powered by
[Simple-DataTables](https://github.com/fiduswriter/Simple-DataTables){target="_blank"}.

![HTML Data Table](htmldatatable01.png "HTML Data Table")

## Features

- [x] Separate table for issues of each issue provider.
- [x] Table with `Severity`, `Project`, `Path`, `File`, `Location`, `Rule`, `Message`.
- [x] Each column sortable by user.
- [x] Paged table with possibility for user to change number of entries per page.
- [x] Client-side full text search.
- [x] No internet access required for displaying.

## Requirements

* Cake.Issues.Reporting.Generic 0.2.1 or higher

## Usage

To create a report using the HTML Data Table template you can use the
[GenericIssueReportTemplate.HtmlDataTable](https://cakebuild.net/api/Cake.Issues.Reporting.Generic/GenericIssueReportTemplate/62ADE81F){target="_blank"}
enum value:

=== "Cake .NET Tool"

    ```csharp
    CreateIssueReport(
        issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDataTable),
        @"c:\repo",
        @"c:\report.html");
    ```

=== "Cake Frosting"

    ```csharp
    context.CreateIssueReport(
        issues,
        context.GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDataTable),
        @"c:\repo",
        @"c:\report.html");
    ```

## Options

This template doesn't support any options.

## Demos

* [Default](htmldatatable-demo-default.html){target="_blank"}

## Source Code

!!! tip
    You can use the source code as a template for your [custom template].

Source code is available on [GitHub](https://github.com/cake-contrib/Cake.Issues/blob/develop/src/Cake.Issues.Reporting.Generic/Templates/DataTable.cshtml){target="_blank"}.

[custom template]: ../examples/custom-template.md
