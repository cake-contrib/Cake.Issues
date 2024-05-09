namespace Cake.Issues.Tests
{
    using Cake.Core.Diagnostics;

    public class IssuesFixture
    {
        public IssuesFixture()
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };
            this.IssueProviders = new List<FakeIssueProvider> { new(this.Log) };
            this.Settings =
                new ReadIssuesSettings(
                    new Core.IO.DirectoryPath(@"c:\Source\Cake.Issues"));
        }

        public FakeLog Log { get; init; }

        public IList<FakeIssueProvider> IssueProviders { get; init; }

        public ReadIssuesSettings Settings { get; init; }

        public IEnumerable<IIssue> ReadIssues()
        {
            var issueReader = new IssuesReader(this.Log, this.IssueProviders, this.Settings);
            return issueReader.ReadIssues();
        }
    }
}
