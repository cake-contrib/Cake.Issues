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
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, null));

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
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, null));

            // Then
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Fact]
        public void Should_Not_Throw_If_No_Issues()
        {
            // Given
            var issues = new List<IIssue>();

            // When
            BuildBreaker.BreakBuildOnIssues(issues, null);

            // Then
        }

        [Fact]
        public void Should_Call_Handler_If_Any_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            IEnumerable<IIssue> issuesPassedToHandler = null;
            void handler(IEnumerable<IIssue> x) => issuesPassedToHandler = x;

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, handler));

            // Then
            issuesPassedToHandler.ShouldNotBeNull().ShouldContain(issues.Single());
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Fact]
        public void Should_Not_Call_Handler_If_No_Issues()
        {
            // Given
            var issues = new List<IIssue>();
            IEnumerable<IIssue> issuesPassedToHandler = null;
            void handler(IEnumerable<IIssue> x) => issuesPassedToHandler = x;

            // When
            BuildBreaker.BreakBuildOnIssues(issues, handler);

            // Then
            issuesPassedToHandler.ShouldBeNull();
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
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, priority, null));

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
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, priority, null));

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
            BuildBreaker.BreakBuildOnIssues(issues, priority, null);

            // Then
        }

        [Fact]
        public void Should_Call_Handler_If_Any_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var priority = IssuePriority.Warning;
            IEnumerable<IIssue> issuesPassedToHandler = null;
            void handler(IEnumerable<IIssue> x) => issuesPassedToHandler = x;

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, priority, handler));

            // Then
            issuesPassedToHandler.ShouldNotBeNull().ShouldContain(issues.Single());
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Fact]
        public void Should_Not_Call_Handler_If_No_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var priority = IssuePriority.Error;
            IEnumerable<IIssue> issuesPassedToHandler = null;
            void handler(IEnumerable<IIssue> x) => issuesPassedToHandler = x;

            // When
            BuildBreaker.BreakBuildOnIssues(issues, priority, handler);

            // Then
            issuesPassedToHandler.ShouldBeNull();
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
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, providerType, null));

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
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, providerType, null));

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
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, providerType, null));

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
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, providerType, null));

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
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, providerType, null));

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
            BuildBreaker.BreakBuildOnIssues(issues, providerType, null);

            // Then
        }

        [Fact]
        public void Should_Call_Handler_If_Any_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            var providerType = "ProviderType Foo";
            IEnumerable<IIssue> issuesPassedToHandler = null;
            void handler(IEnumerable<IIssue> x) => issuesPassedToHandler = x;

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, providerType, handler));

            // Then
            issuesPassedToHandler.ShouldNotBeNull().ShouldContain(issues.Single());
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Fact]
        public void Should_Not_Call_Handler_If_No_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var providerType = "ProviderType Bar";
            IEnumerable<IIssue> issuesPassedToHandler = null;
            void handler(IEnumerable<IIssue> x) => issuesPassedToHandler = x;

            // When
            BuildBreaker.BreakBuildOnIssues(issues, providerType, handler);

            // Then
            issuesPassedToHandler.ShouldBeNull();
        }
    }

    public sealed class TheBreakBuildOnIssuesMethodWithPriorityAndProviderTypeParameters
    {
        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            IEnumerable<IIssue> issues = null;
            var minimumPriority = IssuePriority.Undefined;
            IList<string> issueProvidersToConsider = [];
            IList<string> issueProvidersToIgnore = [];

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(
                issues,
                minimumPriority,
                issueProvidersToConsider,
                issueProvidersToIgnore,
                null));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Throw_If_IssueProvidersToConsider_Is_Null()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            var minimumPriority = IssuePriority.Undefined;
            IList<string> issueProvidersToConsider = null;
            IList<string> issueProvidersToIgnore = [];

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(
                issues,
                minimumPriority,
                issueProvidersToConsider,
                issueProvidersToIgnore,
                null));

            // Then
            result.IsArgumentNullException("issueProvidersToConsider");
        }

        [Fact]
        public void Should_Throw_If_IssueProvidersToIgnore_Is_Null()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Error)
                    .Create()];
            var minimumPriority = IssuePriority.Warning;
            IList<string> issueProvidersToConsider = [];
            IList<string> issueProvidersToIgnore = null;

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(
                issues,
                minimumPriority,
                issueProvidersToConsider,
                issueProvidersToIgnore,
                null));

            // Then
            result.IsArgumentNullException("issueProvidersToIgnore");
        }

        [Fact]
        public void Should_Throw_If_Any_Issue_And_No_Filter()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            var minimumPriority = IssuePriority.Undefined;
            IList<string> issueProvidersToConsider = [];
            IList<string> issueProvidersToIgnore = [];

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(
                issues,
                minimumPriority,
                issueProvidersToConsider,
                issueProvidersToIgnore,
                null));

            // Then
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Theory]
        [InlineData(IssuePriority.Undefined)]
        [InlineData(IssuePriority.Hint)]
        [InlineData(IssuePriority.Suggestion)]
        [InlineData(IssuePriority.Warning)]
        public void Should_Throw_If_Issue_Of_Relevant_Priority(IssuePriority priority)
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var minimumPriority = priority;
            IList<string> issueProvidersToConsider = [];
            IList<string> issueProvidersToIgnore = [];

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(
                issues,
                minimumPriority,
                issueProvidersToConsider,
                issueProvidersToIgnore,
                null));

            // Then
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Theory]
        [InlineData(IssuePriority.Error)]
        public void Should_Not_Throw_If_No_Issue_Of_Relevant_Priority(IssuePriority priority)
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var minimumPriority = priority;
            IList<string> issueProvidersToConsider = [];
            IList<string> issueProvidersToIgnore = [];

            // When
            BuildBreaker.BreakBuildOnIssues(
                issues,
                minimumPriority,
                issueProvidersToConsider,
                issueProvidersToIgnore,
                null);

            // Then
        }

        [Theory]
        [InlineData("ProviderType Foo")]
        [InlineData("ProviderType Foo,ProviderType Bar")]
        public void Should_Throw_If_IssueProvider_To_Consider(string value)
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            var minimumPriority = IssuePriority.Undefined;
            IList<string> issueProvidersToConsider = value.Split(',');
            IList<string> issueProvidersToIgnore = [];

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(
                issues,
                minimumPriority,
                issueProvidersToConsider,
                issueProvidersToIgnore,
                null));

            // Then
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Theory]
        [InlineData("ProviderType Bar")]
        public void Should_Not_Throw_If_No_Issue_Is_Not_In_List_Of_Issue_Providers_To_Consider(string value)
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var minimumPriority = IssuePriority.Undefined;
            IList<string> issueProvidersToConsider = value.Split(',');
            IList<string> issueProvidersToIgnore = [];

            // When
            BuildBreaker.BreakBuildOnIssues(
                issues,
                minimumPriority,
                issueProvidersToConsider,
                issueProvidersToIgnore,
                null);

            // Then
        }

        [Theory]
        [InlineData("ProviderType Bar")]
        public void Should_Throw_If_IssueProvider_Is_Not_Ignored(string value)
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            var minimumPriority = IssuePriority.Undefined;
            IList<string> issueProvidersToConsider = [];
            IList<string> issueProvidersToIgnore = value.Split(',');

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(
                issues,
                minimumPriority,
                issueProvidersToConsider,
                issueProvidersToIgnore,
                null));

            // Then
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Theory]
        [InlineData("ProviderType Foo")]
        [InlineData("ProviderType Foo,ProviderType Bar")]
        public void Should_Not_Throw_If_IssueProvider_Is_Ignored(string value)
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var minimumPriority = IssuePriority.Undefined;
            IList<string> issueProvidersToConsider = [];
            IList<string> issueProvidersToIgnore = value.Split(',');

            // When
            BuildBreaker.BreakBuildOnIssues(
                issues,
                minimumPriority,
                issueProvidersToConsider,
                issueProvidersToIgnore,
                null);

            // Then
        }

        [Fact]
        public void Should_Not_Throw_If_IssueProvider_Is_To_Consider_And_Ignored()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            var minimumPriority = IssuePriority.Undefined;
            IList<string> issueProvidersToConsider = ["ProviderType Foo"];
            IList<string> issueProvidersToIgnore = ["ProviderType Foo"];

            // When
            BuildBreaker.BreakBuildOnIssues(
                issues,
                minimumPriority,
                issueProvidersToConsider,
                issueProvidersToIgnore,
                null);

            // Then
        }

        [Fact]
        public void Should_Not_Throw_If_Priority_Matches_But_IssueProvider_Is_Ignored()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var minimumPriority = IssuePriority.Warning;
            IList<string> issueProvidersToConsider = [];
            IList<string> issueProvidersToIgnore = ["ProviderType Foo"];

            // When
            BuildBreaker.BreakBuildOnIssues(
                issues,
                minimumPriority,
                issueProvidersToConsider,
                issueProvidersToIgnore,
                null);

            // Then
        }

        [Fact]
        public void Should_Call_Handler_If_Any_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            var minimumPriority = IssuePriority.Undefined;
            IList<string> issueProvidersToConsider = [];
            IList<string> issueProvidersToIgnore = [];
            IEnumerable<IIssue> issuesPassedToHandler = null;
            void handler(IEnumerable<IIssue> x) => issuesPassedToHandler = x;

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(
                issues,
                minimumPriority,
                issueProvidersToConsider,
                issueProvidersToIgnore,
                handler));

            // Then
            issuesPassedToHandler.ShouldNotBeNull().ShouldContain(issues.Single());
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Fact]
        public void Should_Not_Call_Handler_If_No_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var providerType = "ProviderType Bar";
            IEnumerable<IIssue> issuesPassedToHandler = null;
            void handler(IEnumerable<IIssue> x) => issuesPassedToHandler = x;

            // When
            BuildBreaker.BreakBuildOnIssues(issues, providerType, handler);

            // Then
            issuesPassedToHandler.ShouldBeNull();
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
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, predicate, null));

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
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, predicate, null));

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
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, predicate, null));

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
            BuildBreaker.BreakBuildOnIssues(issues, predicate, null);

            // Then
        }

        [Fact]
        public void Should_Call_Handler_If_Any_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            static bool predicate(IIssue x) => x.MessageText == "Message Foo";
            IEnumerable<IIssue> issuesPassedToHandler = null;
            void handler(IEnumerable<IIssue> x) => issuesPassedToHandler = x;

            // When
            var result = Record.Exception(() => BuildBreaker.BreakBuildOnIssues(issues, predicate, handler));

            // Then
            issuesPassedToHandler.ShouldNotBeNull().ShouldContain(issues.Single());
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Fact]
        public void Should_Not_Call_Handler_If_No_Issues()
        {
            // Given
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            static bool predicate(IIssue x) => false;
            IEnumerable<IIssue> issuesPassedToHandler = null;
            void handler(IEnumerable<IIssue> x) => issuesPassedToHandler = x;

            // When
            BuildBreaker.BreakBuildOnIssues(issues, predicate, handler);

            // Then
            issuesPassedToHandler.ShouldBeNull();
        }
    }
}
