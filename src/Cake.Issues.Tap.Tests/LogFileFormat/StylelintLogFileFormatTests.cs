namespace Cake.Issues.Tap.Tests.LogFileFormat;

using Cake.Issues.Tap.LogFileFormat;

public sealed class StylelintLogFileFormatTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() => new StylelintLogFileFormat(null));

            // Then
            result.IsArgumentNullException("log");
        }
    }

    public sealed class TheReadIssuesMethod
    {
        [Fact]
        public void Should_Read_Issue_Correct_When_No_Warnings()
        {
            // Given
            var fixture = new TapIssuesProviderFixture<StylelintLogFileFormat>("no-warnings.tap");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(0);
        }

        [Fact]
        public void Should_Read_Issue_Correct_When_Warnings()
        {
            // Given
            var fixture = new TapIssuesProviderFixture<StylelintLogFileFormat>("warnings.tap");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(4);

            var issue = issues[0];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Unexpected bar",
                    "Cake.Issues.Tap.TapIssuesProvider",
                    "TAP")
                    .InFile("path/to/file1.css", 2, 3, 1, 3)
                    .WithPriority(IssuePriority.Error)
                    .OfRule("baz", new Uri("https://stylelint.io/user-guide/rules/baz"))
                    .Create());

            issue = issues[1];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Unexpected foo",
                    "Cake.Issues.Tap.TapIssuesProvider",
                    "TAP")
                    .InFile("path/to/file2.css", 1, 2, 1, 3)
                    .WithPriority(IssuePriority.Error)
                    .OfRule("bar", new Uri("https://stylelint.io/user-guide/rules/bar"))
                    .Create());

            issue = issues[2];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Unexpected foo",
                    "Cake.Issues.Tap.TapIssuesProvider",
                    "TAP")
                    .InFile("path/to/file2.css", 4, 5, 1, 3)
                    .WithPriority(IssuePriority.Error)
                    .OfRule("bar", new Uri("https://stylelint.io/user-guide/rules/bar"))
                    .Create());

            issue = issues[3];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Unexpected foo 2",
                    "Cake.Issues.Tap.TapIssuesProvider",
                    "TAP")
                    .InFile("path/to/file2.css", 10, 11, 1, 2)
                    .WithPriority(IssuePriority.Error)
                    .OfRule("bar2", new Uri("https://stylelint.io/user-guide/rules/bar2"))
                    .Create());
        }

        [Fact]
        public void Should_Read_Issue_Correct_When_Parse_Error()
        {
            // Given
            var fixture = new TapIssuesProviderFixture<StylelintLogFileFormat>("parse-error.tap");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);

            var issue = issues[0];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Cannot parse selector (parseError)",
                    "Cake.Issues.Tap.TapIssuesProvider",
                    "TAP")
                    .InFile("path/to/file.css", 1, 1)
                    .WithPriority(IssuePriority.Error)
                    .OfRule("parseError")
                    .Create());

            issue = issues[1];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Unexpected foo",
                    "Cake.Issues.Tap.TapIssuesProvider",
                    "TAP")
                    .InFile("path/to/file.css", 2, 1)
                    .WithPriority(IssuePriority.Error)
                    .OfRule("unknown")
                    .Create());
        }

        [Fact]
        public void Should_Read_Issue_Correct_When_Message_Contains_Double_Quotes()
        {
            // Given
            var fixture = new TapIssuesProviderFixture<StylelintLogFileFormat>("double-quotes-in-message.tap");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);

            var issue = issues[0];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Unexpected \"width\" property. Use \"inline-size\". (csstools/use-logical)",
                    "Cake.Issues.Tap.TapIssuesProvider",
                    "TAP")
                    .InFile("path/to/file.css", 13, 13, 3, 15)
                    .WithPriority(IssuePriority.Error)
                    .OfRule("csstools/use-logical")
                    .Create());
        }

        [Theory]
        [InlineData(@"c:\Source\Cake.Issues", "test.scss")]
        [InlineData(@"c:\Source\Cake.Issues\", "test.scss")]
        [InlineData(@"c:/Source/Cake.Issues", "test.scss")]
        [InlineData(@"c:/Source/Cake.Issues/", "test.scss")]
        [InlineData(@"c:\Source", @"Cake.Issues\test.scss")]
        [InlineData(@"c:\Source\", @"Cake.Issues\test.scss")]
        [InlineData(@"c:/Source", @"Cake.Issues\test.scss")]
        [InlineData(@"c:/Source/", @"Cake.Issues\test.scss")]
        public void Should_Read_Issue_Correct_When_Absolute_Windows_Path(string repositoryRoot, string relativeFilePath)
        {
            // Given
            var fixture = new TapIssuesProviderFixture<StylelintLogFileFormat>("absolute-windows-path.tap", repositoryRoot);

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);

            var issue = issues[0];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Unexpected empty block (block-no-empty)",
                    "Cake.Issues.Tap.TapIssuesProvider",
                    "TAP")
                    .InFile(relativeFilePath, 16, 16, 6, 8)
                    .WithPriority(IssuePriority.Error)
                    .OfRule("block-no-empty", new Uri("https://stylelint.io/user-guide/rules/block-no-empty"))
                    .Create());
        }

        [Theory]
        [InlineData("/src/Cake.Issues", "test.scss")]
        [InlineData("/src/Cake.Issues/", "test.scss")]
        [InlineData("/src", "Cake.Issues/test.scss")]
        [InlineData("/src/", "Cake.Issues/test.scss")]
        public void Should_Read_Issue_Correct_When_Absolute_Linux_Path(string repositoryRoot, string relativeFilePath)
        {
            // Given
            var fixture = new TapIssuesProviderFixture<StylelintLogFileFormat>("absolute-linux-path.tap", repositoryRoot);

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);

            var issue = issues[0];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Unexpected empty block (block-no-empty)",
                    "Cake.Issues.Tap.TapIssuesProvider",
                    "TAP")
                    .InFile(relativeFilePath, 16, 16, 6, 8)
                    .WithPriority(IssuePriority.Error)
                    .OfRule("block-no-empty", new Uri("https://stylelint.io/user-guide/rules/block-no-empty"))
                    .Create());
        }

        [Fact]
        public void Should_Ignore_Issues_From_Outside_Repository()
        {
            // Given
            var fixture = new TapIssuesProviderFixture<StylelintLogFileFormat>("absolute-windows-path.tap", @"c:\foo");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(0);
        }
    }
}
