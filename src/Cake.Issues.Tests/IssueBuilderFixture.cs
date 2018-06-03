namespace Cake.Issues.Tests
{
    public class IssueBuilderFixture
    {
        public IssueBuilderFixture()
        {
            this.IssueBuilder = IssueBuilder.NewIssue("Message", "ProviderType", "ProviderName");
        }

        public IssueBuilder IssueBuilder { get; private set; }
    }
}
