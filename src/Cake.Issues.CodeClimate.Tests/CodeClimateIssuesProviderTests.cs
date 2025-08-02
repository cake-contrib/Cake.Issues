namespace Cake.Issues.CodeClimate.Tests;

using System.Linq;

public class CodeClimateIssuesProviderTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() =>
                new CodeClimateIssuesProvider(
                    null!,
                    new CodeClimateIssuesSettings("Foo".ToByteArray())));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Throw_If_IssueProviderSettings_Are_Null()
        {
            // Given / When
            var result = Record.Exception(() =>
                new CodeClimateIssuesProvider(
                    new FakeLog(),
                    null!));

            // Then
            result.IsArgumentNullException("issueProviderSettings");
        }
    }

    public sealed class TheProviderNameProperty
    {
        [Fact]
        public void Should_Return_Correct_Value()
        {
            // Given
            var provider =
                new CodeClimateIssuesProvider(
                    new FakeLog(),
                    new CodeClimateIssuesSettings("Foo".ToByteArray()));

            // When
            var result = provider.ProviderName;

            // Then
            result.ShouldBe("CodeClimate");
        }
    }

    public sealed class TheReadIssuesMethod
    {
        [Fact]
        public void Should_Return_Empty_List_If_Log_Is_Empty()
        {
            // Given
            var fixture = new CodeClimateIssuesProviderFixture("empty.json");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.ShouldBeEmpty();
        }

        [Fact]
        public void Should_Return_Issue_For_Valid_Entry()
        {
            // Given
            var fixture = new CodeClimateIssuesProviderFixture("valid_issue.json");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            CheckIssue(
                issues[0],
                "src/foo.js",
                10,
                12,
                null,
                null,
                "complexity",
                IssuePriority.Warning,
                "Method has too many statements");
        }

        [Fact]
        public void Should_Return_Issue_For_Position_Based_Location()
        {
            // Given
            var fixture = new CodeClimateIssuesProviderFixture("position_based.json");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            CheckIssue(
                issues[0],
                "src/bar.js",
                5,
                5,
                10,
                15,
                "style",
                IssuePriority.Suggestion,
                "Missing semicolon");
        }

        [Fact]
        public void Should_Skip_Non_Issue_Types()
        {
            // Given
            var fixture = new CodeClimateIssuesProviderFixture("non_issue_type.json");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.ShouldBeEmpty();
        }

        [Fact]
        public void Should_Handle_Missing_Severity()
        {
            // Given
            var fixture = new CodeClimateIssuesProviderFixture("missing_severity.json");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            issues[0].Priority.ShouldBe((int)IssuePriority.Undefined);
        }

        private static void CheckIssue(
            IIssue issue,
            string affectedFileRelativePath,
            int? line,
            int? endLine,
            int? column,
            int? endColumn,
            string rule,
            IssuePriority priority,
            string message)
        {
            if (issue.AffectedFileRelativePath == null)
            {
                affectedFileRelativePath.ShouldBeNull();
            }
            else
            {
                issue.AffectedFileRelativePath.ToString().ShouldBe(affectedFileRelativePath);
            }

            if (line.HasValue)
                issue.Line.ShouldBe(line.Value);
            else
                issue.Line.ShouldBeNull();
                
            if (endLine.HasValue)
                issue.EndLine.ShouldBe(endLine.Value);
            else
                issue.EndLine.ShouldBeNull();
                
            if (column.HasValue)
                issue.Column.ShouldBe(column.Value);
            else
                issue.Column.ShouldBeNull();
                
            if (endColumn.HasValue)
                issue.EndColumn.ShouldBe(endColumn.Value);
            else
                issue.EndColumn.ShouldBeNull();
            issue.FileLink.ShouldBeNull();
            issue.Rule().ShouldBe(rule);
            issue.RuleUrl.ShouldBeNull();
            issue.Priority.ShouldBe((int)priority);
            issue.Message(IssueCommentFormat.PlainText).ShouldBe(message);
            issue.ProviderType.ShouldBe("Cake.Issues.CodeClimate.CodeClimateIssuesProvider");
            issue.ProviderName.ShouldBe("CodeClimate");
        }
    }

    private sealed class CodeClimateIssuesProviderFixture : BaseConfigurableIssueProviderFixture<CodeClimateIssuesProvider, CodeClimateIssuesSettings>
    {
        public CodeClimateIssuesProviderFixture(string fileResourceName)
            : base(fileResourceName)
        {
            this.ReadIssuesSettings = new ReadIssuesSettings(@"C:\build\");
        }

        protected override string FileResourceNamespace => "Cake.Issues.CodeClimate.Tests.Testfiles.";
    }
}