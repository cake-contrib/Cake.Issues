using Cake.Frosting;
using Cake.Issues.Reporting;
using Cake.Issues.Reporting.Generic;
using System.Collections.Generic;

[TaskName("Create-Reports-HtmlDxDataGrid-Sorting")]
[IsDependentOn(typeof(AnalyzeTask))]
public class CreateReportsHtmlDxDataGridSortingTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.CreateIssueReport(
            context.Issues,
            context.GenericIssueReportFormatFromEmbeddedTemplate(
                GenericIssueReportTemplate.HtmlDxDataGrid,
                settings => 
                    settings
                        .WithOption(HtmlDxDataGridOption.SortedColumns, new List<ReportColumn> { ReportColumn.Rule })
                        .WithOption(HtmlDxDataGridOption.RuleSortOrder, ColumnSortOrder.Descending)),
            context.RepoRootFolder,
            context.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-sorting.html"));

    }
}