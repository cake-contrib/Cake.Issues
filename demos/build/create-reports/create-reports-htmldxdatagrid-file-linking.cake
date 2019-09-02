Task("Create-Reports-HtmlDxDataGrid-File-Linking")
    .Description("Creates HtmlDxDataGrid demo report showing how to have issues linked to file")
    .IsDependentOn("Read-Issues")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDxDataGrid,
            settings => settings
                .WithOption(
                    HtmlDxDataGridOption.FileLinkSettings,
                    GenericIssueReportFileLinkSettingsForGitHub(
                        new System.Uri("https://github.com/cake-contrib/Cake.Issues.Website"),
                        "develop",
                        "demos"))),
        data.RepoRootFolder,
        data.TemplateGalleryFolder.CombineWithFilePath("htmldxdatagrid-demo-filelinking.html"));
});