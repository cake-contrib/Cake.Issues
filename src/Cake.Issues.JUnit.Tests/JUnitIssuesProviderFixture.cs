namespace Cake.Issues.JUnit.Tests;

internal class JUnitIssuesProviderFixture
    : BaseConfigurableIssueProviderFixture<JUnitIssuesProvider, JUnitIssuesSettings>
{
    public JUnitIssuesProviderFixture(string fileResourceName)
        : this(fileResourceName, @"c:\Source\Cake.Issues")
    {
    }

    public JUnitIssuesProviderFixture(string fileResourceName, string repositoryRoot)
        : base(fileResourceName)
    {
        this.ReadIssuesSettings =
            new ReadIssuesSettings(repositoryRoot);
    }

    protected override string FileResourceNamespace => "Cake.Issues.JUnit.Tests.Testfiles.";
}