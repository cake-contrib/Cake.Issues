Task("Create-Reports-HtmlDxDataGrid-ExportFormat-Pdf")
    .Description("Creates HtmlDxDataGrid demo report showing how to enable PDF export")
    .IsDependentOn("Analyze")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.EnableExporting, true)
                .WithOption(HtmlDxDataGridOption.ExportFormat, HtmlDxDataGridExportFormat.Pdf)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-exportformat-pdf.html"));
});