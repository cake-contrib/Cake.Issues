Task("Create-Reports-HtmlDxDataGrid-Additional-Columns")
    .Description("Creates HtmlDxDataGrid demo report showing how to add additional columns to a report")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(
                    HtmlDxDataGridOption.AdditionalColumns,
                    new List<HtmlDxDataGridColumnDescription>
                    {
                        new HtmlDxDataGridColumnDescription(
                            "IsSrcFolder",
                            issue => 
                            { 
                                return issue.AffectedFileRelativePath?.FullPath.StartsWith("src/"); 
                            })
                        {
                            Caption = "Source Folder",
                        }
                     })),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-additionalcolumns.html"));
});