---
Order: 20
Title: Provider settings
Description: Instructions how to implement an issue provider with specific settings.
---
Often issue providers require specific settings.
For these cases the `Cake.Issue` addin provides the [BaseConfigurableIssueProvider] and [IssueProviderSettings] classes
for simplifying implementation in the issue provider addin.

# Implementing issue provider

A concrete class inheriting from [BaseConfigurableIssueProvider] needs to be implemented defining the
concrete settings class to use:

```csharp
/// <summary>
/// My issue provider.
/// </summary>
public class MyIssuesProvider : BaseConfigurableIssueProvider<MyIssuesSettings>
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

    /// <inheritdoc />
    protected override IEnumerable<IIssue> InternalReadIssues()
    {
        var result = new List<IIssue>();

        // Implement issue provider logic here.
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

Also a concrete class inheriting from [IssueProviderSettings] needs to be implemented.
Based on the capabilities of the issue provider the appropriate constructors for reading from the file system
or memory can be made public:

```csharp
/// <summary>
/// Settings for my issue provider.
/// </summary>
public class MyIssuesSettings : IssueProviderSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MyIssuesSettings"/> class
    /// for reading a log file on disk.
    /// </summary>
    /// <param name="logFilePath">Path to the log file.</param>
    public MyIssuesSettings(FilePath logFilePath)
        : base(logFilePath)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MyIssuesSettings"/> class
    /// for a log file content in memory.
    /// </summary>
    /// <param name="logFileContent">Content of the log file.</param>
    public MyIssuesSettings(byte[] logFileContent)
        : base(logFileContent)
    {
    }

    // Add additional settings for the issue provider here.
}
```

# Aliases

An alias for reading issues with the provider should be provided:

```csharp
/// <summary>
/// Gets an instance of my issues provider using specified settings.
/// </summary>
/// <param name="context">The context.</param>
/// <param name="settings">Settings for reading the log.</param>
/// <returns>Instance of my issues provider.</returns>
/// <example>
/// <para>Read issues using my issues provider:</para>
/// <code>
/// <![CDATA[
///     var settings =
///         new MyIssuesSettings(@"c:\build\issues.log");
///
///     var issues =
///         ReadIssues(
///             MyIssues(settings));
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
/// <param name="logFilePath">Path to the log file.</param>
/// <returns>Instance of my issues provider.</returns>
/// <example>
/// <para>Read issues using my issues provider:</para>
/// <code>
/// <![CDATA[
///     var issues =
///         ReadIssues(
///             MyIssuesFromFilePath(@"c:\build\issues.log"));
/// ]]>
/// </code>
/// </example>
[CakeMethodAlias]
[CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
public static IIssueProvider MyIssuesFromFilePath(
    this ICakeContext context,
    FilePath logFilePath)
{
    context.NotNull(nameof(context));
    logFilePath.NotNull(nameof(logFilePath));

    return context.MyIssues(new MyIssuesSettings(logFilePath));
}

/// <summary>
/// Gets an instance of my issues provider for reading a log file from memory.
/// </summary>
/// <param name="context">The context.</param>
/// <param name="logFileContent">Content of the log file.</param>
/// <returns>Instance of my issues provider.</returns>
/// <example>
/// <para>Read issues using my issues provider:</para>
/// <code>
/// <![CDATA[
///     var issues =
///         ReadIssues(
///             MyIssuesFromContent(logFileContent));
/// ]]>
/// </code>
/// </example>
[CakeMethodAlias]
[CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
public static IIssueProvider MyIssuesFromContent(
    this ICakeContext context,
    string logFileContent)
{
    context.NotNull(nameof(context));
    logFileContent.NotNullOrWhiteSpace(nameof(logFileContent));

    return context.MyIssues(new MyIssuesSettings(logFileContent.ToByteArray()));
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

[BaseConfigurableIssueProvider]: ../../../../api/Cake.Issues/BaseConfigurableIssueProvider_1/
[IssueProviderSettings ]: ../../../../api/Cake.Issues/IssueProviderSettings/