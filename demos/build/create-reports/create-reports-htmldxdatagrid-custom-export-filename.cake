Task("Create-Reports-HtmlDxDataGrid-Custom-Export-Filename")
    .Description("Creates HtmlDxDataGrid demo report showing how to change default name of exported file")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.EnableExporting, true)
                .WithOption(HtmlDxDataGridOption.ExportFileName, "custom-filename")),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-customexportfilename.html"));
});