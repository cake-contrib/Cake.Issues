namespace Cake.Issues.EsLint.Tests.LogFileFormat
{
    using System.Linq;
    using Cake.Issues.EsLint.LogFileFormat;
    using Core.IO;
    using Shouldly;
    using Xunit;

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
                CheckIssue(
                    issues[0],
                    @"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js",
                    1,
                    "no-unused-vars",
                    "http://eslint.org/docs/rules/no-unused-vars",
                    400,
                    "Error",
                    "'addOne' is defined but never used.");
                CheckIssue(
                    issues[1],
                    @"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js",
                    2,
                    "use-isnan",
                    "http://eslint.org/docs/rules/use-isnan",
                    400,
                    "Error",
                    "Use the isNaN function to compare with NaN.");
                CheckIssue(
                    issues[2],
                    @"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js",
                    3,
                    "space-unary-ops",
                    "http://eslint.org/docs/rules/space-unary-ops",
                    400,
                    "Error",
                    "Unexpected space before unary operator '++'.");
                CheckIssue(
                    issues[3],
                    @"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js",
                    3,
                    "semi",
                    "http://eslint.org/docs/rules/semi",
                    300,
                    "Warning",
                    "Missing semicolon.");
                CheckIssue(
                    issues[4],
                    @"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js",
                    4,
                    "no-else-return",
                    "http://eslint.org/docs/rules/no-else-return",
                    300,
                    "Warning",
                    "Unnecessary 'else' after 'return'.");
                CheckIssue(
                    issues[5],
                    @"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js",
                    5,
                    "indent",
                    "http://eslint.org/docs/rules/indent",
                    300,
                    "Warning",
                    "Expected indentation of 8 spaces but found 6.");
                CheckIssue(
                    issues[6],
                    @"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js",
                    5,
                    "consistent-return",
                    "http://eslint.org/docs/rules/consistent-return",
                    400,
                    "Error",
                    "Function 'addOne' expected a return value.");
                CheckIssue(
                    issues[7],
                    @"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js",
                    5,
                    "semi",
                    "http://eslint.org/docs/rules/semi",
                    300,
                    "Warning",
                    "Missing semicolon.");
                CheckIssue(
                    issues[8],
                    @"src\Cake.Issues.EsLint.Tests\Testfiles\fullOfProblems.js",
                    7,
                    "no-extra-semi",
                    "http://eslint.org/docs/rules/no-extra-semi",
                    400,
                    "Error",
                    "Unnecessary semicolon.");
            }

            private static void CheckIssue(
                IIssue issue,
                string affectedFileRelativePath,
                int? line,
                string rule,
                string ruleUrl,
                int priority,
                string priorityName,
                string message)
            {
                issue.ProviderType.ShouldBe("Cake.Issues.EsLint.EsLintIssuesProvider");
                issue.ProviderName.ShouldBe("ESLint");

                if (issue.AffectedFileRelativePath == null)
                {
                    affectedFileRelativePath.ShouldBeNull();
                }
                else
                {
                    issue.AffectedFileRelativePath.ToString().ShouldBe(new FilePath(affectedFileRelativePath).ToString());
                    issue.AffectedFileRelativePath.IsRelative.ShouldBe(true, "Issue path is not relative");
                }

                issue.Line.ShouldBe(line);
                issue.Rule.ShouldBe(rule);

                if (issue.RuleUrl == null)
                {
                    ruleUrl.ShouldBeNull();
                }
                else
                {
                    issue.RuleUrl.ToString().ShouldBe(ruleUrl);
                }

                issue.Priority.ShouldBe(priority);
                issue.PriorityName.ShouldBe(priorityName);
                issue.Message.ShouldBe(message);
            }
        }
    }
}
