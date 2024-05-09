namespace Cake.Issues.InspectCode.Tests
{
    internal class InspectCodeIssuesProviderFixture(string fileResourceName)
        : BaseConfigurableIssueProviderFixture<InspectCodeIssuesProvider, InspectCodeIssuesSettings>(fileResourceName)
    {
        protected override string FileResourceNamespace => "Cake.Issues.InspectCode.Tests.Testfiles.";
    }
}
