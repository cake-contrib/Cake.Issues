namespace Cake.Issues.Reporting.Generic
{
    /// <summary>
    /// Options for the <see cref="GenericIssueReportTemplate.HtmlDxDataGrid"/> template
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
        /// Default value is <c>true</c>.
        /// </summary>
        LineVisible,

        /// <summary>
        /// Sort order of the <see cref="ReportColumn.Line"/> column if it is part of <see cref="SortedColumns"/>.
        /// See <see cref="ColumnSortOrder"/> for possible values.
        /// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        /// </summary>
        LineSortOrder,

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
        /// Default value is <see cref="ReportColumn.ProviderName"/>.
        /// </summary>
        GroupedColumns,

        /// <summary>
        /// List of <see cref="ReportColumn"/> which should be sorted.
        /// Default value is <see cref="ReportColumn.PriorityName"/>, <see cref="ReportColumn.ProjectName"/>,
        /// <see cref="ReportColumn.FileDirectory"/>, <see cref="ReportColumn.FileName"/>, <see cref="ReportColumn.Line"/>.
        /// </summary>
        SortedColumns,

        /// <summary>
        /// List of <see cref="HtmlDxDataGridColumnDescription"/> for additional columsn which should be added to the grid.
        /// Default value is an empty list.
        /// </summary>
        AdditionalColumns,

        /// <summary>
        /// Settings for having issues linked to files.
        /// Value needs to be an instance of <see cref="FileLinkSettings"/>.
        /// Default value is <c>null</c>.
        /// </summary>
        FileLinkSettings
    }
}
