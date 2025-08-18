namespace Cake.Issues.Tests;

using System;
using Cake.Core;

public sealed class Aliases
{
    public sealed class BreakBuildOnIssuesAliasWithIssuesParameter
    {
        [Fact]
        public void Should_Throw_If_Context_Is_Null()
        {
            // Given
            ICakeContext context = null;
            IEnumerable<IIssue> issues = [];

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues));

            // Then
            result.IsArgumentNullException("context");
        }

        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = null;

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Throw_If_Any_Issues()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues));

            // Then
            result.IsIssuesFoundException("Found 1 issue.");
        }
    }

    public sealed class BreakBuildOnIssuesAliasWithIssuesAndHandlerParameter
    {
        [Fact]
        public void Should_Throw_If_Context_Is_Null()
        {
            // Given
            ICakeContext context = null;
            IEnumerable<IIssue> issues = [];
            static void handler(IEnumerable<IIssue> issue) { }

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, handler));

            // Then
            result.IsArgumentNullException("context");
        }

        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = null;
            static void handler(IEnumerable<IIssue> issue) { }

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, handler));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Not_Throw_If_Handler_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [];
            Action<IEnumerable<IIssue>> handler = null;

            // When
            context.BreakBuildOnIssues(issues, handler);

            // Then
        }

        [Fact]
        public void Should_Call_Handler_If_Any_Issues()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            IEnumerable<IIssue> issuesPassedToHandler = null;
            void handler(IEnumerable<IIssue> x) => issuesPassedToHandler = x;

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, handler));

            // Then
            issuesPassedToHandler.ShouldNotBeNull().ShouldContain(issues.Single());
            result.IsIssuesFoundException("Found 1 issue.");
        }
    }

    public sealed class BreakBuildOnIssuesAliasWithIssuesAndPriorityParameter
    {
        [Fact]
        public void Should_Throw_If_Context_Is_Null()
        {
            // Given
            ICakeContext context = null;
            IEnumerable<IIssue> issues = [];
            var priority = IssuePriority.Error;

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, priority));

            // Then
            result.IsArgumentNullException("context");
        }

        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = null;
            var priority = IssuePriority.Error;

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, priority));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Throw_If_Matching_Issues()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var priority = IssuePriority.Warning;

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, priority));

            // Then
            result.IsIssuesFoundException("Found 1 issue.");
        }
    }

    public sealed class BreakBuildOnIssuesAliasWithIssuesPriorityAndHandlerParameter
    {
        [Fact]
        public void Should_Throw_If_Context_Is_Null()
        {
            // Given
            ICakeContext context = null;
            IEnumerable<IIssue> issues = [];
            var priority = IssuePriority.Error;
            static void handler(IEnumerable<IIssue> issue) { }

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, priority, handler));

            // Then
            result.IsArgumentNullException("context");
        }

        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = null;
            var priority = IssuePriority.Error;
            static void handler(IEnumerable<IIssue> issue) { }

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, priority, handler));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Not_Throw_If_Handler_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [];
            var priority = IssuePriority.Error;
            Action<IEnumerable<IIssue>> handler = null;

            // When
            context.BreakBuildOnIssues(issues, priority, handler);

            // Then
        }

        [Fact]
        public void Should_Call_Handler_If_Any_Issues()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var priority = IssuePriority.Warning;
            IEnumerable<IIssue> issuesPassedToHandler = null;
            void handler(IEnumerable<IIssue> x) => issuesPassedToHandler = x;

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, priority, handler));

            // Then
            issuesPassedToHandler.ShouldNotBeNull().ShouldContain(issues.Single());
            result.IsIssuesFoundException("Found 1 issue.");
        }
    }

    public sealed class BreakBuildOnIssuesAliasWithIssuesAndProviderTypeParameter
    {
        [Fact]
        public void Should_Throw_If_Context_Is_Null()
        {
            // Given
            ICakeContext context = null;
            IEnumerable<IIssue> issues = [];
            var providerType = "ProviderType Foo";

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, providerType));

            // Then
            result.IsArgumentNullException("context");
        }

        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = null;
            var providerType = "ProviderType Foo";

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, providerType));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Throw_If_ProviderType_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [];
            string providerType = null;

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, providerType));

            // Then
            result.IsArgumentNullException("providerType");
        }

        [Fact]
        public void Should_Throw_If_ProviderType_Is_Empty()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [];
            var providerType = string.Empty;

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, providerType));

            // Then
            result.IsArgumentOutOfRangeException("providerType");
        }

        [Fact]
        public void Should_Throw_If_ProviderType_Is_Whitespace()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [];
            var providerType = " ";

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, providerType));

            // Then
            result.IsArgumentOutOfRangeException("providerType");
        }

        [Fact]
        public void Should_Throw_If_Matching_Issues()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            var providerType = "ProviderType Foo";

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, providerType));

            // Then
            result.IsIssuesFoundException("Found 1 issue.");
        }
    }

    public sealed class BreakBuildOnIssuesAliasWithIssuesProviderTypeAndHandlerParameter
    {
        [Fact]
        public void Should_Throw_If_Context_Is_Null()
        {
            // Given
            ICakeContext context = null;
            IEnumerable<IIssue> issues = [];
            var providerType = "ProviderType Foo";
            static void handler(IEnumerable<IIssue> issue) { }

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, providerType, handler));

            // Then
            result.IsArgumentNullException("context");
        }

        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = null;
            var providerType = "ProviderType Foo";
            static void handler(IEnumerable<IIssue> issue) { }

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, providerType, handler));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Throw_If_ProviderType_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [];
            string providerType = null;
            static void handler(IEnumerable<IIssue> issue) { }

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, providerType, handler));

            // Then
            result.IsArgumentNullException("providerType");
        }

        [Fact]
        public void Should_Throw_If_ProvderType_Is_Empty()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [];
            var providerType = string.Empty;
            static void handler(IEnumerable<IIssue> issue) { }

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, providerType, handler));

            // Then
            result.IsArgumentOutOfRangeException("providerType");
        }

        [Fact]
        public void Should_Throw_If_ProvderType_Is_Whitespace()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [];
            var providerType = " ";
            static void handler(IEnumerable<IIssue> issue) { }

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, providerType, handler));

            // Then
            result.IsArgumentOutOfRangeException("providerType");
        }

        [Fact]
        public void Should_Not_Throw_If_Handler_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [];
            var providerType = "ProviderType Foo";
            Action<IEnumerable<IIssue>> handler = null;

            // When
            context.BreakBuildOnIssues(issues, providerType, handler);

            // Then
        }

        [Fact]
        public void Should_Call_Handler_If_Any_Issues()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            var providerType = "ProviderType Foo";
            IEnumerable<IIssue> issuesPassedToHandler = null;
            void handler(IEnumerable<IIssue> x) => issuesPassedToHandler = x;

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, providerType, handler));

            // Then
            issuesPassedToHandler.ShouldNotBeNull().ShouldContain(issues.Single());
            result.IsIssuesFoundException("Found 1 issue.");
        }
    }

    public sealed class BreakBuildOnIssuesAliasWithIssuesAndSettingsParameter
    {
        [Fact]
        public void Should_Throw_If_Context_Is_Null()
        {
            // Given
            ICakeContext context = null;
            IEnumerable<IIssue> issues = [];
            var settings = new BuildBreakingSettings();

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, settings));

            // Then
            result.IsArgumentNullException("context");
        }

        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = null;
            var settings = new BuildBreakingSettings();

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, settings));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Throw_If_Settings_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [];
            BuildBreakingSettings settings = null;

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, settings));

            // Then
            result.IsArgumentNullException("settings");
        }

        [Theory]
        [InlineData(IssuePriority.Undefined)]
        [InlineData(IssuePriority.Hint)]
        [InlineData(IssuePriority.Suggestion)]
        [InlineData(IssuePriority.Warning)]
        public void Should_Throw_If_Issue_Of_Relevant_Priority(IssuePriority priority)
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var settings =
                new BuildBreakingSettings
                {
                    MinimumPriority = priority,
                    IssueProvidersToConsider = [],
                    IssueProvidersToIgnore = []
                };

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, settings));

            // Then
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Theory]
        [InlineData("ProviderType Foo")]
        [InlineData("ProviderType Foo,ProviderType Bar")]
        public void Should_Throw_If_IssueProvider_To_Consider(string value)
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            var settings =
                new BuildBreakingSettings
                {
                    MinimumPriority = IssuePriority.Undefined,
                    IssueProvidersToConsider = value.Split(','),
                    IssueProvidersToIgnore = []
                };

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, settings));

            // Then
            result.IsIssuesFoundException("Found 1 issue.");
        }

        [Theory]
        [InlineData("ProviderType Foo")]
        [InlineData("ProviderType Foo,ProviderType Bar")]
        public void Should_Not_Throw_If_IssueProvider_Is_Ignored(string value)
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .WithPriority(IssuePriority.Warning)
                    .Create()];
            var settings =
                new BuildBreakingSettings
                {
                    MinimumPriority = IssuePriority.Undefined,
                    IssueProvidersToConsider = [],
                    IssueProvidersToIgnore = value.Split(',')
                };

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, settings));

            // Then
        }
    }

    public sealed class BreakBuildOnIssuesAliasWithIssuesSettingsAndHandlerParameter
    {
        [Fact]
        public void Should_Throw_If_Context_Is_Null()
        {
            // Given
            ICakeContext context = null;
            IEnumerable<IIssue> issues = [];
            var settings = new BuildBreakingSettings();
            static void handler(IEnumerable<IIssue> issue) { }

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, settings, handler));

            // Then
            result.IsArgumentNullException("context");
        }

        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = null;
            var settings = new BuildBreakingSettings();
            static void handler(IEnumerable<IIssue> issue) { }

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, settings, handler));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Throw_If_Settings_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [];
            BuildBreakingSettings settings = null;
            static void handler(IEnumerable<IIssue> issue) { }

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, settings, handler));

            // Then
            result.IsArgumentNullException("settings");
        }

        [Fact]
        public void Should_Call_Handler_If_Any_Issues()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            var settings =
                new BuildBreakingSettings
                {
                    MinimumPriority = IssuePriority.Undefined,
                    IssueProvidersToConsider = [],
                    IssueProvidersToIgnore = []
                };
            IEnumerable<IIssue> issuesPassedToHandler = null;
            void handler(IEnumerable<IIssue> x) => issuesPassedToHandler = x;

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, settings, handler));

            // Then
            issuesPassedToHandler.ShouldNotBeNull().ShouldContain(issues.Single());
            result.IsIssuesFoundException("Found 1 issue.");
        }
    }

    public sealed class BreakBuildOnIssuesAliasWithIssuesAndPredicateParameter
    {
        [Fact]
        public void Should_Throw_If_Context_Is_Null()
        {
            // Given
            ICakeContext context = null;
            IEnumerable<IIssue> issues = [];
            static bool predicate(IIssue issue) => true;

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, predicate));

            // Then
            result.IsArgumentNullException("context");
        }

        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = null;
            static bool predicate(IIssue issue) => true;

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, predicate));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Throw_If_Predicate_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [];
            Func<IIssue, bool> predicate = null;

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, predicate));

            // Then
            result.IsArgumentNullException("predicate");
        }

        [Fact]
        public void Should_Throw_If_Matching_Issues()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            static bool predicate(IIssue x) => x.MessageText == "Message Foo";

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, predicate));

            // Then
            result.IsIssuesFoundException("Found 1 issue.");
        }
    }

    public sealed class BreakBuildOnIssuesAliasWithIssuesPredicateAndHandlerParameter
    {
        [Fact]
        public void Should_Throw_If_Context_Is_Null()
        {
            // Given
            ICakeContext context = null;
            IEnumerable<IIssue> issues = [];
            static bool predicate(IIssue issue) => true;
            static void handler(IEnumerable<IIssue> issue) { }

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, predicate, handler));

            // Then
            result.IsArgumentNullException("context");
        }

        [Fact]
        public void Should_Throw_If_Issues_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = null;
            static bool predicate(IIssue issue) => true;
            static void handler(IEnumerable<IIssue> issue) { }

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, predicate, handler));

            // Then
            result.IsArgumentNullException("issues");
        }

        [Fact]
        public void Should_Throw_If_Predicate_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [];
            Func<IIssue, bool> predicate = null;
            static void handler(IEnumerable<IIssue> issue) { }

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, predicate, handler));

            // Then
            result.IsArgumentNullException("predicate");
        }

        [Fact]
        public void Should_Not_Throw_If_Handler_Is_Null()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [];
            static bool predicate(IIssue issue) => true;
            Action<IEnumerable<IIssue>> handler = null;

            // When
            context.BreakBuildOnIssues(issues, predicate, handler);

            // Then
        }

        [Fact]
        public void Should_Call_Handler_If_Any_Issues()
        {
            // Given
            var fixture = new CakeContextFixture();
            var context = fixture.CreateContext();
            IEnumerable<IIssue> issues = [
                IssueBuilder
                    .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                    .Create()];
            static bool predicate(IIssue x) => x.MessageText == "Message Foo";
            IEnumerable<IIssue> issuesPassedToHandler = null;
            void handler(IEnumerable<IIssue> x) => issuesPassedToHandler = x;

            // When
            var result = Record.Exception(() => context.BreakBuildOnIssues(issues, predicate, handler));

            // Then
            issuesPassedToHandler.ShouldNotBeNull().ShouldContain(issues.Single());
            result.IsIssuesFoundException("Found 1 issue.");
        }
    }
}