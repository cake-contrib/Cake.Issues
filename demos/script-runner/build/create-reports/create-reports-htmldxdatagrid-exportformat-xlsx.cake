Task("Create-Reports-HtmlDxDataGrid-ExportFormat-Xlsx")
    .Description("Creates HtmlDxDataGrid demo report showing how to enable Excel export")
    .IsDependentOn("Analyze")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.EnableExporting, true)
                .WithOption(HtmlDxDataGridOption.ExportFormat, HtmlDxDataGridExportFormat.Excel)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-exportformat-xlsx.html"));
});