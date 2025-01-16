/// <summary>
/// Contains functionality related to my issue provider.
/// </summary>
[CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
public static class MyIssueAliases
{
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
    ///     var settings = new MyIssuesSettings(@"c:\build\issues.log");
    ///     var issues = ReadIssues(MyIssues(settings));
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
    /// <param name="logFilePath">Path to the log file.</param>
    /// <returns>Instance of my issues provider.</returns>
    /// <example>
    /// <para>Read issues using my issues provider:</para>
    /// <code>
    /// <![CDATA[
    ///     var issues = ReadIssues(
    ///         MyIssuesFromFilePath(@"c:\build\issues.log"));
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider MyIssuesFromFilePath(
        this ICakeContext context,
        FilePath logFilePath)
    {
        context.NotNull();
        logFilePath.NotNull();

        return context.MyIssues(new MyIssuesSettings(logFilePath));
    }

    /// <summary>
    /// Gets an instance of my issues provider for reading a log file
    /// from memory.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="logFileContent">Content of the log file.</param>
    /// <returns>Instance of my issues provider.</returns>
    /// <example>
    /// <para>Read issues using my issues provider:</para>
    /// <code>
    /// <![CDATA[
    ///     var issues = ReadIssues(
    ///         MyIssuesFromContent(logFileContent));
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider MyIssuesFromContent(
        this ICakeContext context,
        string logFileContent)
    {
        context.NotNull();
        logFileContent.NotNullOrWhiteSpace();

        return context.MyIssues(
            new MyIssuesSettings(logFileContent.ToByteArray()));
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
