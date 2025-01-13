/// <summary>
/// Class for retrieving an URL linking to a site describing a rule.
/// </summary>
internal class MyRuleUrlResolver : BaseRuleUrlResolver<MyRuleDescription>
{
    /// <inheritdoc/>
    protected override bool TryGetRuleDescription(
        string rule,
        MyRuleDescription ruleDescription)
    {
        // Take the first 3 characters as category 
        ruleDescription.Category = rule[..3];
        // Take everything afterwards as the ID
        ruleDescription.RuleId = int.Parse(rule.Substring(3));

        return true;
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="MyRuleUrlResolver"/> class.
    /// </summary>
    private MyRuleUrlResolver()
    {
        // Add resolver for different issue categories.
        this.AddUrlResolver(x =>
            x.Category
                .Equals(
                    "FOO",
                    StringComparison.InvariantCultureIgnoreCase) ?
                new Uri("https://www.google.com/search?q=%22" + x.Rule) :
                null);
        this.AddUrlResolver(x =>
            x.Category
                .Equals(
                    "BAR",
                    StringComparison.InvariantCultureIgnoreCase) ?
                new Uri("https://www.bing.com/search?q=%22" + x.Rule) :
                null);
    }
}
