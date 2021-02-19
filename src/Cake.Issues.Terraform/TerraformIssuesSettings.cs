namespace Cake.Issues.Terraform
{
    using Cake.Core.IO;

    /// <summary>
    /// Settings for <see cref="TerraformIssuesAliases"/>.
    /// </summary>
    public class TerraformIssuesSettings : IssueProviderSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TerraformIssuesSettings"/> class.
        /// </summary>
        /// <param name="validateOutputFilePath">Path to the the Terraform output file.</param>
        /// <param name="terraformRootPath">Path to the directory of the Terraform scripts.
        /// Either the full path or the path relative to the repository root.</param>
        public TerraformIssuesSettings(FilePath validateOutputFilePath, DirectoryPath terraformRootPath)
            : base(validateOutputFilePath)
        {
            terraformRootPath.NotNull(nameof(terraformRootPath));

            this.TerraformRootPath = terraformRootPath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerraformIssuesSettings"/> class.
        /// </summary>
        /// <param name="validateOutput">Output of the Terraform validate command.</param>
        /// <param name="terraformRootPath">Path to the directory of the Terraform scripts.
        /// Either the full path or the path relative to the repository root.</param>
        public TerraformIssuesSettings(byte[] validateOutput, DirectoryPath terraformRootPath)
            : base(validateOutput)
        {
            terraformRootPath.NotNull(nameof(terraformRootPath));

            this.TerraformRootPath = terraformRootPath;
        }

        /// <summary>
        /// Gets the path to the directory of the Terraform scripts.
        /// Either the full path or the path relative to the repository root.
        /// </summary>
        public DirectoryPath TerraformRootPath { get; private set; }
    }
}
