namespace Cake.Issues.DocFx.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using Cake.Testing;
    using Core.Diagnostics;
    using Core.IO;
    using DocFx;
    using IssueProvider;
    using Issues;

    internal class DocFxProviderFixture
    {
        public DocFxProviderFixture(string fileResourceName, DirectoryPath docRootPath)
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };

            using (var stream = this.GetType().Assembly.GetManifestResourceStream("Cake.Issues.DocFx.Tests.Testfiles." + fileResourceName))
            {
                using (var sr = new StreamReader(stream))
                {
                    this.DocFxIssuesSettings =
                        DocFxIssuesSettings.FromContent(
                            sr.ReadToEnd(),
                            docRootPath);
                }
            }

            this.RepositorySettings =
                new RepositorySettings(@"c:\Source\Cake.Issues");
        }

        public FakeLog Log { get; set; }

        public DocFxIssuesSettings DocFxIssuesSettings { get; set; }

        public RepositorySettings RepositorySettings { get; set; }

        public DocFxIssuesProvider Create()
        {
            var provider = new DocFxIssuesProvider(this.Log, this.DocFxIssuesSettings);
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
