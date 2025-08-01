namespace Cake.Issues.JUnit.Tests;

public sealed class JUnitIssuesProviderTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() =>
                new JUnitIssuesProvider(
                    null,
                    new JUnitIssuesSettings("Foo".ToByteArray())));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Throw_If_IssueProviderSettings_Are_Null()
        {
            // Given / When
            var result = Record.Exception(() => new JUnitIssuesProvider(new FakeLog(), null));

            // Then
            result.IsArgumentNullException("issueProviderSettings");
        }
    }

    public sealed class TheReadIssuesMethod
    {
        [Fact]
        public void Should_Read_Issues_Correct_For_CppLint()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture("cpplint.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);
            
            var issue1 = issues[0];
            issue1.ProviderType.ShouldBe("Cake.Issues.JUnit.JUnitIssuesProvider");
            issue1.ProviderName.ShouldBe("JUnit");
            issue1.MessageText.ShouldBe("Lines should be <= 80 characters long\nsrc/example.cpp:15:  Lines should be <= 80 characters long  [whitespace/line_length] [2]");
            issue1.Priority.ShouldBe(IssuePriority.Error);
            issue1.Rule.ShouldBe("warning");
            issue1.AffectedFileRelativePath.ShouldBe("src/example.cpp");
            issue1.Line.ShouldBe(15);

            var issue2 = issues[1];
            issue2.ProviderType.ShouldBe("Cake.Issues.JUnit.JUnitIssuesProvider");
            issue2.ProviderName.ShouldBe("JUnit");
            issue2.MessageText.ShouldBe("Include order issue\nsrc/example.cpp:5:  #includes are not properly sorted  [build/include_order] [4]");
            issue2.Priority.ShouldBe(IssuePriority.Error);
            issue2.Rule.ShouldBe("warning");
            issue2.AffectedFileRelativePath.ShouldBe("src/example.cpp");
            issue2.Line.ShouldBe(5);
        }

        [Fact]
        public void Should_Read_Issues_Correct_For_Kubeconform()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture("kubeconform.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);

            var issue1 = issues[0];
            issue1.ProviderType.ShouldBe("Cake.Issues.JUnit.JUnitIssuesProvider");
            issue1.ProviderName.ShouldBe("JUnit");
            issue1.MessageText.ShouldBe("Invalid resource definition\ndeployment.yaml:10:15: error validating data: ValidationError(Deployment.spec.template.spec.containers[0].image): invalid value: \"\", expected non-empty string");
            issue1.Priority.ShouldBe(IssuePriority.Error);
            issue1.Rule.ShouldBe("ValidationError");
            issue1.AffectedFileRelativePath.ShouldBe("deployment.yaml");
            issue1.Line.ShouldBe(10);
            issue1.Column.ShouldBe(15);

            var issue2 = issues[1];
            issue2.ProviderType.ShouldBe("Cake.Issues.JUnit.JUnitIssuesProvider");
            issue2.ProviderName.ShouldBe("JUnit");
            issue2.MessageText.ShouldBe("Port configuration invalid\nservice.yaml:8:5: Port 8080 is already in use by another service");
            issue2.Priority.ShouldBe(IssuePriority.Error);
            issue2.Rule.ShouldBe("ConfigError");
            issue2.AffectedFileRelativePath.ShouldBe("service.yaml");
            issue2.Line.ShouldBe(8);
            issue2.Column.ShouldBe(5);
        }

        [Fact]
        public void Should_Read_Issues_Correct_For_HtmlHint()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture("htmlhint.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);

            var issue1 = issues[0];
            issue1.ProviderType.ShouldBe("Cake.Issues.JUnit.JUnitIssuesProvider");
            issue1.ProviderName.ShouldBe("JUnit");
            issue1.MessageText.ShouldBe("Tagname must be lowercase\nindex.html(12,5): Tagname 'DIV' must be lowercase");
            issue1.Priority.ShouldBe(IssuePriority.Error);
            issue1.Rule.ShouldBe("error");
            issue1.AffectedFileRelativePath.ShouldBe("index.html");
            issue1.Line.ShouldBe(12);
            issue1.Column.ShouldBe(5);

            var issue2 = issues[1];
            issue2.ProviderType.ShouldBe("Cake.Issues.JUnit.JUnitIssuesProvider");
            issue2.ProviderName.ShouldBe("JUnit");
            issue2.MessageText.ShouldBe("Attribute value must be in double quotes\nabout.html line 8: The value of attribute 'class' must be in double quotes.");
            issue2.Priority.ShouldBe(IssuePriority.Error);
            issue2.Rule.ShouldBe("warning");
            issue2.AffectedFileRelativePath.ShouldBe("about.html");
            issue2.Line.ShouldBe(8);
        }

        [Fact]
        public void Should_Read_Issues_Correct_For_CommitLint()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture("commitlint.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);

            var issue = issues[0];
            issue.ProviderType.ShouldBe("Cake.Issues.JUnit.JUnitIssuesProvider");
            issue.ProviderName.ShouldBe("JUnit");
            issue.MessageText.ShouldBe("Type must be one of the allowed values\ncommit-2: type must be one of [build, chore, ci, docs, feat, fix, perf, refactor, revert, style, test]");
            issue.Priority.ShouldBe(IssuePriority.Error);
            issue.Rule.ShouldBe("error");
        }

        [Fact]
        public void Should_Handle_Empty_TestSuite()
        {
            // Given
            var junitContent = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<testsuite name=""empty"" tests=""0"" failures=""0"" errors=""0"" time=""0.000"">
</testsuite>";
            var fixture = new JUnitIssuesProviderFixture("empty.xml");
            fixture.SetFileContent(junitContent);

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(0);
        }

        [Fact]
        public void Should_Handle_Invalid_XML()
        {
            // Given
            var junitContent = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<testsuite name=""invalid"" tests=""1"" failures=""1"" errors=""0"">
  <testcase classname=""test"" name=""test"">
    <failure message=""test failure"">
  </testcase>";
            var fixture = new JUnitIssuesProviderFixture("invalid.xml");
            fixture.SetFileContent(junitContent);

            // When / Then
            Should.Throw<IssuesException>(() => fixture.ReadIssues().ToList())
                .Message.ShouldContain("Failed to parse JUnit XML");
        }
    }
}