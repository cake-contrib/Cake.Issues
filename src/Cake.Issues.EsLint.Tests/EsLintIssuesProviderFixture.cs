namespace Cake.Issues.EsLint.Tests
{
    using Cake.Issues.Testing;

    public class EsLintIssuesProviderFixture<T>
        : BaseMultiFormatIssueProviderFixture<EsLintIssuesProvider, EsLintIssuesSettings, T>
        where T : BaseEsLintLogFileFormat
    {
        public EsLintIssuesProviderFixture(string fileResourceName)
            : base(fileResourceName)
        {
            this.ReadIssuesSettings =
                new ReadIssuesSettings(@"c:\Source\Cake.Issues");
        }

        protected override string FileResourceNamespace => "Cake.Issues.EsLint.Tests.Testfiles." + typeof(T).Name + ".";
    }
}
