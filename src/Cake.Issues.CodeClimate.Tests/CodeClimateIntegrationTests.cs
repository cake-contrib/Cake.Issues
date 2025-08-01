namespace Cake.Issues.CodeClimate.Tests;

public class CodeClimateIntegrationTests
{
    [Fact]
    public void Should_Parse_Sample_CodeClimate_Output()
    {
        // Given
        var sampleJson = @"[
  {
    ""type"": ""issue"",
    ""check_name"": ""EditorConfig/indent_style"",
    ""description"": ""Wrong indentation type (expected: space, found: tab)"",
    ""categories"": [""Style""],
    ""location"": {
      ""path"": ""src/example.js"",
      ""lines"": {
        ""begin"": 5,
        ""end"": 5
      }
    },
    ""severity"": ""minor""
  },
  {
    ""type"": ""issue"",
    ""check_name"": ""complexity"",
    ""description"": ""Function has too many parameters"",  
    ""categories"": [""Complexity""],
    ""location"": {
      ""path"": ""src/complex.ts"",
      ""positions"": {
        ""begin"": {
          ""line"": 15,
          ""column"": 5
        },
        ""end"": {
          ""line"": 18,
          ""column"": 10
        }
      }
    },
    ""severity"": ""critical""
  }
]";

        var fixture = new CodeClimateIssuesProviderFixture(sampleJson);

        // When
        var issues = fixture.ReadIssues().ToList();

        // Then
        issues.Count.ShouldBe(2);
        
        // Check first issue (line-based)
        var firstIssue = issues.First(i => i.Rule == "EditorConfig/indent_style");
        firstIssue.Message.ShouldBe("Wrong indentation type (expected: space, found: tab)");
        firstIssue.AffectedFileRelativePath?.ToString().ShouldBe(@"src\example.js");
        firstIssue.Line.ShouldBe(5);
        firstIssue.EndLine.ShouldBe(5);
        firstIssue.Priority.ShouldBe(IssuePriority.Suggestion);

        // Check second issue (position-based)
        var secondIssue = issues.First(i => i.Rule == "complexity");
        secondIssue.Message.ShouldBe("Function has too many parameters");
        secondIssue.AffectedFileRelativePath?.ToString().ShouldBe(@"src\complex.ts");
        secondIssue.Line.ShouldBe(15);
        secondIssue.EndLine.ShouldBe(18);
        secondIssue.Column.ShouldBe(5);
        secondIssue.EndColumn.ShouldBe(10);
        secondIssue.Priority.ShouldBe(IssuePriority.Error);
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