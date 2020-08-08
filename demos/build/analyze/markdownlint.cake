#addin "Cake.Markdownlint"

Task("Lint-Documentation")
    .IsDependentOn("Lint-DemoDocumentation")
    .IsDependentOn("Lint-TemplateGalleryDocumentation");

Task("Lint-DemoDocumentation")
    .Description("Runs Markdownint on demo documentation")
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
        Run = "Demos documentation"
    };

    data.Issues.AddRange(
        ReadIssues(
            MarkdownlintIssuesFromFilePath(
                markdownLintLogFilePath,
                MarkdownlintCliLogFileFormat),
            readIssuesSettings));
});

Task("Lint-TemplateGalleryDocumentation")
    .Description("Runs Markdownint on template gallery documentation")
    .Does<BuildData>(data =>
{
    var markdownLintLogFilePath = data.RepoRootFolder.CombineWithFilePath("markdown.log");

    // Run markdownlint
    var settings =
        MarkdownlintNodeJsRunnerSettings.ForDirectory(data.TemplateGalleryFolder);
    settings.OutputFile = markdownLintLogFilePath;
    settings.ThrowOnIssue = false;
    RunMarkdownlintNodeJs(settings);

    // Read issues
    var readIssuesSettings = new ReadIssuesSettings(data.RepoRootFolder)
    {
        Run = "Template gallery documentation"
    };

    data.Issues.AddRange(
        ReadIssues(
            MarkdownlintIssuesFromFilePath(
                markdownLintLogFilePath,
                MarkdownlintCliLogFileFormat),
            readIssuesSettings));
});