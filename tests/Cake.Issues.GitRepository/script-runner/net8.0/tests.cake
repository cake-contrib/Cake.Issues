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

Task("CheckBinaryFilesTrackedByLfs-Should-Return-Files-Not-Tracked-By-Lfs")
    .Does(() =>
{
    var repoRootDir = MakeAbsolute(Directory("./"));

    var issues =
        ReadIssues(
            GitRepositoryIssuesAliases.GitRepositoryIssues(
                Context,
                new GitRepositoryIssuesSettings
                {
                    CheckBinaryFilesTrackedByLfs = true
                }),
            new ReadIssuesSettings(repoRootDir));

    if (issues.Count() != 2)
    {
        throw new Exception($"Expected 2 issues, but found {issues.Count()}");
    }

    if (!issues.Any(i => i.AffectedFileRelativePath.GetFilename().ToString().Equals("cake-contrib-small.png", StringComparison.InvariantCultureIgnoreCase)))
    {
        throw new Exception("Expected issue for 'cake-contrib-small.png' not found.");
    }

    if (!issues.Any(i => i.AffectedFileRelativePath.GetFilename().ToString().Equals("file-with-umlaut-ä.png", StringComparison.InvariantCultureIgnoreCase)))
    {
        throw new Exception("Expected issue for 'file-with-umlaut-ä.png' not found.");
    }

    Information("Successful");
});

Task("CheckBinaryFilesTrackedByLfs-Should-Ignore-Empty-Files")
    .Does(() =>
{
    var repoRootDir = MakeAbsolute(Directory("./"));

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

    Information("Successful");
});

Task("CheckBinaryFilesTrackedByLfs-Should-Ignore-FilesToExclude")
    .Does(() =>
{
    var repoRootDir = MakeAbsolute(Directory("./"));

    var settings =
        new GitRepositoryIssuesSettings
        {
            CheckBinaryFilesTrackedByLfs = true,
        };
    settings.FilesToExclude.Add("**/cake-contrib-small.png");

    var issues =
        ReadIssues(
            GitRepositoryIssuesAliases.GitRepositoryIssues(Context, settings),
            new ReadIssuesSettings(repoRootDir));

    if (issues.Count() != 1)
    {
        throw new Exception($"Expected 1 issues, but found {issues.Count()}");
    }

    if (!issues.Any(i => i.AffectedFileRelativePath.GetFilename().ToString().Equals("file-with-umlaut-ä.png", StringComparison.InvariantCultureIgnoreCase)))
    {
        throw new Exception("Expected issue for 'file-with-umlaut-ä.png' not found.");
    }

    Information("Successful");
});

Task("CheckBinaryFilesTrackedByLfs-Should-Ignore-FilesToExcludeFromBinaryFilesLfsCheck")
    .Does(() =>
{
    var repoRootDir = MakeAbsolute(Directory("./"));

    var settings =
        new GitRepositoryIssuesSettings
        {
            CheckBinaryFilesTrackedByLfs = true,
        };
    settings.FilesToExcludeFromBinaryFilesLfsCheck.Add("**/cake-contrib-small.png");

    var issues =
        ReadIssues(
            GitRepositoryIssuesAliases.GitRepositoryIssues(Context, settings),
            new ReadIssuesSettings(repoRootDir));

    if (issues.Count() != 1)
    {
        throw new Exception($"Expected 1 issues, but found {issues.Count()}");
    }

    if (!issues.Any(i => i.AffectedFileRelativePath.GetFilename().ToString().Equals("file-with-umlaut-ä.png", StringComparison.InvariantCultureIgnoreCase)))
    {
        throw new Exception("Expected issue for 'file-with-umlaut-ä.png' not found.");
    }

    Information("Successful");
});

Task("CheckFilesPathLength-Should-Return-Issues-For-Files-Exceeding-Maximum-Length")
    .Does(() =>
{
    var repoRootDir = MakeAbsolute(Directory("./"));

    var issues =
        ReadIssues(
            GitRepositoryIssuesAliases.GitRepositoryIssues(
                Context,
                new GitRepositoryIssuesSettings
                {
                    CheckFilesPathLength = true,
                    MaxFilePathLength = 30
                }),
            new ReadIssuesSettings(repoRootDir));

    if (issues.Count() != 2)
    {
        throw new Exception($"Expected 2 issues, but found {issues.Count()}");
    }

    if (!issues.Any(i => i.AffectedFileRelativePath.GetFilename().ToString().Equals("cake-contrib-small.png", StringComparison.InvariantCultureIgnoreCase)))
    {
        throw new Exception("Expected issue for 'cake-contrib-small.png' not found.");
    }

    if (!issues.Any(i => i.AffectedFileRelativePath.GetFilename().ToString().Equals("file-with-umlaut-ä.png", StringComparison.InvariantCultureIgnoreCase)))
    {
        throw new Exception("Expected issue for 'file-with-umlaut-ä.png' not found.");
    }

    Information("Successful");
});

Task("CheckFilesPathLength-Should-Ignore-FilesToExclude")
    .Does(() =>
{
    var repoRootDir = MakeAbsolute(Directory("./"));

    var settings =
        new GitRepositoryIssuesSettings
        {
            CheckFilesPathLength = true,
            MaxFilePathLength = 30
        };
    settings.FilesToExclude.Add("**/cake-contrib-small.png");

    var issues =
        ReadIssues(
            GitRepositoryIssuesAliases.GitRepositoryIssues(Context,settings),
            new ReadIssuesSettings(repoRootDir));

    if (issues.Count() != 1)
    {
        throw new Exception($"Expected 1 issues, but found {issues.Count()}");
    }
    if (!issues.Any(i => i.AffectedFileRelativePath.GetFilename().ToString().Equals("file-with-umlaut-ä.png", StringComparison.InvariantCultureIgnoreCase)))
    {
        throw new Exception("Expected issue for 'file-with-umlaut-ä.png' not found.");
    }

    Information("Successful");
});

Task("CheckFilesPathLength-Should-Ignore-FilesToExcludeFromFilePathLengthCheck")
    .Does(() =>
{
    var repoRootDir = MakeAbsolute(Directory("./"));

    var settings =
        new GitRepositoryIssuesSettings
        {
            CheckFilesPathLength = true,
            MaxFilePathLength = 30
        };
    settings.FilesToExcludeFromFilePathLengthCheck.Add("**/cake-contrib-small.png");

    var issues =
        ReadIssues(
            GitRepositoryIssuesAliases.GitRepositoryIssues(Context,settings),
            new ReadIssuesSettings(repoRootDir));

    if (issues.Count() != 1)
    {
        throw new Exception($"Expected 1 issues, but found {issues.Count()}");
    }
    if (!issues.Any(i => i.AffectedFileRelativePath.GetFilename().ToString().Equals("file-with-umlaut-ä.png", StringComparison.InvariantCultureIgnoreCase)))
    {
        throw new Exception("Expected issue for 'file-with-umlaut-ä.png' not found.");
    }

    Information("Successful");
});

Task("Run-All-Tests")
    .IsDependentOn("CheckBinaryFilesTrackedByLfs-Should-Return-Files-Not-Tracked-By-Lfs")
    .IsDependentOn("CheckBinaryFilesTrackedByLfs-Should-Ignore-Empty-Files")
    .IsDependentOn("CheckBinaryFilesTrackedByLfs-Should-Ignore-FilesToExclude")
    .IsDependentOn("CheckBinaryFilesTrackedByLfs-Should-Ignore-FilesToExcludeFromBinaryFilesLfsCheck")
    .IsDependentOn("CheckFilesPathLength-Should-Return-Issues-For-Files-Exceeding-Maximum-Length")
    .IsDependentOn("CheckFilesPathLength-Should-Ignore-FilesToExclude")
    .IsDependentOn("CheckFilesPathLength-Should-Ignore-FilesToExcludeFromFilePathLengthCheck");

//////////////////////////////////////////////////

RunTarget(target);