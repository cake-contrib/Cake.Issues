namespace Cake.Issues.Tests
{
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Cake.Issues.Testing;
    using Cake.Testing;

    public class IssuesFixture
    {
        public IssuesFixture()
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };
            this.IssueProviders = new List<FakeIssueProvider> { new FakeIssueProvider(this.Log) };
            this.Settings =
                new ReadIssuesSettings(
                    new Core.IO.DirectoryPath(@"c:\Source\Cake.Issues"));
        }

        public FakeLog Log { get; set; }

        public IList<FakeIssueProvider> IssueProviders { get; set; }

        public ReadIssuesSettings Settings { get; set; }

        public IEnumerable<IIssue> ReadIssues()
        {
            var issueReader = new IssuesReader(this.Log, this.IssueProviders, this.Settings);
            return issueReader.ReadIssues();
        }
    }
}
