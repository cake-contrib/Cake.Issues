namespace Cake.Issues.EsLint.Tests.LogFileFormat
{
    using Cake.Issues.EsLint.LogFileFormat;

    public sealed class JsonLogFileFormatTests
    {
        public sealed class TheJsonFormatCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new JsonLogFileFormat(null));

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
                var fixture = new EsLintIssuesProviderFixture<JsonLogFileFormat>("jsonFormatWindows.json");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(9);
                IssueChecker.Check(
                    issues[0],
                    IssueBuilder.NewIssue(
                        "'addOne' is defined but never used.",
                        "Cake.Issues.EsLint.EsLintIssuesProvider",
                        "ESLint")
                        .InFile(@"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js", 1, 10)
                        .OfRule("no-unused-vars", new Uri("http://eslint.org/docs/rules/no-unused-vars"))
                        .WithPriority(IssuePriority.Error)
                        .Create());
                IssueChecker.Check(
                    issues[1],
                    IssueBuilder.NewIssue(
                        "Use the isNaN function to compare with NaN.",
                        "Cake.Issues.EsLint.EsLintIssuesProvider",
                        "ESLint")
                        .InFile(@"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js", 2, 9)
                        .OfRule("use-isnan", new Uri("http://eslint.org/docs/rules/use-isnan"))
                        .WithPriority(IssuePriority.Error)
                        .Create());
                IssueChecker.Check(
                    issues[2],
                    IssueBuilder.NewIssue(
                        "Unexpected space before unary operator '++'.",
                        "Cake.Issues.EsLint.EsLintIssuesProvider",
                        "ESLint")
                        .InFile(@"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js", 3, 16)
                        .OfRule("space-unary-ops", new Uri("http://eslint.org/docs/rules/space-unary-ops"))
                        .WithPriority(IssuePriority.Error)
                        .Create());
                IssueChecker.Check(
                    issues[3],
                    IssueBuilder.NewIssue(
                        "Missing semicolon.",
                        "Cake.Issues.EsLint.EsLintIssuesProvider",
                        "ESLint")
                        .InFile(@"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js", 3, 20)
                        .OfRule("semi", new Uri("http://eslint.org/docs/rules/semi"))
                        .WithPriority(IssuePriority.Warning)
                        .Create());
                IssueChecker.Check(
                    issues[4],
                    IssueBuilder.NewIssue(
                        "Unnecessary 'else' after 'return'.",
                        "Cake.Issues.EsLint.EsLintIssuesProvider",
                        "ESLint")
                        .InFile(@"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js", 4, 12)
                        .OfRule("no-else-return", new Uri("http://eslint.org/docs/rules/no-else-return"))
                        .WithPriority(IssuePriority.Warning)
                        .Create());
                IssueChecker.Check(
                    issues[5],
                    IssueBuilder.NewIssue(
                        "Expected indentation of 8 spaces but found 6.",
                        "Cake.Issues.EsLint.EsLintIssuesProvider",
                        "ESLint")
                        .InFile(@"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js", 5, 1)
                        .OfRule("indent", new Uri("http://eslint.org/docs/rules/indent"))
                        .WithPriority(IssuePriority.Warning)
                        .Create());
                IssueChecker.Check(
                    issues[6],
                    IssueBuilder.NewIssue(
                        "Function 'addOne' expected a return value.",
                        "Cake.Issues.EsLint.EsLintIssuesProvider",
                        "ESLint")
                        .InFile(@"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js", 5, 7)
                        .OfRule("consistent-return", new Uri("http://eslint.org/docs/rules/consistent-return"))
                        .WithPriority(IssuePriority.Error)
                        .Create());
                IssueChecker.Check(
                    issues[7],
                    IssueBuilder.NewIssue(
                        "Missing semicolon.",
                        "Cake.Issues.EsLint.EsLintIssuesProvider",
                        "ESLint")
                        .InFile(@"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js", 5, 13)
                        .OfRule("semi", new Uri("http://eslint.org/docs/rules/semi"))
                        .WithPriority(IssuePriority.Warning)
                        .Create());
                IssueChecker.Check(
                    issues[8],
                    IssueBuilder.NewIssue(
                        "Unnecessary semicolon.",
                        "Cake.Issues.EsLint.EsLintIssuesProvider",
                        "ESLint")
                        .InFile(@"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js", 7, 2)
                        .OfRule("no-extra-semi", new Uri("http://eslint.org/docs/rules/no-extra-semi"))
                        .WithPriority(IssuePriority.Error)
                        .Create());
            }

            [Fact]
            public void Should_Read_Issue_Without_Line_And_Column_Correct()
            {
                // Given
                var fixture = new EsLintIssuesProviderFixture<JsonLogFileFormat>("issueWithoutLineAndColumn.json");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                IssueChecker.Check(
                    issues.Single(),
                    IssueBuilder.NewIssue(
                        "some message.",
                        "Cake.Issues.EsLint.EsLintIssuesProvider",
                        "ESLint")
                        .InFile(@"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js")
                        .OfRule("some-rule", new Uri("http://eslint.org/docs/rules/some-rule"))
                        .WithPriority(IssuePriority.Error)
                        .Create());
            }

            [Fact]
            public void Should_Read_Issue_Without_Rule_Correct()
            {
                // Given
                var fixture = new EsLintIssuesProviderFixture<JsonLogFileFormat>("issueWithoutRule.json");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                IssueChecker.Check(
                    issues.Single(),
                    IssueBuilder.NewIssue(
                        "some message.",
                        "Cake.Issues.EsLint.EsLintIssuesProvider",
                        "ESLint")
                        .InFile(@"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js", 1, 10)
                        .WithPriority(IssuePriority.Error)
                        .Create());
            }
        }
    }
}
