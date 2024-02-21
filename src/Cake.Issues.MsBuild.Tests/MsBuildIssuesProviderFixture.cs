namespace Cake.Issues.MsBuild.Tests
{
    using Cake.Issues.Testing;

    internal class MsBuildIssuesProviderFixture<T>
        : BaseMultiFormatIssueProviderFixture<MsBuildIssuesProvider, MsBuildIssuesSettings, T>
        where T : BaseMsBuildLogFileFormat
    {
        public MsBuildIssuesProviderFixture(string fileResourceName)
            : base(fileResourceName)
        {
            this.ReadIssuesSettings = new ReadIssuesSettings(@"c:\Source\Cake.Issues.MsBuild");
        }

        protected override string FileResourceNamespace => "Cake.Issues.MsBuild.Tests.Testfiles." + typeof(T).Name + ".";
    }
}
