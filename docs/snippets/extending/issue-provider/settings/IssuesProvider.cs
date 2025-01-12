/// <summary>
/// My issue provider.
/// </summary>
/// <param name="log">The Cake log context.</param>
/// <param name="settings">Settings for reading the log file.</param>
public class MyIssuesProvider(ICakeLog log, MyIssuesSettings settings)
    : BaseConfigurableIssueProvider<MyIssuesSettings>(log, settings)
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
