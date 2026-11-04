namespace Cake.Issues.Tap.Tests;

internal class TapIssuesProviderFixture<T>
    : BaseMultiFormatIssueProviderFixture<TapIssuesProvider, TapIssuesSettings, T>
    where T : BaseTapLogFileFormat
{
    public TapIssuesProviderFixture(string fileResourceName)
        : this(fileResourceName, @"c:\Source\Cake.Issues")
    {
    }

    public TapIssuesProviderFixture(string fileResourceName, string repositoryRoot)
        : base(fileResourceName)
    {
        this.ReadIssuesSettings =
            new ReadIssuesSettings(repositoryRoot);
    }

    protected override string FileResourceNamespace => "Cake.Issues.Tap.Tests.Testfiles." + typeof(T).Name + ".";
}
