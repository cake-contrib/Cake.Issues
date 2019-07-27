namespace Cake.Issues.GitRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;
    using Cake.Core.Tooling;

    /// <summary>
    /// Provider for issues in Git repositories.
    /// </summary>
    internal class GitRepositoryIssuesProvider : BaseIssueProvider
    {
        private readonly GitRunner runner;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitRepositoryIssuesProvider"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The Cake environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="toolLocator">The tool locator.</param>
        /// <param name="issueProviderSettings">Settings for the issue provider.</param>
        public GitRepositoryIssuesProvider(
            ICakeLog log,
            IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator toolLocator,
            GitRepositoryIssuesSettings issueProviderSettings)
            : base(log)
        {
            fileSystem.NotNull(nameof(fileSystem));
            environment.NotNull(nameof(environment));
            processRunner.NotNull(nameof(processRunner));
            toolLocator.NotNull(nameof(toolLocator));
            issueProviderSettings.NotNull(nameof(issueProviderSettings));

            this.IssueProviderSettings = issueProviderSettings;
            this.runner = new GitRunner(fileSystem, environment, processRunner, toolLocator);
        }

        /// <inheritdoc />
        public override string ProviderName => "Git Repository";

        /// <summary>
        /// Gets the settings for the issue provider.
        /// </summary>
        protected GitRepositoryIssuesSettings IssueProviderSettings { get; private set; }

        /// <inheritdoc />
        protected override IEnumerable<IIssue> InternalReadIssues(IssueCommentFormat format)
        {
            var result = new List<IIssue>();

            if (this.IssueProviderSettings.CheckBinaryFilesTrackedByLfs)
            {
                result.AddRange(this.CheckForBinaryFilesNotTrackedByLfs(format));
            }

            return result;
        }

        /// <summary>
        /// Checks for binary files which are not tracked by LFS.
        /// </summary>
        /// <param name="format">Preferred format of the messages.</param>
        /// <returns>List of issues for binary files which are not tracked by LFS.</returns>
        private IEnumerable<IIssue> CheckForBinaryFilesNotTrackedByLfs(IssueCommentFormat format)
        {
            var allFiles = this.GetAllFilesFromRepository();
            if (!allFiles.Any())
            {
                return new List<IIssue>();
            }

            var textFiles = this.GetTextFilesFromRepository();
            if (!textFiles.Any())
            {
                return new List<IIssue>();
            }

            this.Log.Information("Determine binary files...");
            var binaryFiles = allFiles.Except(textFiles);
            this.Log.Information("Found {0} binary file(s)", binaryFiles.Count());

            if (!binaryFiles.Any())
            {
                return new List<IIssue>();
            }

            this.Log.Debug(string.Join(Environment.NewLine, binaryFiles));

            this.Log.Information("Checking if binary files are tracked by LFS...");
            var lfsTrackedFiles = this.GetLfsTrackedFilesFromRepository();

            var binaryFilesNotTrackedByLfs = binaryFiles.Except(lfsTrackedFiles);
            this.Log.Information("Found {0} binary file(s) not tracked by LFS", binaryFilesNotTrackedByLfs.Count());

            var result = new List<IIssue>();
            foreach (var file in binaryFilesNotTrackedByLfs)
            {
                string message = null;
                switch (format)
                {
                    case IssueCommentFormat.Markdown:
                        message = $"The binary file `{file}` is not tracked by Git LFS";
                        break;
                    case IssueCommentFormat.Html:
                        message = $"The binary file <pre>{file}</pre> is not tracked by Git LFS";
                        break;
                    default:
                        message = $"The binary file \"{file}\" is not tracked by Git LFS";
                        break;
                }

                result.Add(
                    IssueBuilder
                        .NewIssue(message, this)
                        .InFile(file)
                        .OfRule("BinaryFileNotTrackedByLfs")
                        .WithPriority(IssuePriority.Warning)
                        .Create());
            }

            return result;
        }

        /// <summary>
        /// Returns a list of the files in the repository.
        /// </summary>
        /// <returns>List of files in the repository.</returns>
        private IEnumerable<string> GetAllFilesFromRepository()
        {
            var settings = new GitRunnerSettings
            {
                WorkingDirectory = this.Settings.RepositoryRoot,
            };

            this.Log.Information("Reading all files from repository '{0}'...", this.Settings.RepositoryRoot);
            settings.Arguments.Clear();
            settings.Arguments.Add("ls-files");
            var allFiles = this.runner.RunCommand(settings);

            if (allFiles == null)
            {
                throw new Exception("Error reading files from repository");
            }

            this.Log.Information("Found {0} file(s)", allFiles.Count());

            return allFiles;
        }

        /// <summary>
        /// Returns a list of text files in the repository.
        /// </summary>
        /// <returns>List of text files in the repository.</returns>
        private IEnumerable<string> GetTextFilesFromRepository()
        {
            var settings = new GitRunnerSettings
            {
                WorkingDirectory = this.Settings.RepositoryRoot,
            };

            this.Log.Information("Reading all text files from repository '{0}'...", this.Settings.RepositoryRoot);
            settings.Arguments.Clear();
            settings.Arguments.Add("grep -Il .");
            var textFiles = this.runner.RunCommand(settings);
            if (textFiles == null)
            {
                throw new Exception("Error reading text files from repository");
            }

            this.Log.Information("Found {0} text file(s)", textFiles.Count());

            return textFiles;
        }

        /// <summary>
        /// Returns a list of files tracked by Git LFS.
        /// </summary>
        /// <returns>List of files tracked by Git LFS.</returns>
        private IEnumerable<string> GetLfsTrackedFilesFromRepository()
        {
            var settings = new GitRunnerSettings
            {
                WorkingDirectory = this.Settings.RepositoryRoot,
            };

            this.Log.Information("Reading all LFS tracked files from repository '{0}'...", this.Settings.RepositoryRoot);
            settings.Arguments.Clear();
            settings.Arguments.Add("lfs ls-files");
            var lfsTrackedFiles = this.runner.RunCommand(settings);
            if (lfsTrackedFiles == null)
            {
                throw new Exception("Error reading LFS tracked files from repository");
            }

            this.Log.Information("Found {0} LFS tracked file(s)", lfsTrackedFiles.Count());

            return lfsTrackedFiles;
        }
    }
}