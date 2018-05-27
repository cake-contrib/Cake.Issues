namespace Cake.Issues.Markdownlint.Tests.Markdownlint
{
    using System.Collections.Generic;
    using System.IO;
    using Cake.Issues.Markdownlint.Markdownlint;
    using Cake.Testing;
    using Core.Diagnostics;

    internal class MarkdownlintIssuesProviderFixture
    {
        public MarkdownlintIssuesProviderFixture(string fileResourceName)
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };

            using (var stream = this.GetType().Assembly.GetManifestResourceStream("Cake.Issues.Markdownlint.Tests.Testfiles." + fileResourceName))
            {
                using (var sr = new StreamReader(stream))
                {
                    this.Settings =
                        MarkdownlintIssuesSettings.FromContent(
                            sr.ReadToEnd());
                }
            }

            this.RepositorySettings =
                new RepositorySettings(@"c:\Source\Cake.Issues");
        }

        public FakeLog Log { get; set; }

        public MarkdownlintIssuesSettings Settings { get; set; }

        public RepositorySettings RepositorySettings { get; set; }

        public MarkdownlintIssuesProvider Create()
        {
            var provider = new MarkdownlintIssuesProvider(this.Log, this.Settings);
            provider.Initialize(this.RepositorySettings);
            return provider;
        }

        public IEnumerable<IIssue> ReadIssues()
        {
            var issueProvider = this.Create();
            return issueProvider.ReadIssues(IssueCommentFormat.PlainText);
        }
    }
}
