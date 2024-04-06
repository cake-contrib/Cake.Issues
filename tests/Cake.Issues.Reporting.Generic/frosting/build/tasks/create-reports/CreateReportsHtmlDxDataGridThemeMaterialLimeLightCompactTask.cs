using Cake.Frosting;
using Cake.Issues.Reporting;
using Cake.Issues.Reporting.Generic;

[TaskName("Create-Reports-HtmlDxDataGrid-Theme-MaterialLimeLightCompact")]
[IsDependentOn(typeof(AnalyzeTask))]
public class CreateReportsHtmlDxDataGridThemeMaterialLimeLightCompactTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.CreateIssueReport(
            context.Issues,
            context.GenericIssueReportFormatFromEmbeddedTemplate(
                GenericIssueReportTemplate.HtmlDxDataGrid,
                settings => 
                    settings
                        .WithOption(HtmlDxDataGridOption.Theme, DevExtremeTheme.MaterialLimeLightCompact)),
            context.RepoRootFolder,
            context.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-theme-materiallimelightcompact.html"));

    }
}