namespace Cake.Issues.MsBuild.Tests
{
    using System.Linq;
    using Core.IO;
    using Shouldly;
    using Testing;
    using Xunit;

    public sealed class XmlFileLoggerFormatTests
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
                    300,
                    "Warning",
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
                    300,
                    "Warning",
                    "The variable 'foo' is assigned but its value is never used");
            }

            [Fact]
            public void Should_Read_Issue_With_Line_Zero_Correct()
            {
                // Given
                var fixture = new MsBuildIssuesProviderFixture("IssueWithLineZero.xml");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                var issue = issues.Single();
                CheckIssue(
                    issue,
                    @"SHFB",
                    null,
                    "BE0006",
                    300,
                    "Warning",
                    @"Unable to locate any documentation sources for 'c:\Source\Cake.Prca\Cake.Prca..csproj' (Configuration: Debug Platform: AnyCPU)");
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
                    300,
                    "Warning",
                    "Microsoft.Naming : Rename type name 'UniqueQueue(Of T)' so that it does not end in 'Queue'.");
            }

            [Fact]
            public void Should_Read_Issue_Without_Code_Correct()
            {
                // Given
                var fixture = new MsBuildIssuesProviderFixture("IssueWithoutCode.xml");

                // When
                var issues = fixture.ReadIssues().ToList();

                // Then
                issues.Count.ShouldBe(1);
                var issue = issues.Single();
                CheckIssue(
                    issue,
                    @"src\Cake.Issues.MsBuild.Tests\MsBuildIssuesProviderTests.cs",
                    21,
                    null,
                    300,
                    "Warning",
                    @"SA1300 : CSharp.Naming : namespace names begin with an upper-case letter: foo.");
            }

            private static void CheckIssue(
                IIssue issue,
                string affectedFileRelativePath,
                int? line,
                string rule,
                int priority,
                string priorityName,
                string message)
            {
                issue.ProviderType.ShouldBe("Cake.Issues.MsBuild.MsBuildIssuesProvider");
                issue.ProviderName.ShouldBe("MSBuild");

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
                issue.PriorityName.ShouldBe(priorityName);
                issue.Message.ShouldBe(message);
            }
        }
    }
}
