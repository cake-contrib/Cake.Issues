namespace Cake.Issues.Sarif.Tests;

internal class SarifIssuesProviderFixture(string fileResourceName)
    : BaseConfigurableIssueProviderFixture<SarifIssuesProvider, SarifIssuesSettings>(fileResourceName)
{
    protected override string FileResourceNamespace => "Cake.Issues.Sarif.Tests.Testfiles.";
}
