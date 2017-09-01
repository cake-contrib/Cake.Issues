namespace Cake.Issues.MsBuild.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using Cake.Testing;
    using Core.Diagnostics;
    using IssueProvider;

    internal class MsBuildIssuesProviderFixture
    {
        public MsBuildIssuesProviderFixture(string fileResourceName)
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };

            using (var stream = this.GetType().Assembly.GetManifestResourceStream("Cake.Issues.MsBuild.Tests.Testfiles." + fileResourceName))
            {
                using (var sr = new StreamReader(stream))
                {
                    this.MsBuildIssuesSettings =
                        MsBuildIssuesSettings.FromContent(
                            sr.ReadToEnd(),
                            new XmlFileLoggerFormat(this.Log));
                }
            }

            this.RepositorySettings =
                new RepositorySettings(@"c:\Source\Cake.Issues.MsBuild");
        }

        public FakeLog Log { get; set; }

        public MsBuildIssuesSettings MsBuildIssuesSettings { get; set; }

        public RepositorySettings RepositorySettings { get; set; }

        public MsBuildIssuesProvider Create()
        {
            var provider = new MsBuildIssuesProvider(this.Log, this.MsBuildIssuesSettings);
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
