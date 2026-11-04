namespace Cake.Issues.Tap.LogFileFormat;

using System;

/// <summary>
/// Class for retrieving a URL linking to a site describing a rule.
/// </summary>
internal class TextlintRuleUrlResolver : BaseRuleUrlResolver<BaseRuleDescription>
{
    private static readonly Lazy<TextlintRuleUrlResolver> InstanceValue =
        new(() => new TextlintRuleUrlResolver());

    /// <summary>
    /// Initializes a new instance of the <see cref="TextlintRuleUrlResolver"/> class.
    /// </summary>
    private TextlintRuleUrlResolver()
    {
        this.AddUrlResolver(x => new Uri("https://github.com/textlint-rule/textlint-rule-" + x.Rule));
    }

    /// <summary>
    /// Gets the instance of the rule resolver.
    /// </summary>
    public static TextlintRuleUrlResolver Instance => InstanceValue.Value;

    /// <inheritdoc/>
    protected override bool TryGetRuleDescription(string rule, BaseRuleDescription ruleDescription) => true;
}