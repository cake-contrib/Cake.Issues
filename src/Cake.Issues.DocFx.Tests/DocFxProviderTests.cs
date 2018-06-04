namespace Cake.Issues.DocFx.Tests
{
    using System.Linq;
    using Cake.Testing;
    using Core.IO;
    using Shouldly;
    using Testing;
    using Xunit;

    public sealed class DocFxProviderTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() =>
                    new DocFxIssuesProvider(
                        null,
                        DocFxIssuesSettings.FromContent("Foo", @"c:\Source\Cake.Issues")));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                var result = Record.Exception(() =>
                    new DocFxIssuesProvider(
                        new FakeLog(),
                        null));

                // Then
                result.IsArgumentNullException("settings");
            }
        }

        public sealed class TheReadIssuesMethod
        {
            [Theory]
            [InlineData(@"c:\Source\Cake.Issues\docs", "docs/")]
            [InlineData(@"docs", "docs/")]
            [InlineData(@"c:\Source\Cake.Issues\src\docs", "src/docs/")]
            [InlineData(@"src\docs", "src/docs/")]
            [InlineData(@"c:\Source\Cake.Issues", "")]
            [InlineData(@"/", "")]
            public void Should_Read_Issue_Correct(string docRootPath, string docRelativePath)
            {
                // Given
                var fixture = new DocFxProviderFixture("docfx.json", docRootPath);

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                CheckIssue(
                    issues[0],
                    docRelativePath + @"index.md",
                    null,
                    "Build Document.LinkPhaseHandler.Apply Templates",
                    null,
                    300,
                    "Warning",
                    "Invalid cross reference \"[Foo](xref:foo)\".");
            }

            [Fact]
            public void Should_Read_Line_Correct()
            {
                // Given
                var fixture = new DocFxProviderFixture("entry-with-line.json", @"/");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                CheckIssue(
                    issues[0],
                    @"bar.md",
                    45,
                    "Build Document.LinkPhaseHandler.ConceptualDocumentProcessor.Save",
                    null,
                    300,
                    "Warning",
                    "Invalid file link:(~/foo.md).");
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
                issue.ProviderType.ShouldBe("Cake.Issues.DocFx.DocFxIssuesProvider");
                issue.ProviderName.ShouldBe("DocFX");

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
