namespace Cake.Issues.Tap.LogFileFormat;

/// <summary>
/// Class describing rules appearing in TAP logs written by stylelint.
/// </summary>
public class StylelintRuleDescription : BaseRuleDescription
{
    /// <summary>
    /// Gets or sets a value indicating whether the rule is from a plugin or not.
    /// </summary>
    public bool IsPlugin { get; set; }
}