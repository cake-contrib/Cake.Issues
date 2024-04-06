using Cake.Frosting;
using Cake.Issues.Reporting;
using Cake.Issues.Reporting.Generic;

[TaskName("Create-Reports-HtmlDxDataGrid-Theme-DarkCompact")]
[IsDependentOn(typeof(AnalyzeTask))]
public class CreateReportsHtmlDxDataGridThemeDarkCompactTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.CreateIssueReport(
            context.Issues,
            context.GenericIssueReportFormatFromEmbeddedTemplate(
                GenericIssueReportTemplate.HtmlDxDataGrid,
                settings => 
                    settings
                        .WithOption(HtmlDxDataGridOption.Theme, DevExtremeTheme.DarkCompact)),
            context.RepoRootFolder,
            context.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-theme-darkcompact.html"));

    }
}