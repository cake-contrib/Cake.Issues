namespace Cake.Issues.PullRequests.Tests;

using System.Collections.Generic;
using System.Linq;
using Shouldly;

public static class ExceptionAssertExtensions
{
    public static void IsPullRequestIssuesException(this Exception exception, string message)
    {
        var ex = Assert.IsType<PullRequestIssuesException>(exception);
        Assert.Equal(message, ex.Message);
    }
}

/// <summary>
/// Extensions to help with issue comparisons in tests after making IIssue immutable.
/// Since issues are now cloned with enhanced properties, we need to compare by content rather than reference.
/// </summary>
public static class IssueAssertExtensions
{
    /// <summary>
    /// Verifies that the collection contains an issue with the same identifier as the expected issue.
    /// This is needed because IIssue is now immutable and IssuesReader returns new objects.
    /// </summary>
    public static void ShouldContainIssueWithSameIdentifier(this IEnumerable<IIssue> actualIssues, IIssue expectedIssue)
    {
        actualIssues.Any(x => x.Identifier == expectedIssue.Identifier)
            .ShouldBeTrue($"Expected to find issue with identifier '{expectedIssue.Identifier}' but collection only contained: [{string.Join(", ", actualIssues.Select(x => $"'{x.Identifier}'"))}]");
    }
}
