namespace Cake.Issues.Tap.Tests.LogFileFormat;

using Cake.Issues.Tap.LogFileFormat;

public sealed class TextlintLogFileFormatTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() => new TextlintLogFileFormat(null));

            // Then
            result.IsArgumentNullException("log");
        }
    }

    public sealed class TheReadIssuesMethod
    {
        [Fact]
        public void Should_Read_Issue_Correct()
        {
            // Given
            var fixture = new TapIssuesProviderFixture<TextlintLogFileFormat>("textlint.tap");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);

            var issue = issues[0];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Found TODO: '- [ ] Write usage instructions'",
                    "Cake.Issues.Tap.TapIssuesProvider",
                    "TAP")
                    .InFile(@"file.md", 3, 3)
                    .WithPriority(IssuePriority.Error)
                    .OfRule("no-todo")
                    .Create());
        }
    }
}
