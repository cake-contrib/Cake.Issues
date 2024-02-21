Task("Analyze")
    .IsDependentOn("Lint-Documentation");

Task("Lint-Documentation")
    .Description("Runs Markdownint on test documentation")
    .Does<BuildData>(data =>
{
    var markdownLintLogFilePath = data.OutputFolder.CombineWithFilePath("markdownlint-tests.log");

    // Run markdownlint
    var settings =
        MarkdownlintNodeJsRunnerSettings.ForDirectory(data.DocsFolder);
    settings.OutputFile = markdownLintLogFilePath;
    settings.ThrowOnIssue = false;
    RunMarkdownlintNodeJs(settings);

    // Read issues
    var readIssuesSettings = new ReadIssuesSettings(data.RepoRootFolder)
    {
        Run = "Test files",
        FileLinkSettings =
            IssueFileLinkSettingsForGitHubBranch(
                new System.Uri("https://github.com/cake-contrib/Cake.Issues.Reporting.Sarif"),
                "develop",
                "tests"
            )
    };

    data.Issues.AddRange(
        ReadIssues(
            MarkdownlintIssuesFromFilePath(
                markdownLintLogFilePath,
                MarkdownlintCliLogFileFormat),
            readIssuesSettings));
});