namespace Cake.Issues.Tap.Tests;

using Cake.Core.Diagnostics;
using Cake.Issues.Tap.LogFileFormat;

public sealed class TapIssuesProviderTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given
            ICakeLog log = null;

            // When
            var result = Record.Exception(() =>
                new TapIssuesProvider(
                    log,
                    new TapIssuesSettings("Foo".ToByteArray(), new GenericLogFileFormat(new FakeLog()))));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Throw_If_IssueProviderSettings_Are_Null()
        {
            var result = Record.Exception(() =>
                new TapIssuesProvider(
                    new FakeLog(),
                    null));

            // Then
            result.IsArgumentNullException("issueProviderSettings");
        }
    }

    //public sealed class TheReadIssuesMethod
    //{
    //    [Fact]
    //    public void Should_Read_Issue_Correct_For_Specification_File()
    //    {
    //        // Given
    //        var fixture = new TapIssuesProviderFixture("specification.tap");

    //        // When
    //        var issues = fixture.ReadIssues().ToList();

    //        // Then
    //        issues.Count.ShouldBe(2);
    //    }

    //    [Fact]
    //    public void Should_Read_Issue_Correct_For_Stylelint_File_Without_Warnings()
    //    {
    //        // Given
    //        var fixture = new TapIssuesProviderFixture("stylelint-no-warnings.tap");

    //        // When
    //        var issues = fixture.ReadIssues().ToList();

    //        // Then
    //        issues.Count.ShouldBe(0);
    //    }

    //    [Fact]
    //    public void Should_Read_Issue_Correct_For_Stylelint_File_With_Warnings()
    //    {
    //        // Given
    //        var fixture = new TapIssuesProviderFixture("stylelint-warnings.tap");

    //        // When
    //        var issues = fixture.ReadIssues().ToList();

    //        // Then
    //        issues.Count.ShouldBe(2);

    //        var issue = issues[0];
    //        IssueChecker.Check(
    //            issue,
    //            IssueBuilder.NewIssue(
    //                "Message Foo.",
    //                "Cake.Issues.Tap.TapIssuesProvider",
    //                "TAP")
    //                .InFile("path/to/file1.css")
    //                //.OfRule("Rule Foo")
    //                //.WithPriority(IssuePriority.Error)
    //                .Create());

    //        issue = issues[1];
    //        IssueChecker.Check(
    //            issue,
    //            IssueBuilder.NewIssue(
    //                "Message Foo.",
    //                "Cake.Issues.Tap.TapIssuesProvider",
    //                "TAP")
    //                .InFile("path/to/file2.css")
    //                //.OfRule("Rule Foo")
    //                //.WithPriority(IssuePriority.Error)
    //                .Create());
    //    }

    //    [Fact]
    //    public void Should_Read_Issue_Correct_For_Stylelint_File_With_Parse_Error()
    //    {
    //        // Given
    //        var fixture = new TapIssuesProviderFixture("stylelint-parse-error.tap");

    //        // When
    //        var issues = fixture.ReadIssues().ToList();

    //        // Then
    //        issues.Count.ShouldBe(1);
    //    }
    //}
}
