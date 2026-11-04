---
title: HTML DevExtreme Data Grid
description: Template for a HTML report containing a rich data grid with sorting, filtering, grouping and search capabilities.
---

Template for a HTML report containing a rich data grid with sorting, filtering, grouping and search capabilities powered by [DevExtreme].

![HTML DevExtreme Data Grid](htmldxdatagrid01.png "HTML DevExtreme Data Grid")

## Features

- [x] Table with `Provider`, `Run`, `Severity`, `Project`, `Directory`, `File`, `Location`, `Rule ID`, `Rule Name`, `Message` by default.
- [x] Support for grouping by multiple columns by user.
- [x] Total number of issues by each group level.
- [x] Each column sortable by user.
- [x] Data can be filtered by any column by user.
- [x] User customizations persisted in local storage.
- [x] Paged view.
- [x] Infinite scrolling.
- [x] Client-side full text search.
- [x] Client-side export to Microsoft Excel or PDF.
- [x] Fully customizable through [options](#options).

## Requirements

* Cake.Issues.Reporting.Generic 0.3.1 or higher
* The template ships with DevExtreme version `23.1`, which was available under a non-commercial license.
  Some features in the template require a never version and a commercial [License] to be set.
  See [Using commercial version] for examples.

## Usage

To create a report using the HTML DevExtreme Data Grid template you can use the [GenericIssueReportTemplate.HtmlDxDataGrid] enum value:

=== "Cake .NET Tool"

    ```csharp
    CreateIssueReport(
        issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
          GenericIssueReportTemplate.HtmlDxDataGrid),
        @"c:\repo",
        @"c:\report.html");
    ```

=== "Cake Frosting"

    ```csharp
    context.CreateIssueReport(
        issues,
        context.GenericIssueReportFormatFromEmbeddedTemplate(
          GenericIssueReportTemplate.HtmlDxDataGrid),
        @"c:\repo",
        @"c:\report.html");
    ```

### Using commercial version

The template ships with DevExtreme version `23.1`, which was available under a non-commercial license.
To use a never version a commercial [License] is required.

To use the template with a never version and commercial license the following options need to be set:

=== "Cake .NET Tool"

    ```csharp
    CreateIssueReport(
        issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(
                    HtmlDxDataGridOption.DevExtremeLicenseKey,
                    "<LICENSE_KEY>")
                .WithOption(
                    HtmlDxDataGridOption.DevExtremeVersion,
                    "23.2")),
        @"c:\repo",
        @"c:\report.html");
    ```

=== "Cake Frosting"

    ```csharp
    context.CreateIssueReport(
        issues,
        context.GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(
                    HtmlDxDataGridOption.DevExtremeLicenseKey,
                    "<LICENSE_KEY>")
                .WithOption(
                      HtmlDxDataGridOption.DevExtremeVersion,
                      "23.2")),
        @"c:\repo",
        @"c:\report.html");
    ```

## Options

See [HtmlDxDataGridOption] for a list of possible options.

## Demos

The following demo shows the template with its default options:

=== "Cake .NET Tool"

    * [Default](htmldxdatagrid-demo-default.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-default.cake))

=== "Cake Frosting"

    * [Default](htmldxdatagrid-demo-default.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridDefaultTask.cs))

### Themes

The template supports the teams defined in the [DevExtremeTheme] enumeration which can be set
using the [HtmlDxDataGridOption.Theme]:

=== "Cake .NET Tool"

    ```csharp
    CreateIssueReport(
        issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(
                    HtmlDxDataGridOption.Theme,
                    DevExtremeTheme.MaterialBlueLight)),
        @"c:\repo",
        @"c:\report.html");
    ```

    * [Light Theme](htmldxdatagrid-demo-theme-light.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-light.cake))
    * [Dark Theme](htmldxdatagrid-demo-theme-dark.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-dark.cake))
    * [Contrast Theme](htmldxdatagrid-demo-theme-contrast.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-contrast.cake))
    * [Carmine Theme](htmldxdatagrid-demo-theme-carmine.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-carmine.cake))
    * [Dark Moon Theme](htmldxdatagrid-demo-theme-darkmoon.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-darkmoon.cake))
    * [Soft Blue Theme](htmldxdatagrid-demo-theme-softblue.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-softblue.cake))
    * [Dark Violet Theme](htmldxdatagrid-demo-theme-darkviolet.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-darkviolet.cake))
    * [Green Mist Theme](htmldxdatagrid-demo-theme-greenmist.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-greenmist.cake))
    * [Light Compact Theme](htmldxdatagrid-demo-theme-lightcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-lightcompact.cake))
    * [Dark Compact Theme](htmldxdatagrid-demo-theme-darkcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-darkcompact.cake))
    * [Contrast Compact Theme](htmldxdatagrid-demo-theme-contrastcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-contrastcompact.cake))
    * [Material Blue Light Theme](htmldxdatagrid-demo-theme-materialbluelight.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialbluelight.cake))
    * [Material Lime Light Theme](htmldxdatagrid-demo-theme-materiallimelight.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materiallimelight.cake))
    * [Material Orange Light Theme](htmldxdatagrid-demo-theme-materialorangelight.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialorangelight.cake))
    * [Material Purple Light Theme](htmldxdatagrid-demo-theme-materialpurplelight.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialpurplelight.cake))
    * [Material Teal Light Theme](htmldxdatagrid-demo-theme-materialteallight.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialteallight.cake))
    * [Material Blue Dark Theme](htmldxdatagrid-demo-theme-materialbluedark.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialbluedark.cake))
    * [Material Lime Dark Theme](htmldxdatagrid-demo-theme-materiallimedark.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materiallimedark.cake))
    * [Material Orange Dark Theme](htmldxdatagrid-demo-theme-materialorangedark.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialorangedark.cake))
    * [Material Purple Dark Theme](htmldxdatagrid-demo-theme-materialpurpledark.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialpurpledark.cake))
    * [Material Teal Dark Theme](htmldxdatagrid-demo-theme-materialtealdark.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialtealdark.cake))
    * [Material Blue Light Compact Theme](htmldxdatagrid-demo-theme-materialbluelightcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialbluelightcompact.cake))
    * [Material Lime Light Compact Theme](htmldxdatagrid-demo-theme-materiallimelightcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materiallimelightcompact.cake))
    * [Material Orange Light Compact Theme](htmldxdatagrid-demo-theme-materialorangelightcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialorangelightcompact.cake))
    * [Material Purple Light Compact Theme](htmldxdatagrid-demo-theme-materialpurplelightcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialpurplelightcompact.cake))
    * [Material Teal Light Compact Theme](htmldxdatagrid-demo-theme-materialteallightcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialteallightcompact.cake))
    * [Material Blue Dark Compact Theme](htmldxdatagrid-demo-theme-materialbluedarkcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialbluedarkcompact.cake))
    * [Material Lime Dark Compact Theme](htmldxdatagrid-demo-theme-materiallimedarkcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materiallimedarkcompact.cake))
    * [Material Orange Dark Compact Theme](htmldxdatagrid-demo-theme-materialorangedarkcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialorangedarkcompact.cake))
    * [Material Purple Dark Compact Theme](htmldxdatagrid-demo-theme-materialpurpledarkcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialpurpledarkcompact.cake))
    * [Material Teal Dark Compact Theme](htmldxdatagrid-demo-theme-materialtealdarkcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-theme-materialtealdarkcompact.cake))
    * Fluent Blue Light Theme <!-- md:badge commercial -->
    * Fluent SaaS Light Theme <!-- md:badge commercial -->
    * Fluent Blue Light Compact Theme <!-- md:badge commercial -->
    * Fluent SaaS Light Compact Theme <!-- md:badge commercial -->
    * Fluent Blue Dark Theme <!-- md:badge commercial -->
    * Fluent SaaS Dark Theme <!-- md:badge commercial -->
    * Fluent Blue Dark Compact Theme <!-- md:badge commercial -->
    * Fluent SaaS Dark Compact Theme <!-- md:badge commercial -->

=== "Cake Frosting"

    ```csharp
    context.CreateIssueReport(
        issues,
        context.GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(
                      HtmlDxDataGridOption.Theme,
                      DevExtremeTheme.MaterialBlueLight)),
        @"c:\repo",
        @"c:\report.html");
    ```

    * [Light Theme](htmldxdatagrid-demo-theme-light.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeLightTask.cs))
    * [Dark Theme](htmldxdatagrid-demo-theme-dark.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeDarkTask.cs))
    * [Contrast Theme](htmldxdatagrid-demo-theme-contrast.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeContrastTask.cs))
    * [Carmine Theme](htmldxdatagrid-demo-theme-carmine.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeCarmineTask.cs))
    * [Dark Moon Theme](htmldxdatagrid-demo-theme-darkmoon.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeDarkMoonTask.cs))
    * [Soft Blue Theme](htmldxdatagrid-demo-theme-softblue.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeSoftBlueTask.cs))
    * [Dark Violet Theme](htmldxdatagrid-demo-theme-darkviolet.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeDarkVioletTask.cs))
    * [Green Mist Theme](htmldxdatagrid-demo-theme-greenmist.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeGreenMistTask.cs))
    * [Light Compact Theme](htmldxdatagrid-demo-theme-lightcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeLightCompactTask.cs))
    * [Dark Compact Theme](htmldxdatagrid-demo-theme-darkcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeDarkCompactTask.cs))
    * [Contrast Compact Theme](htmldxdatagrid-demo-theme-contrastcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeContrastCompactTask.cs))
    * [Material Blue Light Theme](htmldxdatagrid-demo-theme-materialbluelight.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialBlueLightTask.cs))
    * [Material Lime Light Theme](htmldxdatagrid-demo-theme-materiallimelight.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialLimeLightTask.cs))
    * [Material Orange Light Theme](htmldxdatagrid-demo-theme-materialorangelight.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialOrangeLightTask.cs))
    * [Material Purple Light Theme](htmldxdatagrid-demo-theme-materialpurplelight.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialPurpleLightTask.cs))
    * [Material Teal Light Theme](htmldxdatagrid-demo-theme-materialteallight.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialTealLightTask.cs))
    * [Material Blue Dark Theme](htmldxdatagrid-demo-theme-materialbluedark.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialBlueDarkTask.cs))
    * [Material Lime Dark Theme](htmldxdatagrid-demo-theme-materiallimedark.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialLimeDarkTask.cs))
    * [Material Orange Dark Theme](htmldxdatagrid-demo-theme-materialorangedark.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialOrangeDarkTask.cs))
    * [Material Purple Dark Theme](htmldxdatagrid-demo-theme-materialpurpledark.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialPurpleDarkTask.cs))
    * [Material Teal Dark Theme](htmldxdatagrid-demo-theme-materialtealdark.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialTealDarkTask.cs))
    * [Material Blue Light Compact Theme](htmldxdatagrid-demo-theme-materialbluelightcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialBlueLightCompactTask.cs))
    * [Material Lime Light Compact Theme](htmldxdatagrid-demo-theme-materiallimelightcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialLimeLightCompactTask.cs))
    * [Material Orange Light Compact Theme](htmldxdatagrid-demo-theme-materialorangelightcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialOrangeLightCompactTask.cs))
    * [Material Purple Light Compact Theme](htmldxdatagrid-demo-theme-materialpurplelightcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialPurpleLightCompactTask.cs))
    * [Material Teal Light Compact Theme](htmldxdatagrid-demo-theme-materialteallightcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialTealLightCompactTask.cs))
    * [Material Blue Dark Compact Theme](htmldxdatagrid-demo-theme-materialbluedarkcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialBlueDarkCompactTask.cs))
    * [Material Lime Dark Compact Theme](htmldxdatagrid-demo-theme-materiallimedarkcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialLimeDarkCompactTask.cs))
    * [Material Orange Dark Compact Theme](htmldxdatagrid-demo-theme-materialorangedarkcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialOrangeDarkCompactTask.cs))
    * [Material Purple Dark Compact Theme](htmldxdatagrid-demo-theme-materialpurpledarkcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialPurpleDarkCompactTask.cs))
    * [Material Teal Dark Compact Theme](htmldxdatagrid-demo-theme-materialtealdarkcompact.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridThemeMaterialTealDarkCompactTask.cs))
    * Fluent Blue Light Theme <!-- md:badge commercial -->
    * Fluent SaaS Light Theme <!-- md:badge commercial -->
    * Fluent Blue Light Compact Theme <!-- md:badge commercial -->
    * Fluent SaaS Light Compact Theme <!-- md:badge commercial -->
    * Fluent Blue Dark Theme <!-- md:badge commercial -->
    * Fluent SaaS Dark Theme <!-- md:badge commercial -->
    * Fluent Blue Dark Compact Theme <!-- md:badge commercial -->
    * Fluent SaaS Dark Compact Theme <!-- md:badge commercial -->

### Column visibility

Visible columns can be defined using the `ColumnNameVisible` option:

=== "Cake .NET Tool"

    ```csharp
    CreateIssueReport(
        issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(
                    HtmlDxDataGridOption.LineVisible,
                    false)),
        @"c:\repo",
        @"c:\report.html");
    ```

    Additional columns can be added using the [HtmlDxDataGridOption.AdditionalColumns] option.

    * [Show and hide columns](htmldxdatagrid-demo-columnhiding.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-hide-columns.cake))
    * [Add additional columns](htmldxdatagrid-demo-additionalcolumns.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-additional-columns.cake))
    * [Hide column chooser](htmldxdatagrid-demo-disablecolumnchooser.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-disable-column-chooser.cake))

=== "Cake Frosting"

    ```csharp
    context.CreateIssueReport(
        issues,
        context.GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(
                    HtmlDxDataGridOption.LineVisible,
                    false)),
        @"c:\repo",
        @"c:\report.html");
    ```

    Additional columns can be added using the [HtmlDxDataGridOption.AdditionalColumns] option.

    * [Show and hide columns](htmldxdatagrid-demo-columnhiding.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridHideColumnsTask.cs))
    * [Add additional columns](htmldxdatagrid-demo-additionalcolumns.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridAdditionalColumnsTask.cs))
    * [Hide column chooser](htmldxdatagrid-demo-disablecolumnchooser.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridDisableColumnChooserTask.cs))

### Sorting

Sorted columns can be defined using the [HtmlDxDataGridOption.SortedColumns] and the
`ColumnNameSortOder` options:

=== "Cake .NET Tool"

    ```csharp
    CreateIssueReport(
        issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(
                    HtmlDxDataGridOption.SortedColumns,
                    new List<ReportColumn> { ReportColumn.RuleId })
                .WithOption(
                    HtmlDxDataGridOption.RuleIdSortOrder,
                    ColumnSortOrder.Descending )),
        @"c:\repo",
        @"c:\report.html");
    ```

    * [Change sorting](htmldxdatagrid-demo-sorting.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-sorting.cake))

=== "Cake Frosting"

    ```csharp
    context.CreateIssueReport(
        issues,
        context.GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(
                    HtmlDxDataGridOption.SortedColumns,
                    new List<ReportColumn> { ReportColumn.RuleId })
                .WithOption(
                    HtmlDxDataGridOption.RuleIdSortOrder, 
                    ColumnSortOrder.Descending )),
        @"c:\repo",
        @"c:\report.html");
    ```

    * [Change sorting](htmldxdatagrid-demo-sorting.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridSortingTask.cs))

### Grouping

Grouping can be defined using the [HtmlDxDataGridOption.GroupedColumns] option:

=== "Cake .NET Tool"

    ```csharp
    CreateIssueReport(
        issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(
                    HtmlDxDataGridOption.GroupedColumns, 
                    new List<ReportColumn> { ReportColumn.RuleId })),
        @"c:\repo",
        @"c:\report.html");
    ```

    * [Change grouping](htmldxdatagrid-demo-grouping.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-grouping.cake))
    * [Disable grouping](htmldxdatagrid-demo-disablegrouping.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-disable-grouping.cake))

=== "Cake Frosting"

    ```csharp
    context.CreateIssueReport(
        issues,
        context.GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(
                    HtmlDxDataGridOption.GroupedColumns, 
                    new List<ReportColumn> { ReportColumn.RuleId })),
        @"c:\repo",
        @"c:\report.html");
    ```

    * [Change grouping](htmldxdatagrid-demo-grouping.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridGroupingTask.cs))
    * [Disable grouping](htmldxdatagrid-demo-disablegrouping.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridDisableGroupingTask.cs))

### Exporting

Exporting can be enabled using the [HtmlDxDataGridOption.EnableExporting] option:

=== "Cake .NET Tool"

    ```csharp
    CreateIssueReport(
        issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(
                    HtmlDxDataGridOption.EnableExporting,
                    true)),
        @"c:\repo",
        @"c:\report.html");
    ```

    * [Enable exporting](htmldxdatagrid-demo-enableexporting.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-enable-exporting.cake))
    * [Microsoft Excel export (*.xlsx)](htmldxdatagrid-demo-exportformat-xlsx.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-exportformat-xlsx.cake))
    * [PDF export (*.pdf)](htmldxdatagrid-demo-exportformat-pdf.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-exportformat-pdf.cake))
    * [Custom export file name](htmldxdatagrid-demo-customexportfilename.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-custom-export-filename.cake))

=== "Cake Frosting"

    ```csharp
    context.CreateIssueReport(
        issues,
        context.GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(
                    HtmlDxDataGridOption.EnableExporting,
                    true)),
        @"c:\repo",
        @"c:\report.html");
    ```

    * [Enable exporting](htmldxdatagrid-demo-enableexporting.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridEnableExportingTask.cs))
    * [Microsoft Excel export (*.xlsx)](htmldxdatagrid-demo-exportformat-xlsx.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridExportFormatXlsxTask.cs))
    * [PDF export (*.pdf)](htmldxdatagrid-demo-exportformat-pdf.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridExportFormatPdfTask.cs))
    * [Custom export file name](htmldxdatagrid-demo-customexportfilename.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridCustomExportFilenameTask.cs))

### State persistence

By default customizations made by the user are persisted and re-applied next time the report is shown.
If multiple reports are created, or Cake Issues is used in multiple repositories, all reports share by default the same storage.

This can be changed per generated report using the `HtmlDxDataGridOption.StorageKey` option.
Persistance can be disabled using the `HtmlDxDataGridOption.PersistState` option.

=== "Cake .NET Tool"

    * [Custom storage key](htmldxdatagrid-demo-custom-storage-key.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-custom-storage-key.cake))
    * [Disable state persistence](htmldxdatagrid-demo-disable-persistence.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-disable-persistence.cake))

=== "Cake Frosting"

    * [Custom storage key](htmldxdatagrid-demo-custom-storage-key.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridCustomStorageKeyTask.cs))
    * [Disable state persistence](htmldxdatagrid-demo-disable-persistence.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridDisablePersistenceTask.cs))

### Other features

=== "Cake .NET Tool"

    * [Change title](htmldxdatagrid-demo-changetitle.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-change-title.cake))
    * [Disable header](htmldxdatagrid-demo-disableheader.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-disable-header.cake))
    * [Disable filtering](htmldxdatagrid-demo-disablefiltering.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-disable-filtering.cake))
    * [Disable searching](htmldxdatagrid-demo-disablesearching.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-disable-searching.cake))
    * [Infinite scrolling](htmldxdatagrid-demo-infinitescrolling.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-infinitescrolling.cake))
    * [Custom script location and version](htmldxdatagrid-demo-customscriptlocation.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/script-runner/build/create-reports/create-reports-htmldxdatagrid-custom-script-location.cake))

=== "Cake Frosting"

    * [Change title](htmldxdatagrid-demo-changetitle.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridChangeTitleTask.cs))
    * [Disable header](htmldxdatagrid-demo-disableheader.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridDisableHeaderTask.cs))
    * [Disable filtering](htmldxdatagrid-demo-disablefiltering.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridDisableFilteringTask.cs))
    * [Disable searching](htmldxdatagrid-demo-disablesearching.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridDisableSearchingTask.cs))
    * [Infinite scrolling](htmldxdatagrid-demo-infinitescrolling.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridInfiniteScrollingTask.cs))
    * [Custom script location and version](htmldxdatagrid-demo-customscriptlocation.html)
      ([Source Code](https://github.com/cake-contrib/Cake.Issues/blob/develop/tests/Cake.Issues.Reporting.Generic/frosting/build/tasks/create-reports/CreateReportsHtmlDxDataGridCustomScriptLocationTask.cs))

## Source Code

!!! tip
    You can use the source code as a template for your [custom template].

Source code is available on [GitHub].

[DevExtreme]: https://js.devexpress.com
[License]: https://js.devexpress.com/jQuery/Documentation/Guide/Common/Licensing/
[Using commercial version]: #using-commercial-version
[GenericIssueReportTemplate.HtmlDxDataGrid]: https://cakebuild.net/api/Cake.Issues.Reporting.Generic/GenericIssueReportTemplate/0E9E9D94
[HtmlDxDataGridOption]: https://cakebuild.net/api/Cake.Issues.Reporting.Generic/HtmlDxDataGridOption/
[DevExtremeTheme]: https://cakebuild.net/api/Cake.Issues.Reporting.Generic/DevExtremeTheme/
[HtmlDxDataGridOption.Theme]: https://cakebuild.net/api/Cake.Issues.Reporting.Generic/HtmlDxDataGridOption/EA83DCAB
[HtmlDxDataGridOption.AdditionalColumns]: https://cakebuild.net/api/Cake.Issues.Reporting.Generic/HtmlDxDataGridOption/F9860912
[HtmlDxDataGridOption.SortedColumns]: https://cakebuild.net/api/Cake.Issues.Reporting.Generic/HtmlDxDataGridOption/D578E453
[HtmlDxDataGridOption.GroupedColumns]: https://cakebuild.net/api/Cake.Issues.Reporting.Generic/HtmlDxDataGridOption/0907599C
[HtmlDxDataGridOption.EnableExporting]: https://cakebuild.net/api/Cake.Issues.Reporting.Generic/HtmlDxDataGridOption/1441E285
[custom template]: ../examples/custom-template.md
[GitHub]: https://github.com/cake-contrib/Cake.Issues/blob/develop/src/Cake.Issues.Reporting.Generic/Templates/DxDataGrid.cshtml
