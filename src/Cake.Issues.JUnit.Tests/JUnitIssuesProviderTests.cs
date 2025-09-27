namespace Cake.Issues.JUnit.Tests;

using Cake.Issues.JUnit.LogFileFormat;

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
                    new JUnitIssuesSettings("Foo".ToByteArray(), new GenericJUnitLogFileFormat(new FakeLog()))));

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
            var fixture = new JUnitIssuesProviderFixture<GenericJUnitLogFileFormat>("cpplint.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);

            var issue = issues[0];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Lines should be <= 80 characters long\nsrc/example.cpp:15:  Lines should be <= 80 characters long  [whitespace/line_length] [2]",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .InFile(@"src/example.cpp", 15)
                    .OfRule("warning")
                    .WithPriority(IssuePriority.Error)
                    .Create());

            issue = issues[1];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Include order issue\nsrc/example.cpp:5:  #includes are not properly sorted  [build/include_order] [4]",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .InFile(@"src/example.cpp", 5)
                    .OfRule("warning")
                    .WithPriority(IssuePriority.Error)
                    .Create());
        }

        [Fact]
        public void Should_Handle_CppLint_Passed_Test()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture<CppLintLogFileFormat>("cpplint-passed.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.ShouldBeEmpty();
        }

        [Fact]
        public void Should_Handle_CppLint_Single_Error()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture<CppLintLogFileFormat>("cpplint-single-error.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            var issue = issues.ShouldHaveSingleItem();

            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "ErrMsg1",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .OfRule("errors")
                    .WithPriority(IssuePriority.Error)
                    .Create());
        }

        [Fact]
        public void Should_Handle_CppLint_Multiple_Errors()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture<CppLintLogFileFormat>("cpplint-multiple-errors.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            var issue = issues.ShouldHaveSingleItem();

            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "ErrMsg1\nErrMsg2",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .OfRule("errors")
                    .WithPriority(IssuePriority.Error)
                    .Create());
        }

        [Fact]
        public void Should_Handle_CppLint_Mixed_Error_And_Failure()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture<CppLintLogFileFormat>("cpplint-mixed-error-failure.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);

            IssueChecker.Check(
                issues[0],
                IssueBuilder.NewIssue(
                    "ErrMsg",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .OfRule("errors")
                    .WithPriority(IssuePriority.Error)
                    .Create());

            IssueChecker.Check(
                issues[1],
                IssueBuilder.NewIssue(
                    "5: FailMsg [category/subcategory] [3]",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .InFile("File", 5)
                    .OfRule("File")
                    .WithPriority(IssuePriority.Error)
                    .Create());
        }

        [Fact]
        public void Should_Handle_CppLint_Multiple_Failures_Grouped_By_File()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture<CppLintLogFileFormat>("cpplint-multiple-failures.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);

            IssueChecker.Check(
                issues[0],
                IssueBuilder.NewIssue(
                    "5: FailMsg1 [category/subcategory] [3]\n19: FailMsg3 [category/subcategory] [3]",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .InFile("File1", 5)
                    .OfRule("File1")
                    .WithPriority(IssuePriority.Error)
                    .Create());

            IssueChecker.Check(
                issues[1],
                IssueBuilder.NewIssue(
                    "99: FailMsg2 [category/subcategory] [3]",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .InFile("File2", 99)
                    .OfRule("File2")
                    .WithPriority(IssuePriority.Error)
                    .Create());
        }

        [Fact]
        public void Should_Handle_CppLint_XML_Escaping()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture<CppLintLogFileFormat>("cpplint-xml-escaping.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);

            IssueChecker.Check(
                issues[0],
                IssueBuilder.NewIssue(
                    "&</error>",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .OfRule("errors")
                    .WithPriority(IssuePriority.Error)
                    .Create());

            IssueChecker.Check(
                issues[1],
                IssueBuilder.NewIssue(
                    "5: &</failure> [category/subcategory] [3]",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .InFile("File1", 5)
                    .OfRule("File1")
                    .WithPriority(IssuePriority.Error)
                    .Create());
        }

        [Fact]
        public void Should_Read_Issues_Correct_For_Kubeconform()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture<GenericJUnitLogFileFormat>("kubeconform.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);

            IssueChecker.Check(
                issues[0],
                IssueBuilder.NewIssue(
                    "Invalid resource definition\ndeployment.yaml:10:15: error validating data: ValidationError(Deployment.spec.template.spec.containers[0].image): invalid value: \"\", expected non-empty string",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .InFile(@"deployment.yaml", 10, 15)
                    .OfRule("ValidationError")
                    .WithPriority(IssuePriority.Error)
                    .Create());

            IssueChecker.Check(
                issues[1],
                IssueBuilder.NewIssue(
                    "Port configuration invalid\nservice.yaml:8:5: Port 8080 is already in use by another service",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .InFile(@"service.yaml", 8, 5)
                    .OfRule("ConfigError")
                    .WithPriority(IssuePriority.Error)
                    .Create());
        }

        [Fact]
        public void Should_Read_Issues_Correct_For_HtmlHint()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture<GenericJUnitLogFileFormat>("htmlhint.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);

            IssueChecker.Check(
                issues[0],
                IssueBuilder.NewIssue(
                    "Found 6 errors\nL2 |<html>\n^ An lang attribute must be present on <html> elements. (html-lang-require)\nL16 |  <img src=\"image.jpg\">\n^ An alt attribute must be present on <img> elements. (alt-require)\nL14 |\n^ <main> must be present in <body> tag. (main-require)\nL3 |\n^ <meta charset=\"\"> must be present in <head> tag. (meta-charset-require)\nL3 |\n^ <meta name=\"description\"> must be present in <head> tag. (meta-description-require)\nL3 |\n^ <meta name=\"viewport\"> must be present in <head> tag. (meta-viewport-require)",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .OfRule(@"C:\temp\htmlhint\test.html")
                    .WithPriority(IssuePriority.Error)
                    .Create());
        }

        [Fact]
        public void Should_Read_Issues_Correct_For_CommitLint()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture<GenericJUnitLogFileFormat>("commitlint.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            var issue = issues.ShouldHaveSingleItem();

            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "type must be one of [build, chore, ci, docs, feat, fix, perf, refactor, revert, style, test] (type-enum)\n\nfoo: bar",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .OfRule("error")
                    .WithPriority(IssuePriority.Error)
                    .Create());
        }

        [Fact]
        public void Should_Read_Issues_Correct_For_MarkdownlintCli2()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture<MarkdownlintCli2LogFileFormat>("markdownlint-cli2.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(5);

            IssueChecker.Check(
                issues[0],
                IssueBuilder.NewIssue(
                    "Trailing spaces\nLine 3, Column 10, Expected: 0 or 2; Actual: 1",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .InFile(@"viewme.md", 3, 10)
                    .OfRule("MD009/no-trailing-spaces")
                    .WithPriority(IssuePriority.Error)
                    .Create());

            IssueChecker.Check(
                issues[1],
                IssueBuilder.NewIssue(
                    "Multiple consecutive blank lines\nLine 5, Expected: 1; Actual: 2",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .InFile(@"viewme.md", 5)
                    .OfRule("MD012/no-multiple-blanks")
                    .WithPriority(IssuePriority.Error)
                    .Create());

            IssueChecker.Check(
                issues[2],
                IssueBuilder.NewIssue(
                    "Multiple top-level headings in the same document\nLine 6, Context: \"# Description\"",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .InFile(@"viewme.md", 6)
                    .OfRule("MD025/single-title/single-h1")
                    .WithPriority(IssuePriority.Error)
                    .Create());

            IssueChecker.Check(
                issues[3],
                IssueBuilder.NewIssue(
                    "Multiple spaces after hash on atx style heading\nLine 12, Column 1, Context: \"##  Summary\"",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .InFile(@"viewme.md", 12, 1)
                    .OfRule("MD019/no-multiple-space-atx")
                    .WithPriority(IssuePriority.Error)
                    .Create());

            IssueChecker.Check(
                issues[4],
                IssueBuilder.NewIssue(
                    "Files should end with a single newline character\nLine 14, Column 14",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .InFile(@"viewme.md", 14, 14)
                    .OfRule("MD047/single-trailing-newline")
                    .WithPriority(IssuePriority.Error)
                    .Create());
        }

        [Fact]
        public void Should_Handle_Empty_TestSuite()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture<GenericJUnitLogFileFormat>("empty.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.ShouldBeEmpty();
        }

        [Fact]
        public void Should_Handle_Invalid_XML()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture<GenericJUnitLogFileFormat>("invalid.xml");

            // When / Then
            Should.Throw<Exception>(() => fixture.ReadIssues().ToList())
                .Message.ShouldContain("Failed to parse JUnit XML");
        }
    }
}