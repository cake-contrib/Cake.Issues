#addin "Cake.Issues&prerelease"
#addin "Cake.Issues.CodeClimate&prerelease"
#addin "Cake.Issues.Reporting&prerelease"
#addin "Cake.Issues.Reporting.Generic&prerelease"

var target = Argument("target", "Default");

public class BuildData
{
    public DirectoryPath RepoRootFolder { get; }
    public DirectoryPath TestRootFolder { get; }
    public DirectoryPath OutputFolder { get; }
    public List<IIssue> Issues { get; }

    public BuildData(ICakeContext context)
    {
        this.TestRootFolder = context.MakeAbsolute(context.Directory("./"));
        this.RepoRootFolder = this.TestRootFolder.Combine("..").Combine("..");
        this.OutputFolder = this.TestRootFolder.Combine("output");

        this.Issues = new List<IIssue>();
    }
}

Setup<BuildData>(setupContext =>
{
    return new BuildData(setupContext);
});

Task("Analyze")
    .Description("Reads CodeClimate issues from test data")
    .Does<BuildData>(data =>
{
    var codeClimateLogFilePath = data.TestRootFolder.CombineWithFilePath("codeclimate-test.json");

    // Read issues
    var readIssuesSettings = new ReadIssuesSettings(data.RepoRootFolder)
    {
        Run = "Test files",
        FileLinkSettings =
            IssueFileLinkSettingsForGitHubBranch(
                new System.Uri("https://github.com/cake-contrib/Cake.Issues"),
                "develop",
                "tests"
            )
    };

    data.Issues.AddRange(
        ReadIssues(
            CodeClimateIssuesFromFilePath(codeClimateLogFilePath),
            readIssuesSettings));

    Information($"Found {data.Issues.Count} CodeClimate issues");
});

Task("Create-Reports")
    .Description("Creates a console report")
    .IsDependentOn("Analyze")
    .Does<BuildData>(data =>
{
    CreateIssueReport(
        data.Issues,
        GenericIssueReportFormatFromEmbeddedTemplate(
            GenericIssueReportTemplate.HtmlDiagnosticSingleFileText),
        data.RepoRootFolder,
        data.OutputFolder.CombineWithFilePath("report.html"));

    Information($"Created report with {data.Issues.Count} issues");
});

Task("Default")
    .IsDependentOn("Create-Reports");

RunTarget(target);