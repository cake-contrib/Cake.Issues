namespace Cake.Issues.Tests;

internal class IssueBuilderFixture(string identifier, string messageText, string providerType, string providerName)
{
    public IssueBuilderFixture()
        : this("Identifier", "Message", "ProviderType", "ProviderName")
    {
    }

    public IssueBuilder IssueBuilder { get; } =
            IssueBuilder.NewIssue(identifier, messageText, providerType, providerName);
}
