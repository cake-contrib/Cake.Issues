namespace Cake.Issues.CodeClimate.Tests;

public class CodeClimateIntegrationTests
{
    [Fact]
    public void Should_Parse_Sample_CodeClimate_Output()
    {
        // Given
        var fixture = new CodeClimateIssuesProviderFixture("integration_test.json");

        // When
        var issues = fixture.ReadIssues().ToList();

        // Then
        issues.Count.ShouldBe(2);
        
        // Check first issue (line-based)
        var firstIssue = issues.First(i => i.Rule() == "EditorConfig/indent_style");
        firstIssue.Message(IssueCommentFormat.PlainText).ShouldBe("Wrong indentation type (expected: space, found: tab)");
        firstIssue.AffectedFileRelativePath?.ToString().ShouldBe("src/example.js");
        firstIssue.Line.ShouldBe(5);
        firstIssue.EndLine.ShouldBe(5);
        firstIssue.Priority.ShouldBe((int)IssuePriority.Suggestion);

        // Check second issue (position-based)
        var secondIssue = issues.First(i => i.Rule() == "complexity");
        secondIssue.Message(IssueCommentFormat.PlainText).ShouldBe("Function has too many parameters");
        secondIssue.AffectedFileRelativePath?.ToString().ShouldBe("src/complex.ts");
        secondIssue.Line.ShouldBe(15);
        secondIssue.EndLine.ShouldBe(18);
        secondIssue.Column.ShouldBe(5);
        secondIssue.EndColumn.ShouldBe(10);
        secondIssue.Priority.ShouldBe((int)IssuePriority.Error);
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