/// <summary>
/// Concrete log format.
/// </summary>
/// <param name="log">The Cake log instance.</param>
internal class MyConcreteLogFileFormat(ICakeLog log) : MyLogFileFormat(log)
{
    /// <inheritdoc/>
    public override IEnumerable<IIssue> ReadIssues(
        MyIssuesProvider issueProvider,
        IRepositorySettings repositorySettings,
        MyIssuesSettings issueProviderSettings)
    {
        issueProvider.NotNull();
        repositorySettings.NotNull();
        issueProviderSettings.NotNull();

        var result = new List<IIssue>();

        // Implement log file format logic here.
        result.Add(
            IssueBuilder
                .NewIssue("Some message", issueProvider)
                .WithPriority(IssuePriority.Warning)
                .OfRule("My rule")
                .Create());

        return result;
    }
}
