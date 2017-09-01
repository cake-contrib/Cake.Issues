namespace Cake.Issues.MsBuild.Tests
{
    using System.Linq;
    using Core.IO;
    using IssueProvider;
    using Shouldly;
    using Testing;
    using Xunit;

    public class XmlFileLoggerFormatTests
    {
        public sealed class TheXmlFileLoggerFormatCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new XmlFileLoggerFormat(null));

                // Then
                result.IsArgumentNullException("log");
            }
        }

        public sealed class TheReadIssuesMethod
        {
            [Fact]
            public void Should_Read_Issue_With_File_Correct()
            {
                // Given
                var fixture = new MsBuildIssuesProviderFixture("IssueWithFile.xml");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                var issue = issues.Single();
                CheckIssue(
                    issue,
                    @"src\Cake.Issues.MsBuild.Tests\MsBuildIssuesProviderTests.cs",
                    1311,
                    "CA2201",
                    0,
                    @"Microsoft.Usage : 'ConfigurationManager.GetSortedConfigFiles(String)' creates an exception of type 'ApplicationException', an exception type that is not sufficiently specific and should never be raised by user code. If this exception instance might be thrown, use a different exception type.");
            }

            [Fact]
            public void Should_Read_Issue_With_File_Without_Path_Correct()
            {
                // Given
                var fixture = new MsBuildIssuesProviderFixture("IssueWithOnlyFileName.xml");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                var issue = issues.Single();
                CheckIssue(
                    issue,
                    @"src\Cake.Issues.MsBuild.Tests\MsBuildIssuesProviderTests.cs",
                    13,
                    "CS0219",
                    0,
                    "The variable 'foo' is assigned but its value is never used");
            }

            [Fact]
            public void Should_Read_Issue_Without_File_Correct()
            {
                // Given
                var fixture = new MsBuildIssuesProviderFixture("IssueWithoutFile.xml");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                var issue = issues.Single();
                CheckIssue(
                    issue,
                    null,
                    null,
                    "CA1711",
                    0,
                    "Microsoft.Naming : Rename type name 'UniqueQueue(Of T)' so that it does not end in 'Queue'.");
            }

            private static void CheckIssue(
                IIssue issue,
                string affectedFileRelativePath,
                int? line,
                string rule,
                int priority,
                string message)
            {
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
                issue.Priority.ShouldBe(priority);
                issue.Message.ShouldBe(message);
            }
        }
    }
}
