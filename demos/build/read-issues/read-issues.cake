Task("Read-Issues")
    .Description("Reads code analyzer issues")
    .IsDependentOn("Analyze")
    .Does<BuildData>(data =>
{
    var issueProvider =
        new List<IIssueProvider>
        {
            MsBuildIssuesFromFilePath(
                data.MsBuildLogFilePath,
                MsBuildXmlFileLoggerFormat),
            MarkdownlintIssuesFromFilePath(
                data.MarkdownLintLogFilePath,
                MarkdownlintCliLogFileFormat)
        };

    if (IsRunningOnWindows())
    {
        issueProvider.Add(
            InspectCodeIssuesFromFilePath(
                data.InspectCodeLogFilePath)
        );
    }

    data.Issues.AddRange(ReadIssues(
        issueProvider,
        data.RepoRootFolder));

    Information("{0} issues are found.", data.Issues.Count());
});