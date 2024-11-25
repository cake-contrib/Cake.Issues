namespace Cake.Issues.Tap.Tests;

internal class TapIssuesProviderFixture<T>
    : BaseMultiFormatIssueProviderFixture<TapIssuesProvider, TapIssuesSettings, T>
    where T : BaseTapLogFileFormat
{
    public TapIssuesProviderFixture(string fileResourceName)
        : base(fileResourceName)
    {
        this.ReadIssuesSettings =
            new ReadIssuesSettings(@"c:\Source\Cake.Issues");
    }

    protected override string FileResourceNamespace => "Cake.Issues.Tap.Tests.Testfiles." + typeof(T).Name + ".";
}
