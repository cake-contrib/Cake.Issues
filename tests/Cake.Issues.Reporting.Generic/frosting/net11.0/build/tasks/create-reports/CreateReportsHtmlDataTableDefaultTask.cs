using Cake.Frosting;

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