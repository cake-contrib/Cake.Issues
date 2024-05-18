Task("Lint-Documentation")
    .Description("Runs Markdownlint on demo documentation")
    .Does<BuildData>(data =>
{
    var markdownLintLogFilePath = data.RepoRootFolder.CombineWithFilePath("markdown.log");

    // Run markdownlint
    var settings =
        MarkdownlintNodeJsRunnerSettings.ForDirectory(data.DocsFolder);
    settings.OutputFile = markdownLintLogFilePath;
    settings.ThrowOnIssue = false;
    RunMarkdownlintNodeJs(settings);

    // Read issues
    var readIssuesSettings = new ReadIssuesSettings(data.RepoRootFolder)
    {
        Run = "Demos documentation",
        FileLinkSettings =
            IssueFileLinkSettingsForGitHubBranch(
                new System.Uri("https://github.com/cake-contrib/Cake.Issues"),
                "develop",
                "tests/TestDataGenerator"
            )
    };

    data.Issues.AddRange(
        ReadIssues(
            MarkdownlintIssuesFromFilePath(
                markdownLintLogFilePath,
                MarkdownlintCliLogFileFormat),
            readIssuesSettings));
});