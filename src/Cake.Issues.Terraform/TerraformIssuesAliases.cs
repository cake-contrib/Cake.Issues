namespace Cake.Issues.Terraform
{
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Core.IO;

    /// <summary>
    /// Contains functionality related to reading output from <c>terraform validate -json</c>.
    /// </summary>
    [CakeAliasCategory(IssuesAliasConstants.MainCakeAliasCategory)]
    public static class TerraformIssuesAliases
    {
        /// <summary>
        /// Gets the name of the Terraform issue provider.
        /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Name of the Terraform issue provider.</returns>
        [CakePropertyAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static string TerraformIssuesProviderTypeName(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return TerraformIssuesProvider.ProviderTypeName;
        }

        /// <summary>
        /// Gets an instance of a provider for reading output from <c>terraform validate -json</c> from disk
        /// for Terraform scripts in the repository root.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="validateOutputFilePath">Path to the output of the <c>terraform validate</c> command.</param>
        /// <returns>Instance of a provider for warnings reported by <c>terraform validate</c>.</returns>
        /// <example>
        /// <para>Read warnings reported by <c>terraform validate</c>:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues =
        ///         ReadIssues(
        ///             TerraformIssuesFromFilePath(@"c:\build\validate.json"),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider TerraformIssuesFromFilePath(
            this ICakeContext context,
            FilePath validateOutputFilePath)
        {
            context.NotNull(nameof(context));
            validateOutputFilePath.NotNull(nameof(validateOutputFilePath));

            return context.TerraformIssuesFromFilePath(validateOutputFilePath, "/");
        }

        /// <summary>
        /// Gets an instance of a provider for reading output from <c>terraform validate -json</c> from disk.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="validateOutputFilePath">Path to the output of the <c>terraform validate</c> command.</param>
        /// <param name="terraformRootPath">Path to the directory of the Terraform scripts.
        /// Either the full path or the path relative to the repository root.</param>
        /// <returns>Instance of a provider for warnings reported by <c>terraform validate</c>.</returns>
        /// <example>
        /// <para>Read warnings reported by <c>terraform validate</c>:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues =
        ///         ReadIssues(
        ///             TerraformIssuesFromFilePath(
        ///                 @"c:\build\validate.json",
        ///                 @"c:\repo\tf")),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider TerraformIssuesFromFilePath(
            this ICakeContext context,
            FilePath validateOutputFilePath,
            DirectoryPath terraformRootPath)
        {
            context.NotNull(nameof(context));
            validateOutputFilePath.NotNull(nameof(validateOutputFilePath));
            terraformRootPath.NotNull(nameof(terraformRootPath));

            return context.TerraformIssues(new TerraformIssuesSettings(validateOutputFilePath, terraformRootPath));
        }

        /// <summary>
        /// Gets an instance of a provider for reading output from <c>terraform validate -json</c>
        /// for Terraform scripts in the repository root.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="validateOutput">Content of the <c>terraform validate</c> command.</param>
        /// <returns>Instance of a provider for warnings reported by <c>terraform validate</c>.</returns>
        /// <example>
        /// <para>Read warnings reported by <c>terraform validate</c>:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues =
        ///         ReadIssues(
        ///             TerraformIssuesFromContent(validateOutput)),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider TerraformIssuesFromContent(
            this ICakeContext context,
            string validateOutput)
        {
            context.NotNull(nameof(context));
            validateOutput.NotNullOrWhiteSpace(nameof(validateOutput));

            return context.TerraformIssues(new TerraformIssuesSettings(validateOutput.ToByteArray(), "/"));
        }

        /// <summary>
        /// Gets an instance of a provider for reading output from <c>terraform validate -json</c>.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="validateOutput">Content of the <c>terraform validate</c> command.</param>
        /// <param name="terraformRootPath">Path to the directory of the Terraform scripts.
        /// Either the full path or the path relative to the repository root.</param>
        /// <returns>Instance of a provider for warnings reported by <c>terraform validate</c>.</returns>
        /// <example>
        /// <para>Read warnings reported by <c>terraform validate</c>:</para>
        /// <code>
        /// <![CDATA[
        ///     var issues =
        ///         ReadIssues(
        ///             TerraformIssuesFromContent(
        ///                 validateOutput,
        ///                 @"c:\repo\doc")),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider TerraformIssuesFromContent(
            this ICakeContext context,
            string validateOutput,
            DirectoryPath terraformRootPath)
        {
            context.NotNull(nameof(context));
            validateOutput.NotNullOrWhiteSpace(nameof(validateOutput));
            terraformRootPath.NotNull(nameof(terraformRootPath));

            return context.TerraformIssues(new TerraformIssuesSettings(validateOutput.ToByteArray(), terraformRootPath));
        }

        /// <summary>
        /// Gets an instance of a provider for reading output from <c>terraform validate -json</c> using specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for reading the issues.</param>
        /// <returns>Instance of a provider for warnings reported by <c>terraform validate</c>.</returns>
        /// <example>
        /// <para>Read warnings reported by <c>terraform validate</c>:</para>
        /// <code>
        /// <![CDATA[
        ///     var settings =
        ///         new TerraformIssuesSettings(@"c:\build\validate.json");
        ///
        ///     var issues =
        ///         ReadIssues(
        ///             TerraformIssues(settings),
        ///             @"c:\repo");
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(IssuesAliasConstants.IssueProviderCakeAliasCategory)]
        public static IIssueProvider TerraformIssues(
            this ICakeContext context,
            TerraformIssuesSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return new TerraformIssuesProvider(context.Log, settings);
        }
    }
}
