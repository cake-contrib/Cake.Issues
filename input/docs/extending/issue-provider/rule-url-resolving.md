---
Order: 40
Title: Implementing rule URL resolving
Description: Instructions how to implement rule URL resolving.
---
For cases where additional logic is required to determine the URL for a rule, the `Cake.Issue`
addin provides the [BaseRuleDescription] and [BaseRuleUrlResolver] classes for
simplifying implementation of providing URLs linking to site providing information about issues.

# Implementing RuleUrlResolver

In the issue provider a concrete class inheriting from [BaseRuleDescription] should be implemented
containing all properties required to determine the URL to a rule.
The following class adds two properties `Category` and `RuleId` to the description:

```csharp
/// <summary>
/// Class describing rules for my issue provider.
/// </summary>
public class MyRuleDescription : BaseRuleDescription
{
    /// <summary>
    /// Gets or sets the category of the rule.
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the rule.
    /// </summary>
    public int RuleId { get; set; }
}
```

Also a class inheriting from [BaseRuleUrlResolver] needs to be implemented containing an implementation
of [TryGetRuleDescription] for parsing rule urls to the concrete [BaseRuleDescription] class.

```csharp
/// <summary>
/// Class for retrieving an URL linking to a site describing a rule.
/// </summary>
internal class MyRuleUrlResolver : BaseUrlResolver<MyRuleDescription>
{
        /// <inheritdoc/>
        protected override bool TryGetRuleDescription(string rule, MyRuleDescription ruleDescription)
        {
            ruleDescription.RuleId = rule.Substring(3, rule.Length - 3);
            ruleDescription.Category = rule.Substring(0, 3);

            return true;
        }
    }
```

To use the URL resolver the [ResolveRuleUrl] method needs to be called:

```csharp
var resolver = new MyRuleUrlResolver();
var url = resolver.ResolveRuleUrl(rule)
```

Afterwards different resolvers can be registered which return the actual URL based on the rule description:

```csharp
/// <summary>
/// Class for retrieving an URL linking to a site describing a rule.
/// </summary>
internal class MyRuleUrlResolver : BaseUrlResolver<MyRuleDescription>
{
        /// <summary>
        /// Initializes a new instance of the <see cref="MyRuleUrlResolver"/> class.
        /// </summary>
        private MyRuleUrlResolver()
        {
            // Add resolver for different issue categories.
            this.AddUrlResolver(x =>
                x.Category.ToUpperInvariant() == "FOO" ?
                    new Uri("https://www.google.com/search?q=%22" + x.Rule) :
                    null);
            this.AddUrlResolver(x =>
                x.Category.ToUpperInvariant() == "BAR" ?
                    new Uri("https://www.bing.com/search?q=%22" + x.Rule) :
                    null);
        }
    }
```

# Support custom URL resolvers

The [AddUrlResolver] method can also be called from an Cake alias to allow users of the addin to
register custom resolvers.
For this the URL resolver class needs to be implemented as a singleton:

```csharp
/// <summary>
/// Class for retrieving an URL linking to a site describing a rule.
/// </summary>
internal class MyRuleUrlResolver : BaseUrlResolver<MyRuleDescription>
{
    private static readonly Lazy<MyRuleUrlResolver> InstanceValue =
        new Lazy<MyRuleUrlResolver>(() => new MyRuleUrlResolver());

    /// <summary>
    /// Gets the instance of the rule resolver.
    /// </summary>
    public static MyRuleUrlResolver Instance => InstanceValue.Value;
}

[CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
public static class MyIssueProviderAliases
{
    /// <summary>
    /// Registers a new URL resolver with default priority of 0.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="resolver">Resolver which returns an <see cref="Uri"/> linking to a site
    /// containing help for a specific <see cref="MyRuleDescription"/>.</param>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
    public static void MyIssueProviderAddRuleUrlResolver(
        this ICakeContext context,
        Func<MyRuleDescription, Uri> resolver)
    {
        context.NotNull(nameof(context));
        resolver.NotNull(nameof(resolver));

        MyRuleUrlResolver.Instance.AddUrlResolver(resolver);
    }
```

[BaseRuleDescription]: ../../../api/Cake.Issues/BaseRuleDescription/
[BaseRuleUrlResolver]: ../../../api/Cake.Issues/BaseRuleUrlResolver_1/
[TryGetRuleDescription]: ../../../api/Cake.Issues/BaseRuleUrlResolver_1/D9DB5D44
[AddUrlResolver]: ../../../api/Cake.Issues/BaseRuleUrlResolver_1/AAA4FB20
[ResolveRuleUrl]: ../../../api/Cake.Issues/BaseRuleUrlResolver_1/6B23EC74