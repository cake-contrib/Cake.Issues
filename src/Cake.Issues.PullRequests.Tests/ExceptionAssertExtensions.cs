namespace Cake.Issues.PullRequests.Tests
{
    using System;
    using Xunit;

    public static class ExceptionAssertExtensions
    {
        public static void IsPullRequestIssuesException(this Exception exception, string message)
        {
            Assert.IsType<PullRequestIssuesException>(exception);
            Assert.Equal(message, exception.Message);
        }
    }
}
