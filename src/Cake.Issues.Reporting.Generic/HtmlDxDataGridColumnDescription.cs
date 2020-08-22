namespace Cake.Issues.Reporting.Generic
{
    using System;

    /// <summary>
    /// Description of a column in the <see cref="GenericIssueReportTemplate.HtmlDxDataGrid"/> template.
    /// </summary>
    public class HtmlDxDataGridColumnDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlDxDataGridColumnDescription"/> class.
        /// </summary>
        /// <param name="id">Id of the column.</param>
        /// <param name="valueRetriever">Function for retrieving the value of the column.</param>
        public HtmlDxDataGridColumnDescription(string id, Func<IIssue, object> valueRetriever)
        {
            id.NotNullOrWhiteSpace(nameof(id));
            valueRetriever.NotNull(nameof(valueRetriever));

            this.Id = id;
            this.ValueRetriever = valueRetriever;
        }

        /// <summary>
        /// Gets the ID of the column.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets a function for retrieving the value of the column.
        /// </summary>
        public Func<IIssue, object> ValueRetriever { get; }

        /// <summary>
        /// Gets or sets the caption of the column.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the position of the column regarding other columns.
        /// See <see cref="ReportColumn"/> for values of default columns.
        /// Default value is zero, which means that the column will be added before any default columns.
        /// </summary>
        public int VisibleIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether data can be filtered by this column or not.
        /// Applies only if <see cref="HtmlDxDataGridOption.EnableFiltering"/> is set.
        /// Default value is <c>true</c>.
        /// </summary>
        public bool AllowFiltering { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the user can group data by values of this column or not.
        /// Applies only if <see cref="HtmlDxDataGridOption.EnableGrouping"/> is set.
        /// Default value is <c>true</c>.
        /// </summary>
        public bool AllowGrouping { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether a user can sort rows by this column at runtime or not.
        /// Default value is <c>true</c>.
        /// </summary>
        public bool AllowSorting { get; set; } = true;
    }
}
