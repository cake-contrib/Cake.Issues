---
Order: 10
Title: Simple issue provider
Description: Instructions how to implement a simple issue provider.
---
This tutorial explains how to implement a simple issue provider using the [BaseIssueProvider] class
from the `Cake.Issue` addin.

# Implementing issue provider

A concrete class inheriting from [BaseIssueProvider] needs to be implemented:

```csharp
/// <summary>
/// My issue provider.
/// </summary>
public class MyIssuesProvider : BaseIssueProvider
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MyIssuesProvider"/> class.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    public MyIssuesProvider(ICakeLog log)
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

# Aliases

An alias for reading issues with the provider should be provided:

```csharp
/// <summary>
/// Gets an instance of my issues provider using specified settings.
/// </summary>
/// <param name="context">The context.</param>
/// <returns>Instance of my issues provider.</returns>
/// <example>
/// <para>Read issues using my issues provider:</para>
/// <code>
/// <![CDATA[
///     var issues =
///         ReadIssues(
///             MyIssues());
/// ]]>
/// </code>
/// </example>
[CakeMethodAlias]
[CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
public static IIssueProvider MyIssues(
    this ICakeContext context)
{
    context.NotNull(nameof(context));

    return new MyIssuesProvider(context.Log);
}
```

Additionally a property alias for returning the provider type name should be defined:

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

[BaseIssueProvider]: ../../../../api/Cake.Issues/BaseIssueProvider/