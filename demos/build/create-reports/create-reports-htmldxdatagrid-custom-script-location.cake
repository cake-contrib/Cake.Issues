Task("Create-Reports-HtmlDxDataGrid-Custom-Script-Location")
    .Description("Creates HtmlDxDataGrid demo report showing how to use custom script location and version")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.JQueryLocation, "https://ajax.aspnetcdn.com/ajax/jquery/")
                .WithOption(HtmlDxDataGridOption.JQueryVersion, "3.1.0")
                .WithOption(HtmlDxDataGridOption.DevExtremeLocation, "https://cdn3.devexpress.com/jslib/")
                .WithOption(HtmlDxDataGridOption.DevExtremeVersion, "18.2.3")),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-customscriptlocation.html"));
});