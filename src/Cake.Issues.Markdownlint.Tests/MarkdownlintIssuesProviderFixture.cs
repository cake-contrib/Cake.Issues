namespace Cake.Issues.Markdownlint.Tests
{
    using Cake.Issues.Markdownlint;
    using Cake.Issues.Testing;

    internal class MarkdownlintIssuesProviderFixture<T>
        : BaseMultiFormatIssueProviderFixture<MarkdownlintIssuesProvider, MarkdownlintIssuesSettings, T>
        where T : BaseMarkdownlintLogFileFormat
    {
        public MarkdownlintIssuesProviderFixture(string fileResourceName)
            : base(fileResourceName)
        {
            this.RepositorySettings =
                new RepositorySettings(@"c:\Source\Cake.Issues");
        }

        protected override string FileResourceNamespace => "Cake.Issues.Markdownlint.Tests.Testfiles.";
    }
}
