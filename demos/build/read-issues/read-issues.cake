Task("Read-Issues")
    .Description("Reads code analyzer issues")
    .IsDependentOn("Analyze")
    .Does<BuildData>(data =>
{
    var settings =
        new ReadIssuesSettings(data.RepoRootFolder)
        {
            Format = IssueCommentFormat.Html
        };

    data.Issues.AddRange(ReadIssues(
        new List<IIssueProvider>
        {
            MsBuildIssuesFromFilePath(
                data.MsBuildLogFilePath,
                MsBuildXmlFileLoggerFormat),
            InspectCodeIssuesFromFilePath(
                data.InspectCodeLogFilePath),
            MarkdownlintCliIssuesFromFilePath(
                data.MarkdownLintLogFilePath)
        },
        settings));

    Information("{0} issues are found.", data.Issues.Count());
});