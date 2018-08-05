namespace Cake.Issues.MsBuild.Tests
{
    using Cake.Issues.Testing;

    internal class MsBuildIssuesProviderFixture
        : BaseMultiFormatIssueProviderFixture<MsBuildIssuesProvider, MsBuildIssuesSettings, XmlFileLoggerFormat>
    {
        public MsBuildIssuesProviderFixture(string fileResourceName)
            : base(fileResourceName)
        {
            this.RepositorySettings = new RepositorySettings(@"c:\Source\Cake.Issues.MsBuild");
        }

        protected override string FileResourceNamespace => "Cake.Issues.MsBuild.Tests.Testfiles.";
    }
}
