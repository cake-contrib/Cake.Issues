Task("Create-Reports-HtmlDxDataGrid-InfiniteScrolling")
    .Description("Creates HtmlDxDataGrid demo report showing how to enable infinite scrolling")
    .IsDependentOn("Analyze")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(HtmlDxDataGridOption.DisplayMode, HtmlDxDataGridDisplayMode.InfiniteScroll)),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-infinitescrolling.html"));
});