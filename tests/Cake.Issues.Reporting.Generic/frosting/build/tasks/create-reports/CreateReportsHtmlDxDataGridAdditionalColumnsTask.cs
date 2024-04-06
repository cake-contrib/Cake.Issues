using Cake.Frosting;
using Cake.Issues.Reporting;
using Cake.Issues.Reporting.Generic;
using System.Collections.Generic;

[TaskName("Create-Reports-HtmlDxDataGrid-Additional-Columns")]
[IsDependentOn(typeof(AnalyzeTask))]
public class CreateReportsHtmlDxDataGridAdditionalColumnsTask : FrostingTask<BuildContext>
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
                            HtmlDxDataGridOption.AdditionalColumns,
                            new List<HtmlDxDataGridColumnDescription>
                            {
                                new HtmlDxDataGridColumnDescription(
                                    "IsSrcFolder",
                                    issue =>
                                    {
                                        return issue.AffectedFileRelativePath?.FullPath.StartsWith("src/");
                                    })
                                {
                                    Caption = "Source Folder",
                                }
                            })),
            context.RepoRootFolder,
            context.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-additionalcolumns.html"));

    }
}