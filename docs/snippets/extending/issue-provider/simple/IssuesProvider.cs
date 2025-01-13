/// <summary>
/// My issue provider.
/// </summary>
/// <param name="log">The Cake log context.</param>
public class MyIssuesProvider(ICakeLog log) : BaseIssueProvider(log)
{
    /// <inheritdoc />
    public override string ProviderName => "MyIssuesProvider";

    /// <inheritdoc />
    protected override IEnumerable<IIssue> InternalReadIssues()
    {
        var result = new List<IIssue>();

        // Implement issue provider logic here.
        result.Add(
            IssueBuilder
                .NewIssue("Some message", this)
                .WithPriority(IssuePriority.Warning)
                .OfRule("My rule")
                .Create());

        return result;
    }
}
