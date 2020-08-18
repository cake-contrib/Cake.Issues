Task("Lint-Documentation")
    .IsDependentOn("Lint-TestDocumentation")
    .IsDependentOn("Lint-AddinDocumentation");

Task("Lint-TestDocumentation")
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
        Run = "Demos documentation",
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

Task("Lint-AddinDocumentation")
    .Description("Runs Markdownint on addin documentation")
    .Does<BuildData>(data =>
{
    var markdownLintLogFilePath = data.OutputFolder.CombineWithFilePath("markdownlint-addin.log");

    // Run markdownlint
    var settings =
        MarkdownlintNodeJsRunnerSettings.ForDirectory(data.RepoRootFolder.Combine("../docs"));
    settings.OutputFile = markdownLintLogFilePath;
    settings.ThrowOnIssue = false;
    RunMarkdownlintNodeJs(settings);

    // Read issues
    var readIssuesSettings = new ReadIssuesSettings(data.RepoRootFolder)
    {
        Run = "Addin documentation",
        FileLinkSettings =
            IssueFileLinkSettingsForGitHubBranch(
                new System.Uri("https://github.com/cake-contrib/Cake.Issues.Reporting.Sarif"),
                "develop",
                "docs"
            )
    };

    data.Issues.AddRange(
        ReadIssues(
            MarkdownlintIssuesFromFilePath(
                markdownLintLogFilePath,
                MarkdownlintCliLogFileFormat),
            readIssuesSettings));
});