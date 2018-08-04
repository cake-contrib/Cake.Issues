namespace Cake.Issues.MsBuild.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using Cake.Testing;
    using Core.Diagnostics;

    internal class MsBuildIssuesProviderFixture
    {
        public MsBuildIssuesProviderFixture(string fileResourceName)
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };

            using (var ms = new MemoryStream())
            using (var stream = this.GetType().Assembly.GetManifestResourceStream("Cake.Issues.MsBuild.Tests.Testfiles." + fileResourceName))
            {
                stream.CopyTo(ms);
                this.MsBuildIssuesSettings =
                    new MsBuildIssuesSettings(ms.ToArray(), new XmlFileLoggerFormat(this.Log));
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
