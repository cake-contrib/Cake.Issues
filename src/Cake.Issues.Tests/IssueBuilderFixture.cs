namespace Cake.Issues.Tests
{
    internal class IssueBuilderFixture
    {
        public IssueBuilderFixture()
            : this("Identifier", "Message", "ProviderType", "ProviderName")
        {
        }

        public IssueBuilderFixture(string identifier, string messageText, string providerType, string providerName)
        {
            this.IssueBuilder =
                IssueBuilder.NewIssue(identifier, messageText, providerType, providerName);
        }

        public IssueBuilder IssueBuilder { get; }
    }
}
