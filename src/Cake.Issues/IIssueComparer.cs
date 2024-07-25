namespace Cake.Issues;

using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Comparer to compare if two issues are identical.
/// </summary>
public class IIssueComparer(IIssueProperty ignoredProperties) : IEqualityComparer<IIssue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IIssueComparer"/> class.
    /// Two issues are seen as identical if all properties have identical values.
    /// </summary>
    public IIssueComparer()
        : this(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IIssueComparer"/> class.
    /// </summary>
    /// <param name="compareOnlyPersistentProperties">Flag indicating whether properties which
    /// are affected by changes in files should be considered while comparing issues.
    /// If set to <c>true</c>, the comparer can be used to compare issues from different
    /// build runs, where files might have been changed or renamed.</param>
    /// <remarks>
    /// If <paramref name="compareOnlyPersistentProperties"/> is set to <c>true</c> the following
    /// properties will be ignored while comparing the issue:
    /// <list type="bullet">
    /// <item>
    /// <description><see cref="IIssue.ProjectFileRelativePath"/></description>
    /// </item>
    /// <item>
    /// <description><see cref="IIssue.AffectedFileRelativePath"/></description>
    /// </item>
    /// <item>
    /// <description><see cref="IIssue.Line"/></description>
    /// </item>
    /// <item>
    /// <description><see cref="IIssue.EndLine"/></description>
    /// </item>
    /// <item>
    /// <description><see cref="IIssue.Column"/></description>
    /// </item>
    /// <item>
    /// <description><see cref="IIssue.EndColumn"/></description>
    /// </item>
    /// <item>
    /// <description><see cref="IIssue.FileLink"/></description>
    /// </item>
    /// </list>
    /// </remarks>
    public IIssueComparer(bool compareOnlyPersistentProperties)
        : this(compareOnlyPersistentProperties
              ? IIssueProperty.ProjectFileRelativePath |
                IIssueProperty.AffectedFileRelativePath |
                IIssueProperty.Line |
                IIssueProperty.EndLine |
                IIssueProperty.Column |
                IIssueProperty.EndColumn |
                IIssueProperty.FileLink
              : IIssueProperty.None)
    {
    }

    /// <inheritdoc/>
    public bool Equals(IIssue x, IIssue y) =>
        ReferenceEquals(x, y) ||
        (
            x is not null &&
            y is not null &&
            (ignoredProperties.HasFlag(IIssueProperty.Identifier) || x.Identifier == y.Identifier) &&
            (ignoredProperties.HasFlag(IIssueProperty.ProjectFileRelativePath) || x.ProjectFileRelativePath?.FullPath == y.ProjectFileRelativePath?.FullPath) &&
            (ignoredProperties.HasFlag(IIssueProperty.ProjectName) || x.ProjectName == y.ProjectName) &&
            (ignoredProperties.HasFlag(IIssueProperty.AffectedFileRelativePath) || x.AffectedFileRelativePath?.FullPath == y.AffectedFileRelativePath?.FullPath) &&
            (ignoredProperties.HasFlag(IIssueProperty.Line) || x.Line == y.Line) &&
            (ignoredProperties.HasFlag(IIssueProperty.EndLine) || x.EndLine == y.EndLine) &&
            (ignoredProperties.HasFlag(IIssueProperty.Column) || x.Column == y.Column) &&
            (ignoredProperties.HasFlag(IIssueProperty.EndColumn) || x.EndColumn == y.EndColumn) &&
            (ignoredProperties.HasFlag(IIssueProperty.FileLink) || x.FileLink == y.FileLink) &&
            (ignoredProperties.HasFlag(IIssueProperty.MessageText) || x.MessageText == y.MessageText) &&
            (ignoredProperties.HasFlag(IIssueProperty.MessageHtml) || x.MessageHtml == y.MessageHtml) &&
            (ignoredProperties.HasFlag(IIssueProperty.MessageMarkdown) || x.MessageMarkdown == y.MessageMarkdown) &&
            (ignoredProperties.HasFlag(IIssueProperty.Priority) || x.Priority == y.Priority) &&
            (ignoredProperties.HasFlag(IIssueProperty.PriorityName) || x.PriorityName == y.PriorityName) &&
            (ignoredProperties.HasFlag(IIssueProperty.RuleId) || x.RuleId == y.RuleId) &&
            (ignoredProperties.HasFlag(IIssueProperty.RuleName) || x.RuleName == y.RuleName) &&
            (ignoredProperties.HasFlag(IIssueProperty.RuleUrl) || x.RuleUrl?.ToString() == y.RuleUrl?.ToString()) &&
            (ignoredProperties.HasFlag(IIssueProperty.Run) || x.Run == y.Run) &&
            (ignoredProperties.HasFlag(IIssueProperty.ProviderType) || x.ProviderType == y.ProviderType) &&
            (ignoredProperties.HasFlag(IIssueProperty.ProviderName) || x.ProviderName == y.ProviderName) &&
            (ignoredProperties.HasFlag(IIssueProperty.AdditionalInformation) || DictionaryContentEquals(x.AdditionalInformation, y.AdditionalInformation)));

    /// <inheritdoc/>
    public int GetHashCode(IIssue obj)
    {
        obj.NotNull(nameof(obj));

        var valuesToHash = new List<object>();

        if (!ignoredProperties.HasFlag(IIssueProperty.Identifier))
        {
            valuesToHash.Add(obj.Identifier);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.ProjectFileRelativePath))
        {
            valuesToHash.Add(obj.ProjectFileRelativePath?.ToString());
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.ProjectName))
        {
            valuesToHash.Add(obj.ProjectName);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.AffectedFileRelativePath))
        {
            valuesToHash.Add(obj.AffectedFileRelativePath?.ToString());
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.Line))
        {
            valuesToHash.Add(obj.Line);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.EndLine))
        {
            valuesToHash.Add(obj.EndLine);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.Column))
        {
            valuesToHash.Add(obj.Column);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.EndColumn))
        {
            valuesToHash.Add(obj.EndColumn);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.FileLink))
        {
            valuesToHash.Add(obj.FileLink);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.MessageText))
        {
            valuesToHash.Add(obj.MessageText);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.MessageHtml))
        {
            valuesToHash.Add(obj.MessageHtml);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.MessageMarkdown))
        {
            valuesToHash.Add(obj.MessageMarkdown);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.Priority))
        {
            valuesToHash.Add(obj.Priority);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.PriorityName))
        {
            valuesToHash.Add(obj.PriorityName);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.RuleId))
        {
            valuesToHash.Add(obj.RuleId);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.RuleName))
        {
            valuesToHash.Add(obj.RuleName);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.RuleUrl))
        {
            valuesToHash.Add(obj.RuleUrl);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.Run))
        {
            valuesToHash.Add(obj.Run);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.ProviderType))
        {
            valuesToHash.Add(obj.ProviderType);
        }

        if (!ignoredProperties.HasFlag(IIssueProperty.ProviderName))
        {
            valuesToHash.Add(obj.ProviderName);
        }

        var result = GetHashCode([.. valuesToHash]);

        if (!ignoredProperties.HasFlag(IIssueProperty.AdditionalInformation))
        {
            result += GetDictionaryHashCode(obj.AdditionalInformation);
        }

        return result;
    }

    private static int GetHashCode(params object[] objects)
    {
        unchecked
        {
            var hash = 17;

            foreach (var obj in objects)
            {
                hash = (23 * hash) + (obj is not null ? obj.GetHashCode() : 0);
            }

            return hash;
        }
    }

    private static int GetDictionaryHashCode(IReadOnlyDictionary<string, string> dictionary)
    {
        var hash = 17;
        foreach (var kvp in dictionary.OrderBy(p => p.Key))
        {
            hash ^= kvp.Key.GetHashCode();
            hash ^= kvp.Value.GetHashCode();
        }

        return hash;
    }

    private static bool DictionaryContentEquals(
        IReadOnlyDictionary<string, string> dictionary,
        IReadOnlyDictionary<string, string> anotherDictionary) =>
        (dictionary == null && anotherDictionary == null) ||
        (
            dictionary != null &&
            anotherDictionary != null &&
            dictionary.Count == anotherDictionary.Count &&
            !dictionary.Except(anotherDictionary).Any());
}
