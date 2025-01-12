/// <summary>
/// Base class for all log file formats supported by my issue provider.
/// </summary>
/// <param name="log">The Cake log instance.</param>
public abstract class MyLogFileFormat(ICakeLog log)
    : BaseLogFileFormat<MyIssuesProvider, MyIssuesSettings>(log)
{
}
