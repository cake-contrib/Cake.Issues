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

            var binaryFiles = this.DetermineBinaryFiles(allFiles, textFiles);
            if (!binaryFiles.Any())
            {
                return new List<IIssue>();
            }

            var lfsTrackedFiles = this.GetLfsTrackedFilesFromRepository();
            var binaryFilesNotTrackedByLfs = this.DetermineBinaryFilesNotTrackedWithLfs(binaryFiles, lfsTrackedFiles);

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
                        .OfRule(
                            "BinaryFileNotTrackedByLfs",
                            new Uri("https://cakeissues.net/docs/issue-providers/gitrepository/rules/BinaryFileNotTrackedByLfs"))
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
            this.Log.Verbose("Reading all files from repository '{0}'...", this.Settings.RepositoryRoot);

            var settings = new GitRunnerSettings
            {
                WorkingDirectory = this.Settings.RepositoryRoot,
            };

            settings.Arguments.Clear();
            settings.Arguments.Add("ls-files");
            var allFiles = this.runner.RunCommand(settings);

            if (allFiles == null)
            {
                throw new Exception("Error reading files from repository");
            }

            this.Log.Verbose("Found {0} file(s)", allFiles.Count());

            return allFiles;
        }

        /// <summary>
        /// Returns a list of text files in the repository.
        /// </summary>
        /// <returns>List of text files in the repository.</returns>
        private IEnumerable<string> GetTextFilesFromRepository()
        {
            this.Log.Verbose("Reading all text files from repository '{0}'...", this.Settings.RepositoryRoot);

            var settings = new GitRunnerSettings
            {
                WorkingDirectory = this.Settings.RepositoryRoot,
            };

            settings.Arguments.Clear();
            settings.Arguments.Add("grep -Il .");
            var textFiles = this.runner.RunCommand(settings);
            if (textFiles == null)
            {
                throw new Exception("Error reading text files from repository");
            }

            this.Log.Verbose("Found {0} text file(s)", textFiles.Count());

            return textFiles;
        }

        /// <summary>
        /// Returns a list of files tracked by Git LFS.
        /// </summary>
        /// <returns>List of files tracked by Git LFS.</returns>
        private IEnumerable<string> GetLfsTrackedFilesFromRepository()
        {
            this.Log.Verbose("Reading all LFS tracked files from repository '{0}'...", this.Settings.RepositoryRoot);

            var settings = new GitRunnerSettings
            {
                WorkingDirectory = this.Settings.RepositoryRoot,
            };

            settings.Arguments.Clear();
            settings.Arguments.Add("lfs ls-files");
            var lfsTrackedFiles = this.runner.RunCommand(settings);
            if (lfsTrackedFiles == null)
            {
                throw new Exception("Error reading LFS tracked files from repository");
            }

            this.Log.Verbose("Found {0} LFS tracked file(s)", lfsTrackedFiles.Count());

            return lfsTrackedFiles;
        }

        /// <summary>
        /// Determines binary files.
        /// </summary>
        /// <param name="allFiles">List of all files in the repository.</param>
        /// <param name="textFiles">List of text files in the repository.</param>
        /// <returns>List of binary files in the repository.</returns>
        private IEnumerable<string> DetermineBinaryFiles(IEnumerable<string> allFiles, IEnumerable<string> textFiles)
        {
            this.Log.Verbose("Determine binary files...");

            var binaryFiles = allFiles.Except(textFiles);

            if (binaryFiles.Any())
            {
                this.Log.Debug(string.Join(Environment.NewLine, binaryFiles));
            }

            this.Log.Verbose("Found {0} binary file(s)", binaryFiles.Count());

            return binaryFiles;
        }

        /// <summary>
        /// Determines binary files which are not tracked with LFS.
        /// </summary>
        /// <param name="binaryFiles">List of binary files in the repository.</param>
        /// <param name="lfsTrackedFiles">List of files tracked with LFS in the repository.</param>
        /// <returns>List of binary files in the repository which are not tracked with LFS.</returns>
        private IEnumerable<string> DetermineBinaryFilesNotTrackedWithLfs(IEnumerable<string> binaryFiles, IEnumerable<string> lfsTrackedFiles)
        {
            this.Log.Verbose("Checking if binary files are tracked by LFS...");

            var binaryFilesNotTrackedWithLfs = binaryFiles.Except(lfsTrackedFiles);

            if (binaryFilesNotTrackedWithLfs.Any())
            {
                this.Log.Debug(string.Join(Environment.NewLine, binaryFilesNotTrackedWithLfs));
            }

            this.Log.Verbose("Found {0} binary file(s) not tracked by LFS", binaryFilesNotTrackedWithLfs.Count());

            return binaryFilesNotTrackedWithLfs;
        }
    }
}