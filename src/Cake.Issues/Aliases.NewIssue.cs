﻿namespace Cake.Issues
{
    using Cake.Core;
    using Cake.Core.Annotations;

    /// <content>
    /// Contains functionality related to creating issues.
    /// </content>
    public static partial class Aliases
    {
        /// <summary>
        /// Initiates the creation of a new <see cref="IIssue"/> with <paramref name="message"/> as identifier.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="message">The message of the issue.</param>
        /// <param name="providerType">The unique identifier of the issue provider.</param>
        /// <param name="providerName">The human friendly name of the issue provider.</param>
        /// <returns>Builder class for creating a new <see cref="IIssue"/>.</returns>
        /// <example>
        /// <para>Create a new warning for the myfile.txt file on line 42:</para>
        /// <code>
        /// <![CDATA[
        ///     var issue =
        ///         NewIssue(
        ///             "Something went wrong",
        ///             "MyCakeScript",
        ///             "My Cake Script")
        ///             .InFile("myfile.txt", 42)
        ///             .WithPriority(IssuePriority.Warning)
        ///             .Create();
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.CreateCakeAliasCategory)]
        public static IssueBuilder NewIssue(
            this ICakeContext context,
            string message,
            string providerType,
            string providerName)
        {
            context.NotNull(nameof(context));
            message.NotNullOrWhiteSpace(nameof(message));
            providerType.NotNullOrWhiteSpace(nameof(providerType));
            providerName.NotNullOrWhiteSpace(nameof(providerName));

            return IssueBuilder.NewIssue(message, providerType, providerName);
        }

        /// <summary>
        /// Initiates the creation of a new <see cref="IIssue"/>.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="identifier">The identifier of the issue.</param>
        /// <param name="message">The message of the issue.</param>
        /// <param name="providerType">The unique identifier of the issue provider.</param>
        /// <param name="providerName">The human friendly name of the issue provider.</param>
        /// <returns>Builder class for creating a new <see cref="IIssue"/>.</returns>
        /// <example>
        /// <para>Create a new warning for the myfile.txt file on line 42:</para>
        /// <code>
        /// <![CDATA[
        ///     var issue =
        ///         NewIssue(
        ///             "Issue identifier",
        ///             "Something went wrong",
        ///             "MyCakeScript",
        ///             "My Cake Script")
        ///             .InFile("myfile.txt", 42)
        ///             .WithPriority(IssuePriority.Warning)
        ///             .Create();
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.CreateCakeAliasCategory)]
        public static IssueBuilder NewIssue(
            this ICakeContext context,
            string identifier,
            string message,
            string providerType,
            string providerName)
        {
#pragma warning disable SA1123 // Do not place regions within elements
            #region DupFinder Exclusion
#pragma warning restore SA1123 // Do not place regions within elements

            context.NotNull(nameof(context));
            identifier.NotNullOrWhiteSpace(nameof(identifier));
            message.NotNullOrWhiteSpace(nameof(message));
            providerType.NotNullOrWhiteSpace(nameof(providerType));
            providerName.NotNullOrWhiteSpace(nameof(providerName));

            #endregion

            return IssueBuilder.NewIssue(identifier, message, providerType, providerName);
        }
    }
}
