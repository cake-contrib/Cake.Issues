namespace Cake.Issues.Reporting.Generic
{
    /// <summary>
    /// Options for the <see cref="GenericIssueReportTemplate.HtmlDxPivotGrid"/> template.
    /// </summary>
    public enum HtmlDxPivotGridOption
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

        ///// <summary>
        ///// Flag if the search panel for full text searching should be visible or not.
        ///// Either <c>true</c> or <c>false</c>.
        ///// Default value is <c>true</c>.
        ///// </summary>
        //EnableSearching,

        ///// <summary>
        ///// Flag if the group panel which allows end-user grouping should be visible or not.
        ///// Either <c>true</c> or <c>false</c>.
        ///// If <c>false</c> grouping defined by <see cref="GroupedColumns"/> is still applied.
        ///// Default value is <c>true</c>.
        ///// </summary>
        //EnableGrouping,

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
        /// Area where the <see cref="ReportColumn.ProviderType"/> column should be shown.
        /// See <see cref="HtmlDxPivotGridArea"/> for possible values.
        /// Default value is <see cref="HtmlDxPivotGridArea.Row"/>.
        /// </summary>
        ProviderTypeArea,

        ///// <summary>
        ///// Sort order of the <see cref="ReportColumn.ProviderType"/> column if it is part of <see cref="SortedColumns"/>.
        ///// See <see cref="ColumnSortOrder"/> for possible values.
        ///// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        ///// </summary>
        //ProviderTypeSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.ProviderName"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        ProviderNameVisible,

        /// <summary>
        /// Area where the <see cref="ReportColumn.ProviderName"/> column should be shown.
        /// See <see cref="HtmlDxPivotGridArea"/> for possible values.
        /// Default value is <see cref="HtmlDxPivotGridArea.Filter"/>.
        /// </summary>
        ProviderNameArea,

        ///// <summary>
        ///// Sort order of the <see cref="ReportColumn.ProviderName"/> column if it is part of <see cref="SortedColumns"/>.
        ///// See <see cref="ColumnSortOrder"/> for possible values.
        ///// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        ///// </summary>
        //ProviderNameSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.Priority"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>false</c>.
        /// </summary>
        PriorityVisible,

        /// <summary>
        /// Area where the <see cref="ReportColumn.Priority"/> column should be shown.
        /// See <see cref="HtmlDxPivotGridArea"/> for possible values.
        /// Default value is <see cref="HtmlDxPivotGridArea.Row"/>.
        /// </summary>
        PriorityArea,

        ///// <summary>
        ///// Sort order of the <see cref="ReportColumn.Priority"/> column if it is part of <see cref="SortedColumns"/>.
        ///// See <see cref="ColumnSortOrder"/> for possible values.
        ///// Default value is <see cref="ColumnSortOrder.Descending"/>.
        ///// </summary>
        //PrioritySortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.PriorityName"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        PriorityNameVisible,

        /// <summary>
        /// Area where the <see cref="ReportColumn.PriorityName"/> column should be shown.
        /// See <see cref="HtmlDxPivotGridArea"/> for possible values.
        /// Default value is <see cref="HtmlDxPivotGridArea.Column"/>.
        /// </summary>
        PriorityNameArea,

        ///// <summary>
        ///// Sort order of the <see cref="ReportColumn.PriorityName"/> column if it is part of <see cref="SortedColumns"/>.
        ///// See <see cref="ColumnSortOrder"/> for possible values.
        ///// Default value is <see cref="ColumnSortOrder.Descending"/>.
        ///// </summary>
        //PriorityNameSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.ProjectPath"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>false</c>.
        /// </summary>
        ProjectPathVisible,

        /// <summary>
        /// Area where the <see cref="ReportColumn.ProjectPath"/> column should be shown.
        /// See <see cref="HtmlDxPivotGridArea"/> for possible values.
        /// Default value is <see cref="HtmlDxPivotGridArea.Row"/>.
        /// </summary>
        ProjectPathArea,

        ///// <summary>
        ///// Sort order of the <see cref="ReportColumn.ProjectPath"/> column if it is part of <see cref="SortedColumns"/>.
        ///// See <see cref="ColumnSortOrder"/> for possible values.
        ///// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        ///// </summary>
        //ProjectPathSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.ProjectName"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        ProjectNameVisible,

        /// <summary>
        /// Area where the <see cref="ReportColumn.ProjectName"/> column should be shown.
        /// See <see cref="HtmlDxPivotGridArea"/> for possible values.
        /// Default value is <see cref="HtmlDxPivotGridArea.Row"/>.
        /// </summary>
        ProjectNameArea,

        ///// <summary>
        ///// Sort order of the <see cref="ReportColumn.ProjectName"/> column if it is part of <see cref="SortedColumns"/>.
        ///// See <see cref="ColumnSortOrder"/> for possible values.
        ///// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        ///// </summary>
        //ProjectNameSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.FilePath"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>false</c>.
        /// </summary>
        FilePathVisible,

        /// <summary>
        /// Area where the <see cref="ReportColumn.FilePath"/> column should be shown.
        /// See <see cref="HtmlDxPivotGridArea"/> for possible values.
        /// Default value is <see cref="HtmlDxPivotGridArea.Row"/>.
        /// </summary>
        FilePathArea,

        ///// <summary>
        ///// Sort order of the <see cref="ReportColumn.FilePath"/> column if it is part of <see cref="SortedColumns"/>.
        ///// See <see cref="ColumnSortOrder"/> for possible values.
        ///// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        ///// </summary>
        //FilePathSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.FileDirectory"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        FileDirectoryVisible,

        /// <summary>
        /// Area where the <see cref="ReportColumn.FileDirectory"/> column should be shown.
        /// See <see cref="HtmlDxPivotGridArea"/> for possible values.
        /// Default value is <see cref="HtmlDxPivotGridArea.Row"/>.
        /// </summary>
        FileDirectoryArea,

        ///// <summary>
        ///// Sort order of the <see cref="ReportColumn.FileDirectory"/> column if it is part of <see cref="SortedColumns"/>.
        ///// See <see cref="ColumnSortOrder"/> for possible values.
        ///// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        ///// </summary>
        //FileDirectorySortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.FileName"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        FileNameVisible,

        /// <summary>
        /// Area where the <see cref="ReportColumn.FileName"/> column should be shown.
        /// See <see cref="HtmlDxPivotGridArea"/> for possible values.
        /// Default value is <see cref="HtmlDxPivotGridArea.Row"/>.
        /// </summary>
        FileNameArea,

        ///// <summary>
        ///// Sort order of the <see cref="ReportColumn.FileName"/> column if it is part of <see cref="SortedColumns"/>.
        ///// See <see cref="ColumnSortOrder"/> for possible values.
        ///// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        ///// </summary>
        //FileNameSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.Line"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        LineVisible,

        /// <summary>
        /// Area where the <see cref="ReportColumn.Line"/> column should be shown.
        /// See <see cref="HtmlDxPivotGridArea"/> for possible values.
        /// Default value is <see cref="HtmlDxPivotGridArea.Row"/>.
        /// </summary>
        LineArea,

        ///// <summary>
        ///// Sort order of the <see cref="ReportColumn.Line"/> column if it is part of <see cref="SortedColumns"/>.
        ///// See <see cref="ColumnSortOrder"/> for possible values.
        ///// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        ///// </summary>
        //LineSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.Rule"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        RuleVisible,

        /// <summary>
        /// Area where the <see cref="ReportColumn.Rule"/> column should be shown.
        /// See <see cref="HtmlDxPivotGridArea"/> for possible values.
        /// Default value is <see cref="HtmlDxPivotGridArea.Data"/>.
        /// </summary>
        RuleArea,

        ///// <summary>
        ///// Sort order of the <see cref="ReportColumn.Rule"/> column if it is part of <see cref="SortedColumns"/>.
        ///// See <see cref="ColumnSortOrder"/> for possible values.
        ///// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        ///// </summary>
        //RuleSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.RuleUrl"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>false</c>.
        /// </summary>
        RuleUrlVisible,

        /// <summary>
        /// Area where the <see cref="ReportColumn.RuleUrl"/> column should be shown.
        /// See <see cref="HtmlDxPivotGridArea"/> for possible values.
        /// Default value is <see cref="HtmlDxPivotGridArea.Row"/>.
        /// </summary>
        RuleUrlArea,

        ///// <summary>
        ///// Sort order of the <see cref="ReportColumn.RuleUrl"/> column if it is part of <see cref="SortedColumns"/>.
        ///// See <see cref="ColumnSortOrder"/> for possible values.
        ///// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        ///// </summary>
        //RuleUrlSortOrder,

        /// <summary>
        /// Flag if the <see cref="ReportColumn.Message"/> column should be visible or not.
        /// Either <c>true</c> or <c>false</c>.
        /// Default value is <c>true</c>.
        /// </summary>
        MessageVisible,

        /// <summary>
        /// Area where the <see cref="ReportColumn.Message"/> column should be shown.
        /// See <see cref="HtmlDxPivotGridArea"/> for possible values.
        /// Default value is <see cref="HtmlDxPivotGridArea.Row"/>.
        /// </summary>
        MessageArea,

        ///// <summary>
        ///// Sort order of the <see cref="ReportColumn.Message"/> column if it is part of <see cref="SortedColumns"/>.
        ///// See <see cref="ColumnSortOrder"/> for possible values.
        ///// Default value is <see cref="ColumnSortOrder.Ascending"/>.
        ///// </summary>
        //MessageSortOrder,

        ///// <summary>
        ///// List of <see cref="ReportColumn"/> which should be grouped.
        ///// Grouped columns are always visible.
        ///// Default value is <see cref="ReportColumn.ProviderName"/>.
        ///// </summary>
        //GroupedColumns,

        ///// <summary>
        ///// List of <see cref="ReportColumn"/> which should be sorted.
        ///// Default value is <see cref="ReportColumn.PriorityName"/>, <see cref="ReportColumn.ProjectName"/>,
        ///// <see cref="ReportColumn.FileDirectory"/>, <see cref="ReportColumn.FileName"/>, <see cref="ReportColumn.Line"/>.
        ///// </summary>
        //SortedColumns,

        ///// <summary>
        ///// List of <see cref="HtmlDxDataGridColumnDescription"/> for additional columsn which should be added to the grid.
        ///// Default value is an empty list.
        ///// </summary>
        //AdditionalColumns,

        ///// <summary>
        ///// Settings for having issues linked to files.
        ///// Value needs to be an instance of <see cref="FileLinkSettings"/>.
        ///// Default value is <c>null</c>.
        ///// </summary>
        //FileLinkSettings,

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
        /// Default value is <c>3.1.0</c>.
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
        /// Default value is <c>18.2.7</c>.
        /// </summary>
        DevExtremeVersion
    }
}
