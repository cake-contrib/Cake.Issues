namespace Cake.Issues.Serialization;

/// <summary>
/// Class for serializing and deserializing an <see cref="IIssue"/> instance.
/// </summary>
internal class SerializableIssue
{
    /// <inheritdoc cref="IIssue.ProjectFileRelativePath" />
    public string ProjectFileRelativePath { get; set; }

    /// <inheritdoc cref="IIssue.ProjectName" />
    public string ProjectName { get; set; }

    /// <inheritdoc cref="IIssue.AffectedFileRelativePath" />
    public string AffectedFileRelativePath { get; set; }

    /// <inheritdoc cref="IIssue.Line" />
    public int? Line { get; set; }

    /// <inheritdoc cref="IIssue.MessageText" />
    public string Message { get; set; }

    /// <inheritdoc cref="IIssue.Priority" />
    public int? Priority { get; set; }

    /// <inheritdoc cref="IIssue.PriorityName" />
    public string PriorityName { get; set; }

    /// <inheritdoc cref="IIssue.RuleId" />
    public string Rule { get; set; }

    /// <inheritdoc cref="IIssue.RuleUrl" />
    public string RuleUrl { get; set; }

    /// <inheritdoc cref="IIssue.ProviderType" />
    public string ProviderType { get; set; }

    /// <inheritdoc cref="IIssue.ProviderName" />
    public string ProviderName { get; set; }
}
