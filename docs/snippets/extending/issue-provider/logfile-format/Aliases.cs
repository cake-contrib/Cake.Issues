/// <summary>
/// Contains functionality related to my issue provider.
/// </summary>
[CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
public static class MyIssueAliases
{
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
        context.NotNull();

        return new MyConcreteLogFileFormat(context.Log);
    }

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
        context.NotNull();
        settings.NotNull();

        return new MyIssuesProvider(context.Log, settings);
    }

    /// <summary>
    /// Gets an instance of my issues provider for reading a log file from disk.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="logFilePath">Path to the log file.
    /// The log file needs to be in the format as defined by the
    /// <paramref name="format"/> parameter.</param>
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
        context.NotNull();
        logFilePath.NotNull();
        format.NotNull();

        return context.MyIssues(new MyIssuesSettings(logFilePath, format));
    }

    /// <summary>
    /// Gets an instance of my issues provider for reading a log file
    /// from memory.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="logFileContent">Content of the log file.
    /// The log content needs to be in the format as defined by the
    /// <paramref name="format"/> parameter.</param>
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
        context.NotNull();
        logFileContent.NotNullOrWhiteSpace();
        format.NotNull();

        return
            context.MyIssues(
                new MyIssuesSettings(
                    logFileContent.ToByteArray(),
                    format));
    }

    /// <summary>
    /// Gets the name of my issue provider.
    /// This name can be used to identify issues based on the
    /// <see cref="IIssue.ProviderType"/> property.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>Name of my issue provider.</returns>
    [CakePropertyAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static string MyIssuesProviderTypeName(
        this ICakeContext context)
    {
        context.NotNull();

        return typeof(MyIssuesProvider).FullName;
    }
}
