namespace Cake.Issues.Sarif.Tests
{
    using Cake.Issues.Testing;

    internal class SarifIssuesProviderFixture : BaseConfigurableIssueProviderFixture<SarifIssuesProvider, SarifIssuesSettings>
    {
        public SarifIssuesProviderFixture(string fileResourceName)
            : base(fileResourceName)
        {
        }

        protected override string FileResourceNamespace => "Cake.Issues.Sarif.Tests.Testfiles.";
    }
}
