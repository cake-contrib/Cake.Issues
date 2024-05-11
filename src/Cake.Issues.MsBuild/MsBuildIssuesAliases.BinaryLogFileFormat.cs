namespace Cake.Issues.MsBuild
{
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Issues.MsBuild.LogFileFormat;

    /// <content>
    /// Contains functionality related to <see cref="BinaryLogFileFormat"/>.
    /// </content>
    public static partial class MsBuildIssuesAliases
    {
        /// <summary>
        /// Gets an instance for the MsBuild binary log format.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Instance for the MsBuild binary log format.</returns>
        [CakePropertyAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static BaseMsBuildLogFileFormat MsBuildBinaryLogFileFormat(
            this ICakeContext context)
        {
            context.NotNull();

            return new BinaryLogFileFormat(context.Log);
        }
    }
}
