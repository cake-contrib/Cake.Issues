namespace Cake.Issues.Tests.Testing
{
    using Cake.Issues.Testing;

    internal sealed class FakeConfigurableIssueProviderFixture
        : BaseConfigurableIssueProviderFixture<FakeConfigurableIssueProvider, IssueProviderSettings>
    {
        public FakeConfigurableIssueProviderFixture(string fileResourceName)
            : base(fileResourceName)
        {
        }

        protected override string FileResourceNamespace => "Cake.Issues.Tests.Testfiles.";
    }
}
