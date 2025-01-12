namespace Cake.Issues.MsBuild;

using System;
using System.Text;

/// <summary>
/// Class for retrieving a URL linking to a site describing a rule.
/// </summary>
internal class MsBuildRuleUrlResolver : BaseRuleUrlResolver<MsBuildRuleDescription>
{
    private static readonly Lazy<MsBuildRuleUrlResolver> InstanceValue =
        new(() => new MsBuildRuleUrlResolver());

    /// <summary>
    /// Initializes a new instance of the <see cref="MsBuildRuleUrlResolver"/> class.
    /// </summary>
    private MsBuildRuleUrlResolver()
    {
        // Add resolver for common known issue categories.

        // .NET SDK analyzers
        this.AddUrlResolver(x =>
            x.Category.Equals("CA", StringComparison.OrdinalIgnoreCase) ?
                new Uri("https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/" + x.Rule) :
                null);

        // StyleCop analyzer rules
        this.AddUrlResolver(x =>
            x.Category.Equals("SA", StringComparison.OrdinalIgnoreCase) ?
                new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/" + x.Rule + ".md") :
                null);

        // SonarLint rules
        this.AddUrlResolver(x =>
            x.Category.Equals("S", StringComparison.OrdinalIgnoreCase) ?
                new Uri("https://rules.sonarsource.com/csharp/RSPEC-" + x.RuleId) :
                null);

        // Roslynator rules
        this.AddUrlResolver(x =>
            x.Category.Equals("RCS", StringComparison.OrdinalIgnoreCase) ?
                new Uri("https://github.com/JosefPihrt/Roslynator/blob/main/docs/analyzers/" + x.Rule + ".md") :
                null);
    }

    /// <summary>
    /// Gets the instance of the rule resolver.
    /// </summary>
    public static MsBuildRuleUrlResolver Instance => InstanceValue.Value;

    /// <inheritdoc/>
    protected override bool TryGetRuleDescription(string rule, MsBuildRuleDescription ruleDescription)
    {
        // Parse the rule. Expect it in the form starting with an identifier containing characters
        // followed by the rule id as a number.
        var digitIndex = -1;
        var categoryBuilder = new StringBuilder();
        for (var index = 0; index < rule.Length; index++)
        {
            var currentChar = rule[index];
            if (char.IsDigit(currentChar))
            {
                digitIndex = index;
                break;
            }

            _ = categoryBuilder.Append(currentChar);
        }

        // If rule doesn't contain numbers return false.
        if (digitIndex < 0)
        {
            return false;
        }

        // Try to parse the part after the first number to an integer.
        if (!int.TryParse(rule.AsSpan(digitIndex), out var ruleId))
        {
            return false;
        }

        ruleDescription.RuleId = ruleId;
        ruleDescription.Category = categoryBuilder.ToString();

        return true;
    }
}
