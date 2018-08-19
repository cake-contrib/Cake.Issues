namespace Cake.Issues.PullRequests.Tfs
{
    using Core;
    using Core.Annotations;

    /// <content>
    /// Contains functionality related to commit IDs.
    /// </content>
    public static partial class TfsPullRequestSystemAliases
    {
        /// <summary>
        /// Gets the last commit hash on the source branch of the Team Foundation Server or
        /// Visual Studio Team Services pull request using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for accessing the pull request system.</param>
        /// <example>
        /// <para>Get the hash of the last commit:</para>
        /// <code>
        /// <![CDATA[
        ///     var pullRequestSettings =
        ///         new TfsPullRequestSettings(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm());
        ///
        ///     var hash =
        ///         TfsPullRequestLastSourceCommit(
        ///             pullRequestSettings);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>The hash of the last commit on the source branch or <see cref="string.Empty"/>
        /// if no pull request could be found.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory(PullRequestsAliasConstants.PullRequestSystemCakeAliasCategory)]
        public static string TfsPullRequestLastSourceCommit(
            this ICakeContext context,
            TfsPullRequestSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            var pullRequestSystem = new TfsPullRequestSystem(context.Log, settings);
            return pullRequestSystem.GetCapability<TfsCheckingCommitIdCapability>().GetLastSourceCommitId();
        }
    }
}
