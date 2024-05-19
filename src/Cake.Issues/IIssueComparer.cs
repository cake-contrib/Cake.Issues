namespace Cake.Issues
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Comparer to compare if two issues are identical.
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
    public class IIssueComparer(bool compareOnlyPersistentProperties) : IEqualityComparer<IIssue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IIssueComparer"/> class.
        /// Two issues are seen as identical if all properties have identical values.
        /// </summary>
        public IIssueComparer()
            : this(false)
        {
        }

        /// <inheritdoc/>
        public bool Equals(IIssue x, IIssue y) =>
            ReferenceEquals(x, y) ||
            (
                x is not null &&
                y is not null &&
                (x.Identifier == y.Identifier) &&
                (compareOnlyPersistentProperties || x.ProjectFileRelativePath?.FullPath == y.ProjectFileRelativePath?.FullPath) &&
                (x.ProjectName == y.ProjectName) &&
                (compareOnlyPersistentProperties || x.AffectedFileRelativePath?.FullPath == y.AffectedFileRelativePath?.FullPath) &&
                (compareOnlyPersistentProperties || x.Line == y.Line) &&
                (compareOnlyPersistentProperties || x.EndLine == y.EndLine) &&
                (compareOnlyPersistentProperties || x.Column == y.Column) &&
                (compareOnlyPersistentProperties || x.EndColumn == y.EndColumn) &&
                (compareOnlyPersistentProperties || x.FileLink == y.FileLink) &&
                (x.MessageText == y.MessageText) &&
                (x.MessageHtml == y.MessageHtml) &&
                (x.MessageMarkdown == y.MessageMarkdown) &&
                (x.Priority == y.Priority) &&
                (x.PriorityName == y.PriorityName) &&
                (x.RuleId == y.RuleId) &&
                (x.RuleName == y.RuleName) &&
                (x.RuleUrl?.ToString() == y.RuleUrl?.ToString()) &&
                (x.Run == y.Run) &&
                (x.ProviderType == y.ProviderType) &&
                (x.ProviderName == y.ProviderName) &&
                DictionaryContentEquals(x.AdditionalInformation, y.AdditionalInformation));

        /// <inheritdoc/>
        public int GetHashCode(IIssue obj)
        {
            obj.NotNull(nameof(obj));

            return compareOnlyPersistentProperties
                ? GetHashCode(
                        obj.Identifier,
                        obj.ProjectName,
                        obj.MessageText,
                        obj.MessageHtml,
                        obj.MessageMarkdown,
                        obj.Priority,
                        obj.PriorityName,
                        obj.RuleId,
                        obj.RuleName,
                        obj.RuleUrl,
                        obj.Run,
                        obj.ProviderType,
                        obj.ProviderName) +
                    GetDictionaryHashCode(
                        obj.AdditionalInformation)
                : GetHashCode(
                        obj.Identifier,
                        obj.ProjectFileRelativePath?.ToString(),
                        obj.ProjectName,
                        obj.AffectedFileRelativePath?.ToString(),
                        obj.Line,
                        obj.EndLine,
                        obj.Column,
                        obj.EndColumn,
                        obj.FileLink,
                        obj.MessageText,
                        obj.MessageHtml,
                        obj.MessageMarkdown,
                        obj.Priority,
                        obj.PriorityName,
                        obj.RuleId,
                        obj.RuleName,
                        obj.RuleUrl,
                        obj.Run,
                        obj.ProviderType,
                        obj.ProviderName) +
                    GetDictionaryHashCode(
                        obj.AdditionalInformation);
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
}
