namespace Cake.Issues.Tests
{
    internal class IssueBuilderFixture
    {
        public IssueBuilderFixture()
            : this("Message", "ProviderType", "ProviderName")
        {
        }

        public IssueBuilderFixture(string message, string providerType, string providerName)
        {
            this.IssueBuilder =
                IssueBuilder.NewIssue(message, providerType, providerName);
        }

        public IssueBuilder IssueBuilder { get; private set; }
    }
}
