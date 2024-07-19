namespace Cake.Issues.Sarif.Tests;

internal class SarifIssuesProviderFixture(string fileResourceName)
    : BaseConfigurableIssueProviderFixture<SarifIssuesProvider, SarifIssuesSettings>(fileResourceName)
{
    public bool UseToolNameAsIssueProviderName { get; set; } = true;

    protected override string FileResourceNamespace => "Cake.Issues.Sarif.Tests.Testfiles.";

    protected override SarifIssuesSettings CreateIssueProviderSettings()
    {
        var settings = base.CreateIssueProviderSettings();
        settings.UseToolNameAsIssueProviderName = this.UseToolNameAsIssueProviderName;
        return settings;
    }

}
