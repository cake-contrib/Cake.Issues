namespace Cake.Issues.CodeClimate.Tests;

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
                    new CodeClimateIssuesSettings(@"C:\build\log.json")));

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
                    new CodeClimateIssuesSettings(@"C:\build\log.json"));

            // When
            var result = provider.ProviderName;

            // Then
            result.ShouldBe("CodeClimate");
        }
    }

    public sealed class TheReadIssuesMethod : IssueProviderFixture<CodeClimateIssuesProvider, CodeClimateIssuesSettings>
    {
        [Fact]
        public void Should_Return_Empty_List_If_Log_Is_Empty()
        {
            // Given
            var fixture = new CodeClimateIssuesProviderFixture("[]");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.ShouldBeEmpty();
        }

        [Fact]
        public void Should_Return_Issue_For_Valid_Entry()
        {
            // Given
            var logFileContent = 
                """
                [
                  {
                    "type": "issue",
                    "check_name": "complexity",
                    "description": "Method has too many statements",
                    "categories": ["Complexity"],
                    "location": {
                      "path": "src/foo.js",
                      "lines": {
                        "begin": 10,
                        "end": 12
                      }
                    },
                    "severity": "major"
                  }
                ]
                """;

            var fixture = new CodeClimateIssuesProviderFixture(logFileContent);

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            CheckIssue(
                issues[0],
                @"src\foo.js",
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
            var logFileContent = 
                """
                [
                  {
                    "type": "issue",
                    "check_name": "style",
                    "description": "Missing semicolon",
                    "categories": ["Style"],
                    "location": {
                      "path": "src/bar.js",
                      "positions": {
                        "begin": {
                          "line": 5,
                          "column": 10
                        },
                        "end": {
                          "line": 5,
                          "column": 15
                        }
                      }
                    },
                    "severity": "minor"
                  }
                ]
                """;

            var fixture = new CodeClimateIssuesProviderFixture(logFileContent);

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            CheckIssue(
                issues[0],
                @"src\bar.js",
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
            var logFileContent = 
                """
                [
                  {
                    "type": "measurement",
                    "check_name": "complexity",
                    "description": "Complexity measurement",
                    "categories": ["Complexity"],
                    "location": {
                      "path": "src/foo.js",
                      "lines": {
                        "begin": 10,
                        "end": 12
                      }
                    }
                  }
                ]
                """;

            var fixture = new CodeClimateIssuesProviderFixture(logFileContent);

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.ShouldBeEmpty();
        }

        [Fact]
        public void Should_Handle_Missing_Severity()
        {
            // Given
            var logFileContent = 
                """
                [
                  {
                    "type": "issue",
                    "check_name": "complexity",
                    "description": "Method has too many statements",
                    "categories": ["Complexity"],
                    "location": {
                      "path": "src/foo.js",
                      "lines": {
                        "begin": 10,
                        "end": 12
                      }
                    }
                  }
                ]
                """;

            var fixture = new CodeClimateIssuesProviderFixture(logFileContent);

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            issues[0].Priority.ShouldBe(IssuePriority.Undefined);
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

            issue.Line.ShouldBe(line);
            issue.EndLine.ShouldBe(endLine);
            issue.Column.ShouldBe(column);
            issue.EndColumn.ShouldBe(endColumn);
            issue.FileLink.ShouldBeNull();
            issue.Rule.ShouldBe(rule);
            issue.RuleUrl.ShouldBeNull();
            issue.Priority.ShouldBe(priority);
            issue.Message.ShouldBe(message);
            issue.ProviderType.ShouldBe("Cake.Issues.CodeClimate.CodeClimateIssuesProvider");
            issue.ProviderName.ShouldBe("CodeClimate");
        }
    }

    private sealed class CodeClimateIssuesProviderFixture : BaseConfigurableIssueProviderFixture<CodeClimateIssuesProvider, CodeClimateIssuesSettings>
    {
        public CodeClimateIssuesProviderFixture(string fileContent)
            : base(fileContent)
        {
        }

        protected override string FilePathPrefix => "C:\\build\\";

        protected override string FilePathSuffix => "log.json";
    }
}