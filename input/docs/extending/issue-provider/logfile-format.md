---
Order: 50
Title: Support multiple log file formats
Description: Instructions how to implement support for multiple log file formats.
---
A single issue provider might support reading issues from multiple different log file formats.
For these cases the `Cake.Issue` addin provides the [LogFileFormat] and [MultiFormatIssueProviderSettings]
classes for simplifying implementation in the issue provider addin.

# Implementing log file format infrastructure

In the issue provider an abstract class inheriting from [LogFileFormat] should be implemented
defining the concrete types:

```csharp
/// <summary>
/// Base class for all log file formats supported by my issue provider.
/// </summary>
public abstract class MyLogFileFormat : LogFileFormat<MyIssuesProvider, MyIssuesSettings>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MyLogFileFormat"/> class.
    /// </summary>
    /// <param name="log">The Cake log instance.</param>
    protected MyLogFileFormat(ICakeLog log)
        : base(log)
    {
    }
}
```

Also a concrete class inheriting from [MultiFormatIssueProviderSettings] should be implemented defining the
concrete types.
Based on the capabilities of the log file formats the appropriate constructors for reading from the file system
or memory can be made public:

```csharp
/// <summary>
/// Settings for my issue provider.
/// </summary>
public class MyIssuesSettings : MultiFormatIssueProviderSettings<MyIssuesProvider, MyIssuesSettings>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MyIssuesSettings"/> class
    /// for reading a log file on disk.
    /// </summary>
    /// <param name="logFilePath">Path to the log file.
    /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
    /// <param name="format">Format of the provided log file.</param>
    public MsBuildIssuesSettings(FilePath logFilePath, MyLogFileFormat format)
        : base(logFilePath, format)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MyIssuesSettings"/> class
    /// for a log file content in memory.
    /// </summary>
    /// <param name="logFileContent">Content of the log file.
    /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
    /// <param name="format">Format of the provided log file.</param>
    public MsBuildIssuesSettings(byte[] logFileContent, MyLogFileFormat format)
        : base(logFileContent, format)
    {
    }
}
```

# Implementing log file format

The different log file formats of an issue provider need to be inherited from the abstract log file format class:

```csharp
/// <summary>
/// Concrete log format.
/// </summary>
internal class MyConcreteLogFileFormat : MyLogFileFormat
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MyConcreteLogFileFormat"/> class.
    /// </summary>
    /// <param name="log">The Cake log instance.</param>
    public MyConcreteLogFileFormat(ICakeLog log)
        : base(log)
    {
    }

    /// <inheritdoc/>
    public override IEnumerable<IIssue> ReadIssues(
        MsBuildIssuesProvider issueProvider,
        RepositorySettings repositorySettings,
        MyIssuesSettings issueProviderSettings)
    {
        issueProvider.NotNull(nameof(issueProvider));
        repositorySettings.NotNull(nameof(repositorySettings));
        issueProviderSettings.NotNull(nameof(issueProviderSettings));

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
```

# Aliases

For each concrete log file format a Cake property alias should be provided:

```csharp
/// <summary>
/// Gets an instance of the concrete log format.
/// </summary>
/// <param name="context">The context.</param>
/// <returns>Instance of the concrete log format.</returns>
[CakePropertyAlias]
[CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
public static MyLogFileFormat MyConcreteLogFileFormat(
    this ICakeContext context)
{
    context.NotNull(nameof(context));

    return new MyConcreteLogFileFormat(context.Log);
}
```

Additionally an alias for reading issues with a specific format should be provided:

```csharp
/// <summary>
/// Gets an instance of a provider for issues using specified settings
/// </summary>
/// <param name="context">The context.</param>
/// <param name="settings">Settings for reading the log.</param>
/// <returns>Instance of a provider for issues.</returns>
/// <example>
/// <para>Read issues using my concrete log file format:</para>
/// <code>
/// <![CDATA[
///     var settings =
///         MyIssuesSettings.FromFilePath(
///             @"c:\build\issues.xml",
///             MyConcreteLogFileFormat);
///
///     var issues =
///         ReadIssues(
///             MyIssues(settings),
///             @"c:\repo");
/// ]]>
/// </code>
/// </example>
[CakeMethodAlias]
[CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
public static IIssueProvider MyIssues(
    this ICakeContext context,
    MyIssuesSettings settings)
{
    context.NotNull(nameof(context));
    settings.NotNull(nameof(settings));

    return new MyIssuesProvider(context.Log, settings);
}
```

[LogFileFormat]: ../../../api/Cake.Issues/LogFileFormat_2/
[MultiFormatIssueProviderSettings]: ../../../api/Cake.Issues/MultiFormatIssueProviderSettings_2/