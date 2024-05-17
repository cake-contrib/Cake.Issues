using Cake.Frosting;

[TaskName("Create-Reports-HtmlDxDataGrid-Export-Format-Pdf")]
[IsDependentOn(typeof(AnalyzeTask))]
public class CreateReportsHtmlDxDataGridExportFormatPdfTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.CreateIssueReport(
            context.Issues,
            context.GenericIssueReportFormatFromEmbeddedTemplate(
                GenericIssueReportTemplate.HtmlDxDataGrid,
                settings => 
                    settings
                        .WithOption(HtmlDxDataGridOption.EnableExporting, true)
                        .WithOption(HtmlDxDataGridOption.ExportFormat, HtmlDxDataGridExportFormat.Pdf)),
            context.RepoRootFolder,
            context.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-exportformat-pdf.html"));

    }
}