Task("Create-Reports-HtmlDxDataGrid-Disable-Column-Chooser")
    .Description("Creates HtmlDxDataGrid demo report showing how to disable the column chooser")
    .IsDependentOn("Analyze")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.ShowColumnChooser, false)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-disablecolumnchooser.html"));
});