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
    /// <returns>Instance of my issues provider.</returns>
    /// <example>
    /// <para>Read issues using my issues provider:</para>
    /// <code>
    /// <![CDATA[
    ///     var issues = ReadIssues(MyIssues());
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static IIssueProvider MyIssues(
        this ICakeContext context)
    {
        context.NotNull();

        return new MyIssuesProvider(context.Log);
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
