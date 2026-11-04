namespace Cake.Issues.Sarif.Tests;

internal class SarifIssuesProviderFixture
    : BaseConfigurableIssueProviderFixture<SarifIssuesProvider, SarifIssuesSettings>
{
    public SarifIssuesProviderFixture(string fileResourceName)
        : this(fileResourceName, @"c:\Source\Cake.Issues")
    {
    }

    public SarifIssuesProviderFixture(string fileResourceName, string repositoryRoot)
        : base(fileResourceName)
    {
        this.ReadIssuesSettings =
            new ReadIssuesSettings(repositoryRoot);
    }

    public bool UseToolNameAsIssueProviderName { get; set; } = true;

    public bool IgnoreSuppressedIssues { get; set; } = true;

    protected override string FileResourceNamespace => "Cake.Issues.Sarif.Tests.Testfiles.";

    protected override SarifIssuesSettings CreateIssueProviderSettings()
    {
        var settings = base.CreateIssueProviderSettings();
        settings.UseToolNameAsIssueProviderName = this.UseToolNameAsIssueProviderName;
        settings.IgnoreSuppressedIssues = this.IgnoreSuppressedIssues;
        return settings;
    }

}
