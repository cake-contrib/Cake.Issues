namespace Cake.Issues.Reporting.Generic
{
    /// <summary>
    /// Options for the <see cref="GenericIssueReportTemplate.HtmlDxDataGrid"/> template.
    /// </summary>
    public enum HtmlDxDataGridOption
    {
        /// <summary>
        /// Title of the report.
        /// Default value is <c>Issues Report</c>.
        /// </summary>
        Title,

        /// <summary>
        /// DevExtreme theme to use.
        /// See <see cref="DevExtremeTheme"/> for possible values.
        /// Default value is <see cref="DevExtremeTheme.Light"/>.
        /// </summary>
        Theme,

        /// <summary>
        /// Flag if the title should be shown as header on the top of the page.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        ShowHeader,

        /// <summary>
        /// Flag if the search panel for full text searching should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        EnableSearching,

        /// <summary>
        /// Flag if the group panel which allows end-user grouping should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// If <c>false</c> grouping defined by <see cref="GroupedColumns"/> is still applied.
        /// Default value is <c>true</c>.
        /// </summary>
        EnableGrouping,

        /// <summary>
        /// Flag if filtering should be available or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        EnableFiltering,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.ProviderType"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>false</c>.
        /// </summary>
        ProviderTypeVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.ProviderType"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        ProviderTypeSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.ProviderName"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        ProviderNameVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.ProviderName"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        ProviderNameSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.Run"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c> if any issue contains run information, otherwise <c>false</c>.
        /// </summary>
        RunVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.Run"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        RunSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.Priority"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>false</c>.
        /// </summary>
        PriorityVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.Priority"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Descending"/>.
        /// </summary>
        PrioritySortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.PriorityName"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        PriorityNameVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.PriorityName"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Descending"/>.
        /// </summary>
        PriorityNameSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.ProjectPath"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>false</c>.
        /// </summary>
        ProjectPathVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.ProjectPath"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        ProjectPathSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.ProjectName"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        ProjectNameVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.ProjectName"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        ProjectNameSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.FilePath"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>false</c>.
        /// </summary>
        FilePathVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.FilePath"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        FilePathSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.FileDirectory"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        FileDirectoryVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.FileDirectory"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        FileDirectorySortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.FileName"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        FileNameVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.FileName"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        FileNameSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.Line"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>false</c>.
        /// </summary>
        LineVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.Line"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        LineSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.EndLine"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>false</c>.
        /// </summary>
        EndLineVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.EndLine"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        EndLineSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.Column"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>false</c>.
        /// </summary>
        ColumnVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.Column"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        ColumnSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.EndColumn"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>false</c>.
        /// </summary>
        EndColumnVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.EndColumn"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        EndColumnSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.Location"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        LocationVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.Location"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        LocationSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.Rule"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        RuleVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.Rule"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        RuleSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.RuleUrl"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>false</c>.
        /// </summary>
        RuleUrlVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.RuleUrl"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        RuleUrlSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.Message"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        MessageVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.Message"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        MessageSortOrder,

        /// <summary>
        /// List of <see cref="ReportColumn"/> which should be grouped.
        /// Grouped columns are always visible.
        /// Default value is <see cref="ReportColumn.ProviderName"/> and <see cref="ReportColumn.Run"/>.
        /// </summary>
        GroupedColumns,

        /// <summary>
        /// List of <see cref="ReportColumn"/> which should be sorted.
        /// Default value is <see cref="ReportColumn.PriorityName"/>, <see cref="ReportColumn.ProjectName"/>,
        /// <see cref="ReportColumn.FileDirectory"/>, <see cref="ReportColumn.FileName"/>, <see cref="ReportColumn.Line"/>.
        /// </summary>
        SortedColumns,

        /// <summary>
        /// List of <see cref="HtmlDxDataGridColumnDescription"/> for additional columns which should be added to the grid.
        /// Default value is an empty list.
        /// </summary>
        AdditionalColumns,

        /// <summary>
        /// Location where jQuery can be found.
        /// The following files need to be available:
        /// <list type="bullet">
        /// <item>
        /// <description><c>{JQueryLocation}/jquery-{JQueryVersion}.min.js</c></description>
        /// </item>
        /// </list>
        /// Default value is <c>https://ajax.aspnetcdn.com/ajax/jquery/</c>.
        /// </summary>
        JQueryLocation,

        /// <summary>
        /// Version of jQuery which should be used.
        /// This version needs to match the version required by the selected <see cref="DevExtremeVersion"/>.
        /// Default value is <c>3.5.0</c>.
        /// </summary>
        JQueryVersion,

        /// <summary>
        /// Location where the DevExtreme libraries can be found.
        /// Below the location there needs to be a folder matching <see cref="DevExtremeVersion"/> and
        /// inside there subfolders <c>js</c> and <c>css</c>.
        /// The following files need to be available:
        /// <list type="bullet">
        /// <item>
        /// <description><c>{DevExtremeLocation}/{DevExtremeVersion}/js/dx.all.js</c></description>
        /// </item>
        /// <item>
        /// <description><c>{DevExtremeLocation}/{DevExtremeVersion}/css/dx.common.css</c></description>
        /// </item>
        /// <item>
        /// <description><c>{DevExtremeLocation}/{DevExtremeVersion}/css/{Theme}</c></description>
        /// </item>
        /// </list>
        /// Default value is <c>https://cdn3.devexpress.com/jslib/</c>.
        /// </summary>
        DevExtremeLocation,

        /// <summary>
        /// Version of the DevExtreme libraries which should be used.
        /// If setting this the matching <see cref="JQueryVersion"/> needs to also be set.
        /// Default value is <c>20.1.6</c>.
        /// </summary>
        DevExtremeVersion,

        /// <summary>
        /// Settings for having functionality to open files affected by issues in IDEs.
        /// Value needs to be an instance of <see cref="IdeIntegrationSettings"/>.
        /// Default value is <c>null</c>.
        /// </summary>
        IdeIntegrationSettings,

        /// <summary>
        /// Flag if exporting to Microsoft Excel should be available or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>false</c>.
        /// </summary>
        /// <remarks>
        /// Exporting won't work on Safari on macOS due to missing API for saving files.
        /// </remarks>
        EnableExporting,

        /// <summary>
        /// Default name of Microsoft Excel file without file name extension.
        /// Default value is <c>issue-report</c>.
        /// </summary>
        ExportFileName,

        /// <summary>
        /// Location where JSZip can be found.
        /// Below the location there needs to be a folder matching <see cref="JsZipVersion"/>.
        /// The following files need to be available:
        /// <list type="bullet">
        /// <item>
        /// <description><c>{JsZipLocation}/{JsZipVersion}/jszip.min.js</c></description>
        /// </item>
        /// </list>
        /// Default value is <c>https://cdnjs.cloudflare.com/ajax/libs/jszip/</c>.
        /// </summary>
        JsZipLocation,

        /// <summary>
        /// Version of JsZip which should be used.
        /// This version needs to match the version required by the selected <see cref="DevExtremeVersion"/>.
        /// Default value is <c>3.2.2</c>.
        /// </summary>
        JsZipVersion,
    }
}
