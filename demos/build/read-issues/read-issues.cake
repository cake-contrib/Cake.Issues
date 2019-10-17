Task("Read-Issues")
    .Description("Reads code analyzer issues")
    .IsDependentOn("Analyze")
    .Does<BuildData>(data =>
{
    data.Issues.AddRange(ReadIssues(
        new List<IIssueProvider>
        {
            MsBuildIssuesFromFilePath(
                data.MsBuildLogFilePath,
                MsBuildXmlFileLoggerFormat),
            InspectCodeIssuesFromFilePath(
                data.InspectCodeLogFilePath),
            MarkdownlintIssuesFromFilePath(
                data.MarkdownLintLogFilePath,
                MarkdownlintCliLogFileFormat)
        },
        data.RepoRootFolder));

    Information("{0} issues are found.", data.Issues.Count());
});