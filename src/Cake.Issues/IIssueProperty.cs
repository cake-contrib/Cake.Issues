namespace Cake.Issues;

using System;

/// <summary>
/// List of properties of <see cref="IIssue"/>.
/// </summary>
[Flags]
public enum IIssueProperty
{
    /// <summary>
    /// No property.
    /// </summary>
    None = 0,

    /// <summary>
    /// <see cref="IIssue.Identifier"/> property.
    /// </summary>
    Identifier = 1,

    /// <summary>
    /// <see cref="IIssue.ProjectFileRelativePath"/> property.
    /// </summary>
    ProjectFileRelativePath = 2,

    /// <summary>
    /// <see cref="IIssue.ProjectName"/> property.
    /// </summary>
    ProjectName = 4,

    /// <summary>
    /// <see cref="IIssue.AffectedFileRelativePath"/> property.
    /// </summary>
    AffectedFileRelativePath = 8,

    /// <summary>
    /// <see cref="IIssue.Line"/> property.
    /// </summary>
    Line = 16,

    /// <summary>
    /// <see cref="IIssue.EndLine"/> property.
    /// </summary>
    EndLine = 32,

    /// <summary>
    /// <see cref="IIssue.Column"/> property.
    /// </summary>
    Column = 64,

    /// <summary>
    /// <see cref="IIssue.EndColumn"/> property.
    /// </summary>
    EndColumn = 128,

    /// <summary>
    /// <see cref="IIssue.Offset"/> property.
    /// </summary>
    Offset = 256,

    /// <summary>
    /// <see cref="IIssue.EndOffset"/> property.
    /// </summary>
    EndOffset = 512,

    /// <summary>
    /// <see cref="IIssue.FileLink"/> property.
    /// </summary>
    FileLink = 1024,

    /// <summary>
    /// <see cref="IIssue.MessageText"/> property.
    /// </summary>
    MessageText = 2048,

    /// <summary>
    /// <see cref="IIssue.MessageHtml"/> property.
    /// </summary>
    MessageHtml = 4096,

    /// <summary>
    /// <see cref="IIssue.MessageMarkdown"/> property.
    /// </summary>
    MessageMarkdown = 8192,

    /// <summary>
    /// <see cref="IIssue.Priority"/> property.
    /// </summary>
    Priority = 16384,

    /// <summary>
    /// <see cref="IIssue.PriorityName"/> property.
    /// </summary>
    PriorityName = 32768,

    /// <summary>
    /// <see cref="IIssue.RuleId"/> property.
    /// </summary>
    RuleId = 65536,

    /// <summary>
    /// <see cref="IIssue.RuleName"/> property.
    /// </summary>
    RuleName = 131072,

    /// <summary>
    /// <see cref="IIssue.RuleUrl"/> property.
    /// </summary>
    RuleUrl = 262144,

    /// <summary>
    /// <see cref="IIssue.Run"/> property.
    /// </summary>
    Run = 524288,

    /// <summary>
    /// <see cref="IIssue.ProviderType"/> property.
    /// </summary>
    ProviderType = 1048576,

    /// <summary>
    /// <see cref="IIssue.ProviderName"/> property.
    /// </summary>
    ProviderName = 2097152,

    /// <summary>
    /// <see cref="IIssue.AdditionalInformation"/> property.
    /// </summary>
    AdditionalInformation = 4194304,
}
