namespace Cake.Issues.MsBuild.Tests;

using Cake.Core.Diagnostics;

internal class FakeMsBuildLogFileFormat(ICakeLog log) : BaseMsBuildLogFileFormat(log)
{
    public override IEnumerable<IIssue> ReadIssues(
        MsBuildIssuesProvider issueProvider,
        IRepositorySettings repositorySettings,
        MsBuildIssuesSettings issueProviderSettings) => throw new NotImplementedException();
}
