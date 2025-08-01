namespace Cake.Issues.JUnit.Tests;

using System.Text;

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

    /// <summary>
    /// Sets the content of the log file.
    /// </summary>
    /// <param name="content">Content to set.</param>
    public void SetFileContent(string content)
    {
        content.NotNullOrWhiteSpace();
        this.LogFileContent = Encoding.UTF8.GetBytes(content);
    }
}