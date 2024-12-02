namespace Cake.Issues.Tests;

using System.Collections.Generic;

public class BuildBreakerTests
{
    public sealed class TheBreakBuildOnIssuesMethod
    {
        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            IEnumerable<IIssue> issues = null;

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Throw_If_Any_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues));

            // Then
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Fact]
        public void Should_Not_Throw_If_No_Issues()
        {
            // Given
            var issues = new List<IIssue>();

            // When
            BuildBreaker.BreakBuildOnIssues(issues);

            // Then
        }
    }

    public sealed class TheBreakBuildOnIssuesMethodWithPriorityParameter
    {
        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            IEnumerable<IIssue> issues = null;
            var priority = IssuePriority.Error;

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, priority));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Throw_If_Matching_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var priority = IssuePriority.Warning;

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, priority));

            // Then
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Fact]
        public void Should_Not_Throw_If_No_Matching_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var priority = IssuePriority.Error;

            // When
            BuildBreaker.BreakBuildOnIssues(issues, priority);

            // Then
        }
    }

    public sealed class TheBreakBuildOnIssuesMethodWithProviderTypeParameter
    {
        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            IEnumerable<IIssue> issues = null;
            var providerType = "ProviderType Foo";

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, providerType));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Throw_If_ProviderType_Is_Null()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            string providerType = null;

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, providerType));

            // Then
            result.IsArgumentNullException("providerType");
        }

        [Fact]
        public void Should_Throw_If_ProviderType_Is_Empty()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            var providerType = string.Empty;

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, providerType));

            // Then
            result.IsArgumentOutOfRangeException("providerType");
        }

        [Fact]
        public void Should_Throw_If_ProviderType_Is_Whitespace()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            var providerType = " ";

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, providerType));

            // Then
            result.IsArgumentOutOfRangeException("providerType");
        }

        [Fact]
        public void Should_Throw_If_Matching_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            var providerType = "ProviderType Foo";

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, providerType));

            // Then
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Fact]
        public void Should_Not_Throw_If_No_Matching_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var providerType = "ProviderType Bar";

            // When
            BuildBreaker.BreakBuildOnIssues(issues, providerType);

            // Then
        }
    }

    public sealed class TheBreakBuildOnIssuesMethodWithPredicateParameter
    {
        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            IEnumerable<IIssue> issues = null;
            static bool predicate(IIssue x) => true;

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, predicate));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Throw_If_Predicate_Is_Null()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            Func<IIssue, bool> predicate = null;

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, predicate));

            // Then
            result.IsArgumentNullException("predicate");
        }

        [Fact]
        public void Should_Throw_If_Matching_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            static bool predicate(IIssue x) => x.MessageText == "Message Foo";

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, predicate));

            // Then
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Fact]
        public void Should_Not_Throw_If_No_Matching_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            static bool predicate(IIssue x) => false;

            // When
            BuildBreaker.BreakBuildOnIssues(issues, predicate);

            // Then
        }
    }
}
