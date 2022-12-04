using Cake.Frosting;
using Cake.Issues.Reporting;
using Cake.Issues.Reporting.Generic;

[TaskName("Create-Reports-HtmlDxDataGrid-Custom-Script-Location")]
[IsDependentOn(typeof(AnalyzeTask))]
public class CreateReportsHtmlDxDataGridCustomScriptLocationTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.CreateIssueReport(
            context.Issues,
            context.GenericIssueReportFormatFromEmbeddedTemplate(
                GenericIssueReportTemplate.HtmlDxDataGrid,
                settings => 
                    settings
                        .WithOption(HtmlDxDataGridOption.JQueryLocation, "https://ajax.aspnetcdn.com/ajax/jquery/")
                        .WithOption(HtmlDxDataGridOption.JQueryVersion, "3.1.0")
                        .WithOption(HtmlDxDataGridOption.DevExtremeLocation, "https://cdn3.devexpress.com/jslib/")
                        .WithOption(HtmlDxDataGridOption.DevExtremeVersion, "18.2.3")),
            context.RepoRootFolder,
            context.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-customscriptlocation.html"));

    }
}