namespace Cake.Issues.InspectCode.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using Cake.Testing;
    using Core.Diagnostics;

    internal class InspectCodeIssuesProviderFixture
    {
        public InspectCodeIssuesProviderFixture(string fileResourceName)
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };

            using (var stream = this.GetType().Assembly.GetManifestResourceStream("Cake.Issues.InspectCode.Tests.Testfiles." + fileResourceName))
            {
                using (var sr = new StreamReader(stream))
                {
                    this.Settings =
                        InspectCodeIssuesSettings.FromContent(
                            sr.ReadToEnd());
                }
            }

            this.RepositorySettings =
                new RepositorySettings(@"c:\Source\Cake.Issues");
        }

        public FakeLog Log { get; set; }

        public InspectCodeIssuesSettings Settings { get; set; }

        public RepositorySettings RepositorySettings { get; set; }

        public InspectCodeIssuesProvider Create()
        {
            var provider = new InspectCodeIssuesProvider(this.Log, this.Settings);
            provider.Initialize(this.RepositorySettings);
            return provider;
        }

        public IEnumerable<IIssue> ReadIssues()
        {
            var codeAnalysisProvider = this.Create();
            return codeAnalysisProvider.ReadIssues(IssueCommentFormat.PlainText);
        }
    }
}
