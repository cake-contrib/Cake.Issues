#addin Cake.Issues&prerelease
#addin Cake.Issues.Reporting&prerelease
#addin Cake.Issues.Reporting.Generic&prerelease
#addin Cake.Issues.GitRepository&prerelease

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

    var issues =
        ReadIssues(
            GitRepositoryIssuesAliases.GitRepositoryIssues(
                Context,
                new GitRepositoryIssuesSettings
                {
                    CheckBinaryFilesTrackedByLfs = true
                }),
            new ReadIssuesSettings(repoRootDir));

    if (issues.Any(i => i.AffectedFileRelativePath.GetFilename().ToString().Equals("empty.png", StringComparison.InvariantCultureIgnoreCase)))
    {
        throw new Exception("The empty files should not be treated as binary");
    }

    var reportDir =
        repoRootDir.Combine("BuildArtifacts").Combine("TestResults").Combine("Integration");
    var reportFilePath =
        reportDir.CombineWithFilePath("CheckBinaryFilesTrackedByLfs.html");
    EnsureDirectoryExists(reportDir);

    CreateIssueReport(
        issues,
        GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic),
        repoRootDir,
        reportFilePath);
});

Task("CheckFilesPathLength")
    .Does(() =>
{
    var repoRootDir = MakeAbsolute(Directory("../../"));

    var issues =
        ReadIssues(
            GitRepositoryIssuesAliases.GitRepositoryIssues(
                Context,
                new GitRepositoryIssuesSettings
                {
                    CheckFilesPathLength = true,
                    MaxFilePathLength = 60
                }),
            new ReadIssuesSettings(repoRootDir));

    var reportDir =
        repoRootDir.Combine("BuildArtifacts").Combine("TestResults").Combine("Integration");
    var reportFilePath =
        reportDir.CombineWithFilePath("FilePathTooLong.html");
    EnsureDirectoryExists(reportDir);

    CreateIssueReport(
        issues,
        GenericIssueReportFormatFromEmbeddedTemplate(GenericIssueReportTemplate.HtmlDiagnostic),
        repoRootDir,
        reportFilePath);
});

Task("Run-All-Tests")
    .IsDependentOn("CheckBinaryFilesTrackedByLfs")
    .IsDependentOn("CheckFilesPathLength");

//////////////////////////////////////////////////

RunTarget(target);