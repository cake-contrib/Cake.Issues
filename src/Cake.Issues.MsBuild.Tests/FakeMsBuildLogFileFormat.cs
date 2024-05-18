namespace Cake.Issues.MsBuild.Tests
{
    using Cake.Core.Diagnostics;

    internal class FakeMsBuildLogFileFormat(ICakeLog log) : BaseMsBuildLogFileFormat(log)
    {
        public new (bool Valid, string FilePath) ValidateFilePath(string filePath, IRepositorySettings repositorySettings) =>
            base.ValidateFilePath(filePath, repositorySettings);

        public override IEnumerable<IIssue> ReadIssues(
            MsBuildIssuesProvider issueProvider,
            IRepositorySettings repositorySettings,
            MsBuildIssuesSettings issueProviderSettings) => throw new NotImplementedException();
    }
}
