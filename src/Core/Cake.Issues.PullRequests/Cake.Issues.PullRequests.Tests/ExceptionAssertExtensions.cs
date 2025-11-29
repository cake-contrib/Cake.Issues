namespace Cake.Issues.PullRequests.Tests;

public static class ExceptionAssertExtensions
{
    public static void IsPullRequestIssuesException(this Exception exception, string message)
    {
        var ex = Assert.IsType<PullRequestIssuesException>(exception);
        Assert.Equal(message, ex.Message);
    }
}
