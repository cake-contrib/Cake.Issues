namespace Cake.Issues.Tap.LogFileFormat;

using System;
using System.Collections.Generic;

/// <summary>
/// Class for retrieving a URL linking to a site describing a rule.
/// </summary>
internal class StylelintRuleUrlResolver : BaseRuleUrlResolver<StylelintRuleDescription>
{
    private static readonly Lazy<StylelintRuleUrlResolver> InstanceValue =
        new(() => new StylelintRuleUrlResolver());

    private readonly IList<string> rulesWithoutDocumentation = ["parseError", "unknown"];

    /// <summary>
    /// Initializes a new instance of the <see cref="StylelintRuleUrlResolver"/> class.
    /// </summary>
    private StylelintRuleUrlResolver()
    {
        this.AddUrlResolver(x =>
            !x.IsPlugin && !this.rulesWithoutDocumentation.Contains(x.Rule) ?
            new Uri("https://stylelint.io/user-guide/rules/" + x.Rule) :
            null);
    }

    /// <summary>
    /// Gets the instance of the rule resolver.
    /// </summary>
    public static StylelintRuleUrlResolver Instance => InstanceValue.Value;

    /// <inheritdoc/>
    protected override bool TryGetRuleDescription(string rule, StylelintRuleDescription ruleDescription)
    {
        ruleDescription.IsPlugin = rule.Contains('/');

        return true;
    }
}