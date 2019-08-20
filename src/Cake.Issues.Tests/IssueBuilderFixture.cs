namespace Cake.Issues.Tests
{
    internal class IssueBuilderFixture
    {
        public IssueBuilderFixture()
            : this("Message", "ProviderType", "ProviderName")
        {
        }

        public IssueBuilderFixture(string messageText, string providerType, string providerName)
        {
            this.IssueBuilder =
                IssueBuilder.NewIssue(messageText, providerType, providerName);
        }

        public IssueBuilder IssueBuilder { get; private set; }
    }
}
