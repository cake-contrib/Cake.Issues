public class BuildData
{
    private readonly List<IIssue> issues = new List<IIssue>();

    /// <summary>
    /// Gets issues determined during building.
    /// </summary>
    public IEnumerable<IIssue> Issues
    {
        get
        {
            return issues.AsReadOnly();
        }
    }

    /// <summary>
    /// Add issues to <see cref="Issues"/>.
    /// </summary>
    /// <param name="issues">List of issues which should be added.</param>
    public void AddIssues(IEnumerable<IIssue> issues)
    {
        if (issues == null)
        {
            throw new NullReferenceException(nameof(issues));
        }

        this.issues.AddRange(issues);
    }
}