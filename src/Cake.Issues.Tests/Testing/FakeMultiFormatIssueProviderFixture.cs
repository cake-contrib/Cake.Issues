namespace Cake.Issues.Tests.Testing
{
    using Cake.Issues.Testing;

    internal sealed class FakeMultiFormatIssueProviderFixture(string fileResourceName)
                : BaseMultiFormatIssueProviderFixture<FakeMultiFormatIssueProvider, FakeMultiFormatIssueProviderSettings, FakeLogFileFormat>(fileResourceName)
    {
        protected override string FileResourceNamespace => "Cake.Issues.Tests.Testfiles.";
    }
}
