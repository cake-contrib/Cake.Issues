namespace Cake.Issues.EsLint.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using Cake.Testing;
    using Core.Diagnostics;

    public class EsLintIssuesProviderFixture
    {
        public EsLintIssuesProviderFixture(string fileResourceName)
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };

            using (var stream = this.GetType().Assembly.GetManifestResourceStream("Cake.Issues.EsLint.Tests.Testfiles." + fileResourceName))
            {
                using (var sr = new StreamReader(stream))
                {
                    this.Settings =
                        EsLintIssuesSettings.FromContent(
                            sr.ReadToEnd(),
                            new JsonFormat(this.Log));
                }
            }

            this.RepositorySettings =
                new RepositorySettings(@"c:\Source\Cake.Issues");
        }

        public FakeLog Log { get; set; }

        public EsLintIssuesSettings Settings { get; set; }

        public RepositorySettings RepositorySettings { get; set; }

        internal EsLintIssuesProvider Create()
        {
            var provider = new EsLintIssuesProvider(this.Log, this.Settings);
            provider.Initialize(this.RepositorySettings);
            return provider;
        }

        internal IEnumerable<IIssue> ReadIssues()
        {
            var issueProvider = this.Create();
            return issueProvider.ReadIssues(IssueCommentFormat.PlainText);
        }
    }
}
