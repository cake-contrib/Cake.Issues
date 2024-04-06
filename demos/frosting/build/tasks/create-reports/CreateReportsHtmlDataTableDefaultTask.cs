using Cake.Frosting;
using Cake.Issues.Reporting;
using Cake.Issues.Reporting.Generic;

[TaskName("Create-Reports-HtmlDataTable-Default")]
[IsDependentOn(typeof(AnalyzeTask))]
public class CreateReportsHtmlDataTableDefaultTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.CreateIssueReport(
            context.Issues,
            context.GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDataTable),
            context.RepoRootFolder,
            context.TemplateGalleryFolder.CombineWithFilePath("htmldatatable-demo-default.html"));

    }
}