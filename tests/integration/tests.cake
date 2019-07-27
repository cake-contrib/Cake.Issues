#addin nuget:?package=Cake.Issues
#addin nuget:?package=Cake.Issues.Reporting
#addin nuget:?package=Cake.Issues.Reporting.Generic
#reference "../../tools/Addins/Cake.Issues.GitRepository/lib/netstandard2.0/Cake.Issues.GitRepository.dll"

//////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////

var target = Argument<string>("target", "Run-All-Tests");

//////////////////////////////////////////////////
// SETUP / TEARDOWN
//////////////////////////////////////////////////

Setup(ctx =>
{
});

Teardown(ctx =>
{
});

//////////////////////////////////////////////////
// TARGETS
//////////////////////////////////////////////////

Task("CheckBinaryFilesTrackedByLfs")
    .Does(() =>
{
    var repoRootDir = MakeAbsolute(Directory("../../"));

    var settings =
        new GitRepositoryIssuesSettings
        {
            CheckBinaryFilesTrackedByLfs = true
        };

    var issues =
        ReadIssues(
            GitRepositoryIssuesAliases.GitRepositoryIssues(Context, settings),
            repoRootDir);

    var reportDir =
        repoRootDir.Combine("BuildArtifacts").Combine("TestResults").Combine("Integration");
    var reportFilePath =
        reportDir.CombineWithFilePath("issues-report.html");
    EnsureDirectoryExists(reportDir);

    CreateIssueReport(
        issues,
        GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic),
        repoRootDir,
        reportFilePath);
});

Task("Run-All-Tests")
    .IsDependentOn("CheckBinaryFilesTrackedByLfs");

//////////////////////////////////////////////////

RunTarget(target);