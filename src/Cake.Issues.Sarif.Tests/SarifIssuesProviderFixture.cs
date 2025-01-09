namespace Cake.Issues.Sarif.Tests;

internal class SarifIssuesProviderFixture(string fileResourceName)
    : BaseConfigurableIssueProviderFixture<SarifIssuesProvider, SarifIssuesSettings>(fileResourceName)
{
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
