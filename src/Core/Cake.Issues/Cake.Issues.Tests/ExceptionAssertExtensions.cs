namespace Cake.Issues.Tests;

public static class ExceptionAssertExtensions
{
    public static void IsIssuesFoundException(this Exception exception, string message)
    {
        var ex = Assert.IsType<IssuesFoundException>(exception);
        Assert.Equal(message, ex.Message);
    }
}
