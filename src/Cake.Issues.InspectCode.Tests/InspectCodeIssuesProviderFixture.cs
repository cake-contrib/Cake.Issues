namespace Cake.Issues.InspectCode.Tests
{
    using Cake.Issues.Testing;

    internal class InspectCodeIssuesProviderFixture : BaseConfigurableIssueProviderFixture<InspectCodeIssuesProvider, InspectCodeIssuesSettings>
    {
        public InspectCodeIssuesProviderFixture(string fileResourceName)
            : base(fileResourceName)
        {
        }

        protected override string FileResourceNamespace => "Cake.Issues.InspectCode.Tests.Testfiles.";
    }
}
