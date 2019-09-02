#addin "Cake.Markdownlint"

Task("Lint-Documentation")
    .Description("Runs Markdownint on documentation")
    .Does<BuildData>(data =>
{
    var settings =
        MarkdownlintNodeJsRunnerSettings.ForDirectory(data.DocsFolder);
    settings.OutputFile = data.MarkdownLintLogFilePath;
    settings.ThrowOnIssue = false;
    RunMarkdownlintNodeJs(settings);
});