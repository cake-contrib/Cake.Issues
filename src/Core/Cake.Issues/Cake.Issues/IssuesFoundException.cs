namespace Cake.Issues;

using System;

/// <summary>
/// Exception thrown when issues are found and build should fail.
/// </summary>
[Serializable]
public class IssuesFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IssuesFoundException"/> class.
    /// </summary>
    public IssuesFoundException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IssuesFoundException"/> class for a list of issues.
    /// </summary>
    /// <param name="issueCount">Number of issues which are found.</param>
    public IssuesFoundException(int issueCount)
        : base(issueCount == 1 ? "Found 1 issue." : $"Found {issueCount} issues.")
    {
    }
}

