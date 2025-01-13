/// <summary>
/// My issue provider.
/// </summary>
/// <param name="log">The Cake log context.</param>
/// <param name="settings">Settings for reading the log file.</param>
public class MyIssuesProvider(ICakeLog log, MyIssuesSettings settings)
    : BaseMultiFormatIssueProvider<MyIssuesSettings, MyIssuesProvider>(
        log,
        settings)
{
    /// <inheritdoc />
    public override string ProviderName => "MyIssuesProvider";
}
