namespace Cake.Issues
{
    using System.Collections.Generic;

    /// <summary>
    /// Comparer to compare if two issues are identical.
    /// </summary>
    public class IIssueComparer : IEqualityComparer<IIssue>
    {
        private readonly bool compareOnlyPersistentProperties;

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
        {
            this.compareOnlyPersistentProperties = compareOnlyPersistentProperties;
        }

        /// <inheritdoc/>
        public bool Equals(IIssue x, IIssue y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            return
                (x.Identifier == y.Identifier) &&
                (this.compareOnlyPersistentProperties || x.ProjectFileRelativePath?.FullPath == y.ProjectFileRelativePath?.FullPath) &&
                (x.ProjectName == y.ProjectName) &&
                (this.compareOnlyPersistentProperties || x.AffectedFileRelativePath?.FullPath == y.AffectedFileRelativePath?.FullPath) &&
                (this.compareOnlyPersistentProperties || x.Line == y.Line) &&
                (this.compareOnlyPersistentProperties || x.EndLine == y.EndLine) &&
                (this.compareOnlyPersistentProperties || x.Column == y.Column) &&
                (this.compareOnlyPersistentProperties || x.EndColumn == y.EndColumn) &&
                (this.compareOnlyPersistentProperties || x.FileLink == y.FileLink) &&
                (x.MessageText == y.MessageText) &&
                (x.MessageHtml == y.MessageHtml) &&
                (x.MessageMarkdown == y.MessageMarkdown) &&
                (x.Priority == y.Priority) &&
                (x.PriorityName == y.PriorityName) &&
                (x.Rule == y.Rule) &&
                (x.RuleUrl?.ToString() == y.RuleUrl?.ToString()) &&
                (x.Run == y.Run) &&
                (x.ProviderType == y.ProviderType) &&
                (x.ProviderName == y.ProviderName);
        }

        /// <inheritdoc/>
        public int GetHashCode(IIssue obj)
        {
            if (obj is null)
            {
                return 0;
            }

            if (this.compareOnlyPersistentProperties)
            {
                return
                    GetHashCode(
                        obj.Identifier,
                        obj.ProjectName,
                        obj.MessageText,
                        obj.MessageHtml,
                        obj.MessageMarkdown,
                        obj.Priority,
                        obj.PriorityName,
                        obj.Rule,
                        obj.RuleUrl,
                        obj.Run,
                        obj.ProviderType,
                        obj.ProviderName);
            }
            else
            {
                return
                    GetHashCode(
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
                        obj.Rule,
                        obj.RuleUrl,
                        obj.Run,
                        obj.ProviderType,
                        obj.ProviderName);
            }
        }

        private static int GetHashCode(params object[] objects)
        {
            unchecked
            {
                int hash = 17;

                foreach (var obj in objects)
                {
                    hash = (23 * hash) + (obj is object ? obj.GetHashCode() : 0);
                }

                return hash;
            }
        }
    }
}
