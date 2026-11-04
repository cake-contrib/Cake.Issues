namespace Cake.Issues;

using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using Cake.Issues.Serialization;

/// <content>
/// Contains functionality related to serializing and deserializing issues.
/// </content>
public static partial class Aliases
{
    /// <summary>
    /// Serializes an <see cref="IIssue"/> to a JSON string.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issue">Issue which should be serialized.</param>
    /// <returns>Serialized issue.</returns>
    /// <example>
    /// <para>Serializes an issue to a JSON string:</para>
    /// <code>
    /// <![CDATA[
    ///     var jsonString =
    ///         SerializeIssueToJsonString(issue);
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.SerializationCakeAliasCategory)]
    public static string SerializeIssueToJsonString(
        this ICakeContext context,
        IIssue issue)
    {
        context.NotNull();
        issue.NotNull();

        return issue.SerializeToJsonString();
    }

    /// <summary>
    /// Serializes an <see cref="IEnumerable{IIssue}"/> to a JSON string.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be serialized.</param>
    /// <returns>Serialized issues.</returns>
    /// <example>
    /// <para>Serializes a list of issues to a JSON string:</para>
    /// <code>
    /// <![CDATA[
    ///     var jsonString =
    ///         SerializeIssuesToJsonString(issues);
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.SerializationCakeAliasCategory)]
    public static string SerializeIssuesToJsonString(
        this ICakeContext context,
        IEnumerable<IIssue> issues)
    {
        context.NotNull();
        issues.NotNull();

        return issues.SerializeToJsonString();
    }

    /// <summary>
    /// Serializes an <see cref="IIssue"/> to a JSON file.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issue">Issue which should be serialized.</param>
    /// <param name="filePath">Path to the file.</param>
    /// <example>
    /// <para>Serializes an issue to a JSON file:</para>
    /// <code>
    /// <![CDATA[
    ///     SerializeIssueToJsonFile(issue, @"c:\issue.json");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.SerializationCakeAliasCategory)]
    public static void SerializeIssueToJsonFile(
        this ICakeContext context,
        IIssue issue,
        FilePath filePath)
    {
        context.NotNull();
        issue.NotNull();
        filePath.NotNull();

        issue.SerializeToJsonFile(filePath);
    }

    /// <summary>
    /// Serializes an <see cref="IEnumerable{IIssue}"/> to a JSON file.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="issues">Issues which should be serialized.</param>
    /// <param name="filePath">Path to the file.</param>
    /// <example>
    /// <para>Serializes a list of issues to a JSON file:</para>
    /// <code>
    /// <![CDATA[
    ///     SerializeIssuesToJsonFile(@"c:\issues.json");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.SerializationCakeAliasCategory)]
    public static void SerializeIssuesToJsonFile(
        this ICakeContext context,
        IEnumerable<IIssue> issues,
        FilePath filePath)
    {
        context.NotNull();
        issues.NotNull();
        filePath.NotNull();

        issues.SerializeToJsonFile(filePath);
    }

    /// <summary>
    /// Deserializes an <see cref="Issue"/> from a JSON string.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="jsonString">JSON representation of the issue.</param>
    /// <returns>Instance of the issue.</returns>
    /// <example>
    /// <para>Deserializes an issue from a JSON string:</para>
    /// <code>
    /// <![CDATA[
    ///     var issue =
    ///         DeserializeIssueFromJsonString(jsonString);
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.SerializationCakeAliasCategory)]
    public static Issue DeserializeIssueFromJsonString(
        this ICakeContext context,
        string jsonString)
    {
        context.NotNull();
        jsonString.NotNullOrWhiteSpace();

        return jsonString.DeserializeToIssue();
    }

    /// <summary>
    /// Deserializes an <see cref="IEnumerable{IIssue}"/> from a JSON string.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="jsonString">JSON representation of the issues.</param>
    /// <returns>List of issues.</returns>
    /// <example>
    /// <para>Deserializes a list of issue from a JSON string:</para>
    /// <code>
    /// <![CDATA[
    ///     var issues =
    ///         DeserializeIssuesFromJsonString(jsonString);
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.SerializationCakeAliasCategory)]
    public static IEnumerable<Issue> DeserializeIssuesFromJsonString(
        this ICakeContext context,
        string jsonString)
    {
        context.NotNull();
        jsonString.NotNullOrWhiteSpace();

        return jsonString.DeserializeToIssues();
    }

    /// <summary>
    /// Deserializes an <see cref="Issue"/> from a JSON file.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="filePath">Path to the JSON file.</param>
    /// <returns>Instance of the issue.</returns>
    /// <example>
    /// <para>Deserializes an issue from a JSON file:</para>
    /// <code>
    /// <![CDATA[
    ///     var issue =
    ///         DeserializeIssueFromJsonFile(@"c:\issue.json");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.SerializationCakeAliasCategory)]
    public static Issue DeserializeIssueFromJsonFile(
        this ICakeContext context,
        FilePath filePath)
    {
        context.NotNull();
        filePath.NotNull();

        return filePath.DeserializeToIssue();
    }

    /// <summary>
    /// Deserializes an <see cref="IEnumerable{IIssue}"/> from a JSON file.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="filePath">Path to the JSON file.</param>
    /// <returns>List of issues.</returns>
    /// <example>
    /// <para>Deserializes a list of issue from a JSON file:</para>
    /// <code>
    /// <![CDATA[
    ///     var issues =
    ///         DeserializeIssuesFromJsonFile(@"c:\issues.json");
    /// ]]>
    /// </code>
    /// </example>
    [CakeMethodAlias]
    [CakeAliasCategory(IssuesAliasConstants.SerializationCakeAliasCategory)]
    public static IEnumerable<Issue> DeserializeIssuesFromJsonFile(
        this ICakeContext context,
        FilePath filePath)
    {
        context.NotNull();
        filePath.NotNull();

        return filePath.DeserializeToIssues();
    }
}
