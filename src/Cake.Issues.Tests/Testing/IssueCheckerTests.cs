namespace Cake.Issues.Tests.Testing
{
    using System;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class IssueCheckerTests
    {
        public sealed class TheCheckMethodComparingTwoIssues
        {
            [Fact]
            public void Should_Throw_If_IssueToCheck_Is_Null()
            {
#pragma warning disable SA1123 // Do not place regions within elements
                #region DupFinder Exclusion
#pragma warning restore SA1123 // Do not place regions within elements

                // Given
                var fixture = new IssueBuilderFixture();
                IIssue issueToCheck = null;
                var expectedIssue = fixture.IssueBuilder.Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(issueToCheck, expectedIssue));

                // Then
                result.IsArgumentNullException("issueToCheck");

                #endregion
            }

            [Fact]
            public void Should_Throw_If_ExpectedIssue_Is_Null()
            {
#pragma warning disable SA1123 // Do not place regions within elements
                #region DupFinder Exclusion
#pragma warning restore SA1123 // Do not place regions within elements

                // Given
                var fixture = new IssueBuilderFixture();
                var issueToCheck = fixture.IssueBuilder.Create();
                IIssue expectedIssue = null;

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(issueToCheck, expectedIssue));

                // Then
                result.IsArgumentNullException("expectedIssue");

                #endregion
            }

            [Fact]
            public void Should_Not_Throw_If_Issues_Are_Identical()
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issueToCheck = fixture.IssueBuilder.Create();
                var expectedIssue = fixture.IssueBuilder.Create();

                // When
                IssueChecker.Check(issueToCheck, expectedIssue);

                // Then
            }
        }

        public sealed class TheCheckMethodWithIndividualValues
        {
            [Fact]
            public void Should_Throw_If_Issue_Is_Null()
            {
                // Given
                var fixture = new IssueCheckerFixture();
                IIssue issue = null;

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.Message,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleUrl));

                // Then
                result.IsArgumentNullException("issue");
            }

            [Fact]
            public void Should_Not_Throw_If_All_Values_Are_The_Same()
            {
                // Given
                var fixture = new IssueCheckerFixture();

                // When
                IssueChecker.Check(
                    fixture.Issue,
                    fixture.ProviderType,
                    fixture.ProviderName,
                    fixture.ProjectFileRelativePath,
                    fixture.ProjectName,
                    fixture.AffectedFileRelativePath,
                    fixture.Line,
                    fixture.Message,
                    fixture.Priority,
                    fixture.PriorityName,
                    fixture.Rule,
                    fixture.RuleUrl);

                // Then
            }

            [Theory]
            [InlineData("ProviderType", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_ProviderType_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture("Message", actualValue, "ProviderName");

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        fixture.Issue,
                        expectedValue,
                        fixture.ProviderName,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.Message,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleUrl));

                // Then
                result.ShouldBeOfType<Exception>();
                result.Message.ShouldStartWith("Expected issue.ProviderType");
            }

            [Theory]
            [InlineData("ProviderName", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_ProviderName_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture("Message", "ProviderType", actualValue);

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        fixture.Issue,
                        fixture.ProviderType,
                        expectedValue,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.Message,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleUrl));

                // Then
                result.ShouldBeOfType<Exception>();
                result.Message.ShouldStartWith("Expected issue.ProviderName");
            }

            [Theory]
            [InlineData(@"src\project.file", @"src\foo")]
            public void Should_Throw_If_ProjectFileRelativePath_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .InProjectFile(actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        expectedValue,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.Message,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleUrl));

                // Then
                result.ShouldBeOfType<Exception>();
                result.Message.ShouldStartWith("Expected issue.ProjectFileRelativePath");
            }

            [Theory]
            [InlineData("ProjectName", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_ProjectName_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .InProject(fixture.ProjectFileRelativePath, actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.ProjectFileRelativePath,
                        expectedValue,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.Message,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleUrl));

                // Then
                result.ShouldBeOfType<Exception>();
                result.Message.ShouldStartWith("Expected issue.ProjectName");
            }

            [Theory]
            [InlineData(@"src\source.file", @"src\foo")]
            public void Should_Throw_If_AffectedFileRelativePath_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .InFile(actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        expectedValue,
                        fixture.Line,
                        fixture.Message,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleUrl));

                // Then
                result.ShouldBeOfType<Exception>();
                result.Message.ShouldStartWith("Expected issue.AffectedFileRelativePath");
            }

            [Theory]
            [InlineData(42, 23)]
            [InlineData(null, 42)]
            [InlineData(42, null)]
            public void Should_Throw_If_Line_Is_Different(int? expectedValue, int? actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .InFile(fixture.AffectedFileRelativePath, actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        expectedValue,
                        fixture.Message,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleUrl));

                // Then
                result.ShouldBeOfType<Exception>();
                result.Message.ShouldStartWith("Expected issue.Line");
            }

            [Theory]
            [InlineData("Message", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_Message_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture(actualValue, "ProviderType", "ProviderName");

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        fixture.Issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        expectedValue,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleUrl));

                // Then
                result.ShouldBeOfType<Exception>();
                result.Message.ShouldStartWith("Expected issue.Message");
            }

            [Theory]
            [InlineData(IssuePriority.Error, IssuePriority.Warning)]
            public void Should_Throw_If_Priority_Is_Different(IssuePriority expectedValue, IssuePriority actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .WithPriority(actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.Message,
                        (int)expectedValue,
                        fixture.PriorityName,
                        fixture.Rule,
                        fixture.RuleUrl));

                // Then
                result.ShouldBeOfType<Exception>();
                result.Message.ShouldStartWith("Expected issue.Priority");
            }

            [Theory]
            [InlineData("PriorityName", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_PriorityName_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .WithPriority(fixture.Priority, actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.Message,
                        fixture.Priority,
                        expectedValue,
                        fixture.Rule,
                        fixture.RuleUrl));

                // Then
                result.ShouldBeOfType<Exception>();
                result.Message.ShouldStartWith("Expected issue.PriorityName");
            }

            [Theory]
            [InlineData("Rule", "Foo")]
            [InlineData(null, "Foo")]
            [InlineData("", "Foo")]
            [InlineData(" ", "Foo")]
            public void Should_Throw_If_Rule_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .OfRule(actualValue)
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.Message,
                        fixture.Priority,
                        fixture.PriorityName,
                        expectedValue,
                        fixture.RuleUrl));

                // Then
                result.ShouldBeOfType<Exception>();
                result.Message.ShouldStartWith("Expected issue.Rule");
            }

            [Theory]
            [InlineData("https://google.com", "https://foo.bar")]
            public void Should_Throw_If_RuleUrl_Is_Different(string expectedValue, string actualValue)
            {
                // Given
                var fixture = new IssueCheckerFixture();
                var issue =
                    fixture.IssueBuilder
                        .OfRule(fixture.Rule, new Uri(actualValue))
                        .Create();

                // When
                var result = Record.Exception(() =>
                    IssueChecker.Check(
                        issue,
                        fixture.ProviderType,
                        fixture.ProviderName,
                        fixture.ProjectFileRelativePath,
                        fixture.ProjectName,
                        fixture.AffectedFileRelativePath,
                        fixture.Line,
                        fixture.Message,
                        fixture.Priority,
                        fixture.PriorityName,
                        fixture.Rule,
                        new Uri(expectedValue)));

                // Then
                result.ShouldBeOfType<Exception>();
                result.Message.ShouldStartWith("Expected issue.RuleUrl");
            }
        }
    }
}