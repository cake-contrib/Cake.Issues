namespace Cake.Issues.Tests.Testing
{
    using Cake.Issues.Testing;

    internal sealed class FakeMultiFormatIssueProviderFixture
        : BaseMultiFormatIssueProviderFixture<FakeMultiFormatIssueProvider, FakeMultiFormatIssueProviderSettings, FakeLogFileFormat>
    {
        public FakeMultiFormatIssueProviderFixture(string fileResourceName)
            : base(fileResourceName)
        {
        }

        protected override string FileResourceNamespace => "Cake.Issues.Tests.Testfiles.";
    }
}
