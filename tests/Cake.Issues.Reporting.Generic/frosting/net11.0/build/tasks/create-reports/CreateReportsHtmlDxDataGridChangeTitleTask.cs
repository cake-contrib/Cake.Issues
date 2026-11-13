using Cake.Frosting;

[TaskName("Create-Reports-HtmlDxDataGrid-Change-Title")]
[IsDependentOn(typeof(AnalyzeTask))]
public class CreateReportsHtmlDxDataGridChangeTitleTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.CreateIssueReport(
            context.Issues,
            context.GenericIssueReportFormatFromEmbeddedTemplate(
                GenericIssueReportTemplate.HtmlDxDataGrid,
                settings => 
                    settings
                        .WithOption(HtmlDxDataGridOption.Title, "My Custom Title")),
            context.RepoRootFolder,
            context.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-changetitle.html"));

    }
}