namespace Cake.Issues.Tests.Testing
{
    using Cake.Issues.Testing;

    internal sealed class FakeConfigurableIssueProviderFixture(string fileResourceName)
                : BaseConfigurableIssueProviderFixture<FakeConfigurableIssueProvider, IssueProviderSettings>(fileResourceName)
    {
        protected override string FileResourceNamespace => "Cake.Issues.Tests.Testfiles.";
    }
}
