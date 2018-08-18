---
Order: 30
Title: Multiple log file formats
Description: Instructions how to implement an issue provider with support for multiple log file formats.
---
A single issue provider might support reading issues from multiple different log file formats.
For these cases the `Cake.Issue` addin provides the [BaseMultiFormatIssueProvider], [BaseMultiFormatIssueProviderSettings]
and [BaseLogFileFormat] classes for simplifying implementation in the issue provider addin.

# Implementing issue provider

A concrete class inheriting from [BaseMultiFormatIssueProvider] needs to be implemented defining the
concrete types.

```csharp
/// <summary>
/// My issue provider.
/// </summary>
public class MyIssuesProvider : BaseMultiFormatIssueProvider<MyIssuesSettings, MyIssuesProvider>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MyIssuesProvider"/> class.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    /// <param name="settings">Settings for reading the log file.</param>
    public MyIssuesProvider(ICakeLog log, MyIssuesSettings settings)
        : base(log, settings)
    {
    }

    /// <inheritdoc />
    public override string ProviderName => "MyIssuesProvider";
}
```

Also a concrete class inheriting from [BaseMultiFormatIssueProviderSettings] needs to be implemented defining the
concrete types.
Based on the capabilities of the log file formats the appropriate constructors for reading from the file system
or memory can be made public:

```csharp
/// <summary>
/// Settings for my issue provider.
/// </summary>
public class MyIssuesSettings : BaseMultiFormatIssueProviderSettings<MyIssuesProvider, MyIssuesSettings>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MyIssuesSettings"/> class
    /// for reading a log file on disk.
    /// </summary>
    /// <param name="logFilePath">Path to the log file.
    /// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
    /// <param name="format">Format of the provided log file.</param>
    public MyIssuesSettings(FilePath logFilePath, MyLogFileFormat format)
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
    public MyIssuesSettings(byte[] logFileContent, MyLogFileFormat format)
        : base(logFileContent, format)
    {
    }
}
```

# Implementing log file format infrastructure

An abstract class inheriting from [BaseLogFileFormat] needs to be implemented
defining the concrete types for the issue provider:

```csharp
/// <summary>
/// Base class for all log file formats supported by my issue provider.
/// </summary>
public abstract class MyLogFileFormat : BaseLogFileFormat<MyIssuesProvider, MyIssuesSettings>
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
        MyIssuesProvider issueProvider,
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
///         new MyIssuesSettings(
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

For convenience of the user and based on the capabilities of the issue provider additional aliases for reading
from the file system or from memory can be added:

```csharp
/// <summary>
/// Gets an instance of my issues provider for reading a log file from disk.
/// </summary>
/// <param name="context">The context.</param>
/// <param name="logFilePath">Path to the log file.
/// The log file needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
/// <param name="format">Format of the provided log file.</param>
/// <returns>Instance of my issues provider.</returns>
/// <example>
/// <para>Read issues using my issues provider:</para>
/// <code>
/// <![CDATA[
///     var issues =
///         ReadIssues(
///             MyIssuesFromFilePath(
///                 @"c:\build\issues.log",
///                 MyConcreteLogFileFormat));
/// ]]>
/// </code>
/// </example>
[CakeMethodAlias]
[CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
public static IIssueProvider MyIssuesFromFilePath(
    this ICakeContext context,
    FilePath logFilePath,
    MyLogFileFormat format)
{
    context.NotNull(nameof(context));
    logFilePath.NotNull(nameof(logFilePath));
    format.NotNull(nameof(format));

    return context.MyIssues(new MyIssuesSettings(logFilePath, format));
}

/// <summary>
/// Gets an instance of my issues provider for reading a log file from memory.
/// </summary>
/// <param name="context">The context.</param>
/// <param name="logFileContent">Content of the log file.
/// The log content needs to be in the format as defined by the <paramref name="format"/> parameter.</param>
/// <param name="format">Format of the provided log content.</param>
/// <returns>Instance of my issues provider.</returns>
/// <example>
/// <para>Read issues using my issues provider:</para>
/// <code>
/// <![CDATA[
///     var issues =
///         ReadIssues(
///             MyIssuesFromContent(
///                 logFileContent,
///                 MyConcreteLogFileFormat));
/// ]]>
/// </code>
/// </example>
[CakeMethodAlias]
[CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
public static IIssueProvider MyIssuesFromContent(
    this ICakeContext context,
    string logFileContent,
    MyLogFileFormat format)
{
    context.NotNull(nameof(context));
    logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));
    format.NotNull(nameof(format));

    return context.MyIssues(new MyIssuesSettings(logFileContent.ToByteArray(), format));
}
```

Finally an additional property alias for returning the provider type name should be defined:

```csharp
/// <summary>
/// Gets the name of my issue provider.
/// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
/// </summary>
/// <param name="context">The context.</param>
/// <returns>Name of my issue provider.</returns>
[CakePropertyAlias]
[CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
public static string MyIssuesProviderTypeName(
    this ICakeContext context)
{
    context.NotNull(nameof(context));

    return typeof(MyIssuesProvider).FullName;
}
```

[BaseMultiFormatIssueProvider]: ../../../../api/Cake.Issues/BaseMultiFormatIssueProvider_2/
[BaseMultiFormatIssueProviderSettings]: ../../../../api/Cake.Issues/BaseMultiFormatIssueProviderSettings_2/
[BaseLogFileFormat]: ../../../../api/Cake.Issues/BaseLogFileFormat_2/
