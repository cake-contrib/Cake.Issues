namespace Cake.Issues.Tests.Testing
{
    internal sealed class FakeMultiFormatIssueProviderFixture(string fileResourceName)
                : BaseMultiFormatIssueProviderFixture<FakeMultiFormatIssueProvider, FakeMultiFormatIssueProviderSettings, FakeLogFileFormat>(fileResourceName)
    {
        protected override string FileResourceNamespace => "Cake.Issues.Tests.Testfiles.";
    }
}
