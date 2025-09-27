namespace Cake.Issues.JUnit.Tests;

using Cake.Issues.Testing;

internal class JUnitIssuesProviderFixture<T>
    : BaseMultiFormatIssueProviderFixture<JUnitIssuesProvider, JUnitIssuesSettings, T>
    where T : BaseJUnitLogFileFormat
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