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
            var fixture = new JUnitIssuesProviderFixture("cpplint-passed.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.ShouldBeEmpty();
        }

        [Fact]
        public void Should_Handle_CppLint_Single_Error()
        {
            // Given
            var fixture = new JUnitIssuesProviderFixture("cpplint-single-error.xml");

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
            var fixture = new JUnitIssuesProviderFixture("cpplint-multiple-errors.xml");

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
            var fixture = new JUnitIssuesProviderFixture("cpplint-mixed-error-failure.xml");

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
            var fixture = new JUnitIssuesProviderFixture("cpplint-multiple-failures.xml");

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
            var fixture = new JUnitIssuesProviderFixture("cpplint-xml-escaping.xml");

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
            var fixture = new JUnitIssuesProviderFixture("kubeconform.xml");

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
            var fixture = new JUnitIssuesProviderFixture("htmlhint.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);

            IssueChecker.Check(
                issues[0],
                IssueBuilder.NewIssue(
                    "Tagname must be lowercase\nindex.html(12,5): Tagname 'DIV' must be lowercase",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .InFile(@"index.html", 12, 5)
                    .OfRule("error")
                    .WithPriority(IssuePriority.Error)
                    .Create());

            IssueChecker.Check(
                issues[1],
                IssueBuilder.NewIssue(
                    "Attribute value must be in double quotes\nabout.html line 8: The value of attribute 'class' must be in double quotes.",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .InFile(@"about.html", 8)
                    .OfRule("warning")
                    .WithPriority(IssuePriority.Error)
                    .Create());
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

            IssueChecker.Check(
                issues[0],
                IssueBuilder.NewIssue(
                    "type must be one of [build, chore, ci, docs, feat, fix, perf, refactor, revert, style, test] (type-enum)\n\nfoo: bar",
                    "Cake.Issues.JUnit.JUnitIssuesProvider",
                    "JUnit")
                    .OfRule("error")
                    .WithPriority(IssuePriority.Error)
                    .Create());
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
            Should.Throw<Exception>(() => fixture.ReadIssues().ToList())
                .Message.ShouldContain("Failed to parse JUnit XML");
        }
    }
}