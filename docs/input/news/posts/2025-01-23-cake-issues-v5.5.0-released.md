---
title: Cake Issues v5.5.0 Released
date: 2025-01-23
categories:
  - Release Notes
links:
  - documentation/report-formats/generic/templates/htmldxdatagrid.md
---

Cake Issues version 5.5.0 has been released bringing improvements to HTML reports.

<!-- more -->

This post shows the highlights included in this release.
For update instructions skip to [Updating from previous versions](#updating-from-previous-versions).

❤ Huge thanks to our community! This release would not have been possible without your support and contributions! ❤

People working on this release:

* [eoehen](https://github.com/eoehen)
* [lulicool](https://github.com/lulicool)
* [pascalberger](https://github.com/pascalberger)

## Improvements for reports

This version adds multiple improvements and new features to the [HtmlDxDataGrid template] of the
Cake.Issues.Reporting.Generic addin.

### Column chooser

By default the report comes with a column chooser which allows to hide specific columns by users.

![Column chooser](2025-01-23-column-chooser.gif "Column chooser")

Column chooser can be disabled through the `ShowColumnChooser` option:

=== "Cake .NET Tool"

    ```csharp
    Task("Create-Reports-HtmlDxDataGrid-Disable-Column-Chooser")
        .IsDependentOn("Analyze")
        .Does<BuildData>(data =>
    {
        CreateIssueReport(
            data.Issues,
            GenericIssueReportFormatFromEmbeddedTemplate(
                GenericIssueReportTemplate.HtmlDxDataGrid,
                settings => settings
                    .WithOption(
                        HtmlDxDataGridOption.ShowColumnChooser,
                        false)),
            data.RepoRootFolder,
            data.TemplateGalleryFolder
                .CombineWithFilePath("output.html"));
    });
    ```

=== "Cake Frosting"

    ```csharp
    [TaskName("Create-Reports-HtmlDxDataGrid-Disable-Column-Chooser")]
    [IsDependentOn(typeof(AnalyzeTask))]
    public class CreateReportsHtmlDxDataGridDisableColumnChooserTask
        : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            context.CreateIssueReport(
                context.Issues,
                context.GenericIssueReportFormatFromEmbeddedTemplate(
                    GenericIssueReportTemplate.HtmlDxDataGrid,
                    settings => 
                        settings
                            .WithOption(
                                HtmlDxDataGridOption.ShowColumnChooser,
                                false)),
                context.RepoRootFolder,
                context.TemplateGalleryFolder
                    .CombineWithFilePath("output.html"));
        }
    }
    ```

### Infinite scrolling

There is a new option which will result in a grid with infinite scrolling instead of multiple pages:

![Infinite scrolling](2025-01-23-infinite-scrolling.gif "Infinite scrolling")

By default pager is used.
To enable infinite scrolling the `DisplayMode` option needs to be set to `HtmlDxDataGridDisplayMode.InfiniteScroll`:

=== "Cake .NET Tool"

    ```csharp
    Task("Create-Reports-HtmlDxDataGrid-InfiniteScrolling")
        .IsDependentOn("Analyze")
        .Does<BuildData>(data =>
    {
        CreateIssueReport(
            data.Issues,
            GenericIssueReportFormatFromEmbeddedTemplate(
                GenericIssueReportTemplate.HtmlDxDataGrid,
                settings => settings
                    .WithOption(
                        HtmlDxDataGridOption.DisplayMode,
                        HtmlDxDataGridDisplayMode.InfiniteScroll)),
            data.RepoRootFolder,
            data.TemplateGalleryFolder
                .CombineWithFilePath("output.html"));
    });
    ```

=== "Cake Frosting"

    ```csharp
    [TaskName("Create-Reports-HtmlDxDataGrid-InfiniteScrolling")]
    [IsDependentOn(typeof(AnalyzeTask))]
    public class CreateReportsHtmlDxDataGridInfiniteScrollingTask
        : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            context.CreateIssueReport(
                context.Issues,
                context.GenericIssueReportFormatFromEmbeddedTemplate(
                    GenericIssueReportTemplate.HtmlDxDataGrid,
                    settings => 
                        settings
                            .WithOption(
                                HtmlDxDataGridOption.DisplayMode,
                                HtmlDxDataGridDisplayMode.InfiniteScroll)),
                context.RepoRootFolder,
                context.TemplateGalleryFolder
                    .CombineWithFilePath("output.html"));
        }
    }
    ```

### Fluent themes

It is now possible to create reports using Fluent themes.

!!! note
    Fluent themes are only available in newer versions of the underlying DevExtreme license,
    which require a commercial license.
    See [Using commercial version] for details.

### State persistence

Starting with this version, customizations made by the user are by default persisted and re-applied next time the report is shown.

If multiple reports are created, or Cake Issues is used in multiple repositories, all reports share by default the same storage.
This can be changed per generated report using the `HtmlDxDataGridOption.StorageKey` option:

=== "Cake .NET Tool"

    ```csharp
    Task("Create-Reports-HtmlDxDataGrid-Custom-Storage-Key")
        .IsDependentOn("Analyze")
        .Does<BuildData>(data =>
    {
        CreateIssueReport(
            data.Issues,
            GenericIssueReportFormatFromEmbeddedTemplate(
                GenericIssueReportTemplate.HtmlDxDataGrid,
                settings => settings
                    .WithOption(
                        HtmlDxDataGridOption.StorageKey,
                        "CustomStorageKey")),
            data.RepoRootFolder,
            data.TemplateGalleryFolder
                .CombineWithFilePath("output.html"));
    });
    ```

=== "Cake Frosting"

    ```csharp
    [TaskName("Create-Reports-HtmlDxDataGrid-Custom-Storage-Key")]
    [IsDependentOn(typeof(AnalyzeTask))]
    public class CreateReportsHtmlDxDataGridCustomStorageKeyTask
        : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            context.CreateIssueReport(
                context.Issues,
                context.GenericIssueReportFormatFromEmbeddedTemplate(
                    GenericIssueReportTemplate.HtmlDxDataGrid,
                    settings => 
                        settings
                            .WithOption(
                                HtmlDxDataGridOption.StorageKey,
                                "CustomStorageKey")),
                context.RepoRootFolder,
                context.TemplateGalleryFolder
                    .CombineWithFilePath("output.html"));
        }
    }
    ```

Persistance can be disabled using the `HtmlDxDataGridOption.PersistState` option:

=== "Cake .NET Tool"

    ```csharp
    Task("Create-Reports-HtmlDxDataGrid-Disable-Persistence")
        .IsDependentOn("Analyze")
        .Does<BuildData>(data =>
    {
        CreateIssueReport(
            data.Issues,
            GenericIssueReportFormatFromEmbeddedTemplate(
                GenericIssueReportTemplate.HtmlDxDataGrid,
                settings => settings
                    .WithOption(
                        HtmlDxDataGridOption.PersistState,
                        false)),
            data.RepoRootFolder,
            data.TemplateGalleryFolder
                .CombineWithFilePath("output.html"));
    });
    ```

=== "Cake Frosting"

    ```csharp
    [TaskName("Create-Reports-HtmlDxDataGrid-Disable-Persistence")]
    [IsDependentOn(typeof(AnalyzeTask))]
    public class CreateReportsHtmlDxDataGridDisablePersistenceTask
        : FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            context.CreateIssueReport(
                context.Issues,
                context.GenericIssueReportFormatFromEmbeddedTemplate(
                    GenericIssueReportTemplate.HtmlDxDataGrid,
                    settings => 
                        settings
                            .WithOption(
                                HtmlDxDataGridOption.PersistState,
                                false)),
                context.RepoRootFolder,
                context.TemplateGalleryFolder
                    .CombineWithFilePath("output.html"));
        }
    }
    ```

### Usability improvements

Multiple changes have been made to improve usability:

* Data grid uses the full page height.
  In previous version it could have been, that the grid was higher than the visible part of the page,
  which meant that the user had to scroll down to see the pager to navigate through the grid.
* It is now possible to define the number of records shown on a single page.
* The width of the column adapts to the content.
  Columns which contained whitespace before are now smaller, resulting in more space for columns containing more text.
* Word wrap has been enabled.
  If content of a cell was more than what fit into the cell, previously the text was truncated.
  Starting with this version, the whole text will be shown across multiple lines.
* When using a Material theme the size of the title has been reduced, giving more space to the grid.

### Documentation updates

[HtmlDxDataGrid template] has been updated with additional information and examples for Cake Frosting.

## Updating from previous versions

Cake.Issues 5.5.0 addins are compatible with any 5.x addins.
To update to the new version bump the version of the specific addins.

For details see [release notes](https://github.com/cake-contrib/Cake.Issues/releases/tag/5.5.0)

[HtmlDxDataGrid template]: ../../documentation/report-formats/generic/templates/htmldxdatagrid.md
[Using commercial version]:  ../../documentation/report-formats/generic/templates/htmldxdatagrid.md#using-commercial-version
