namespace Cake.Issues.GitRepository;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    private readonly Lazy<IEnumerable<string>> allFiles;
    private readonly Lazy<IEnumerable<string>> textFiles;
    private readonly Lazy<IEnumerable<string>> binaryFiles;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitRepositoryIssuesProvider"/> class.
    /// </summary>
    /// <param name="log">The Cake log context.</param>
    /// <param name="fileSystem">The file system.</param>
    /// <param name="environment">The Cake environment.</param>
    /// <param name="processRunner">The process runner.</param>
    /// <param name="toolLocator">The tool locator.</param>
    /// <param name="globber">The globber.</param>
    /// <param name="issueProviderSettings">Settings for the issue provider.</param>
    public GitRepositoryIssuesProvider(
        ICakeLog log,
        IFileSystem fileSystem,
        ICakeEnvironment environment,
        IProcessRunner processRunner,
        IToolLocator toolLocator,
        IGlobber globber,
        GitRepositoryIssuesSettings issueProviderSettings)
        : base(log)
    {
        fileSystem.NotNull();
        environment.NotNull();
        processRunner.NotNull();
        toolLocator.NotNull();
        globber.NotNull();
        issueProviderSettings.NotNull();

        this.IssueProviderSettings = issueProviderSettings;
        this.runner = new GitRunner(fileSystem, environment, processRunner, toolLocator);
        this.Globber = globber;

        this.allFiles =
            new Lazy<IEnumerable<string>>(
                this.GetAllFilesFromRepository);
        this.textFiles =
            new Lazy<IEnumerable<string>>(
                this.GetTextFilesFromRepository);
        this.binaryFiles =
            new Lazy<IEnumerable<string>>(
                () => this.DetermineBinaryFiles(this.allFiles.Value, this.textFiles.Value));
    }

    /// <summary>
    /// Gets the globber.
    /// </summary>
    protected IGlobber Globber { get; private set; }

    /// <summary>
    /// Enum representing the different types of checks performed by the provider.
    /// </summary>
    private enum CheckType
    {
        BinaryFilesLfs,
        FilePathLength,
    }

    /// <summary>
    /// Gets the name of the Git repository issue provider.
    /// This name can be used to identify issues based on the <see cref="IIssue.ProviderType"/> property.
    /// </summary>
    public static string ProviderTypeName => typeof(GitRepositoryIssuesProvider).FullName;

    /// <inheritdoc />
    public override string ProviderName => "Git Repository";

    /// <summary>
    /// Gets the settings for the issue provider.
    /// </summary>
    protected GitRepositoryIssuesSettings IssueProviderSettings { get; }

    /// <inheritdoc />
    protected override IEnumerable<IIssue> InternalReadIssues()
    {
        var result = new List<IIssue>();

        var assembly = Assembly.GetAssembly(typeof(GitRepositoryIssuesProvider));
        var fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
        var versionParts = fileVersion.Split('.');
        var issueProviderVersion = $"{versionParts[0]}.{versionParts[1]}.{versionParts[2]}";

        if (this.IssueProviderSettings.CheckBinaryFilesTrackedByLfs)
        {
            result.AddRange(
                this.CheckForBinaryFilesNotTrackedByLfs(issueProviderVersion));
        }

        if (this.IssueProviderSettings.CheckFilesPathLength)
        {
            result.AddRange(this.CheckForFilesPathLength(issueProviderVersion));
        }

        return result;
    }

    /// <summary>
    /// Checks for binary files which are not tracked by LFS.
    /// </summary>
    /// <param name="issueProviderVersion">Version of the issue provider.</param>
    /// <returns>List of issues for binary files which are not tracked by LFS.</returns>
    private List<IIssue> CheckForBinaryFilesNotTrackedByLfs(string issueProviderVersion)
    {
        if (!this.allFiles.Value.Any())
        {
            return [];
        }

        if (!this.binaryFiles.Value.Any())
        {
            return [];
        }

        var lfsTrackedFiles = this.GetLfsTrackedFilesFromRepository();
        var binaryFilesNotTrackedByLfs =
            this.DetermineBinaryFilesNotTrackedWithLfs(this.binaryFiles.Value, lfsTrackedFiles);

        // Filter out excluded files
        var filteredFiles = binaryFilesNotTrackedByLfs.Where(file => !this.IsFileExcluded(file, CheckType.BinaryFilesLfs)).ToList();

        var result = new List<IIssue>();
        foreach (var file in filteredFiles)
        {
            var ruleDescription = new BinaryFileNotTrackedByLfsRuleDescription();

            result.Add(
                IssueBuilder
                    .NewIssue($"The binary file \"{file}\" is not tracked by Git LFS", this)
                    .WithMessageInHtmlFormat($"The binary file <code>{file}</code> is not tracked by Git LFS")
                    .WithMessageInMarkdownFormat($"The binary file `{file}` is not tracked by Git LFS")
                    .InFile(file)
                    .OfRule(ruleDescription, issueProviderVersion)
                    .Create());
        }

        return result;
    }

    /// <summary>
    /// Checks for files path length.
    /// </summary>
    /// <param name="issueProviderVersion">Version of the issue provider.</param>
    /// <returns>List of issues for repository files with paths exceeding the allowed maximum.</returns>
    private List<IIssue> CheckForFilesPathLength(string issueProviderVersion)
    {
        if (!this.allFiles.Value.Any())
        {
            return [];
        }

        var result = new List<IIssue>();

        // Filter out excluded files and check path length
        var filesToCheck = this.allFiles.Value.Where(file =>
            !this.IsFileExcluded(file, CheckType.FilePathLength) &&
            file.Length > this.IssueProviderSettings.MaxFilePathLength);

        foreach (var file in filesToCheck)
        {
            var ruleDescription = new FilePathTooLongRuleDescription();

            result.Add(
                IssueBuilder
                    .NewIssue($"The path for the file \"{file}\" is too long. Maximum allowed path length is {this.IssueProviderSettings.MaxFilePathLength}, actual path length is {file.Length}.", this)
                    .WithMessageInHtmlFormat($"The path for the file <code>{file}</code> is too long. Maximum allowed path length is {this.IssueProviderSettings.MaxFilePathLength}, actual path length is {file.Length}.")
                    .WithMessageInMarkdownFormat($"The path for the file `{file}` is too long. Maximum allowed path length is {this.IssueProviderSettings.MaxFilePathLength}, actual path length is {file.Length}.")
                    .InFile(file)
                    .OfRule(ruleDescription, issueProviderVersion)
                    .Create());
        }

        return result;
    }

    /// <summary>
    /// Returns a list of the files in the repository.
    /// </summary>
    /// <returns>List of files in the repository.</returns>
    private List<string> GetAllFilesFromRepository()
    {
        this.Log.Verbose("Reading all files from repository '{0}'...", this.Settings.RepositoryRoot);

        var settings = new GitRunnerSettings
        {
            WorkingDirectory = this.Settings.RepositoryRoot,
        };

        settings.Arguments.Clear();
        settings.Arguments.Add("ls-files -z");
        var output =
            this.runner.RunCommand(settings)
            ?? throw new Exception("Error reading files from repository");
        var result =
            string.Join(
                    string.Empty,
                    output)
                .Split('\0')
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();
        this.Log.Verbose("Found {0} file(s)", result.Count);

        return result;
    }

    /// <summary>
    /// Returns a list of text files in the repository.
    /// </summary>
    /// <returns>List of text files in the repository.</returns>
    private List<string> GetTextFilesFromRepository()
    {
        this.Log.Verbose("Reading all text files from repository '{0}'...", this.Settings.RepositoryRoot);

        var settings = new GitRunnerSettings
        {
            WorkingDirectory = this.Settings.RepositoryRoot,

            // git grep -IL . can return an exit code of 1 if nothing matches
            HandleExitCode = exitCode => exitCode is 0 or 1,
        };

        settings.Arguments.Clear();
        settings.Arguments.Add("grep -Il .");
        var textFilesFromRepository =
            this.runner.RunCommand(settings)
            ?? throw new Exception("Error reading text files from repository");
        settings.Arguments.Clear();
        settings.Arguments.Add("grep -IL .");
        var emptyFiles = this.runner.RunCommand(settings);

        if (emptyFiles != null && emptyFiles.Any())
        {
            textFilesFromRepository = textFilesFromRepository.Concat(emptyFiles);
        }

        var result = textFilesFromRepository.ToList();

        this.Log.Verbose("Found {0} text file(s)", result.Count);

        return result;
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
        settings.Arguments.Add("lfs ls-files -n");
        var lfsTrackedFiles =
            this.runner.RunCommand(settings)
            ?? throw new Exception("Error reading LFS tracked files from repository");
        lfsTrackedFiles = lfsTrackedFiles.ToList();
        this.Log.Verbose("Found {0} LFS tracked file(s)", lfsTrackedFiles.Count());

        return lfsTrackedFiles;
    }

    /// <summary>
    /// Determines binary files.
    /// </summary>
    /// <param name="allFiles">List of all files in the repository.</param>
    /// <param name="textFiles">List of text files in the repository.</param>
    /// <returns>List of binary files in the repository.</returns>
    private List<string> DetermineBinaryFiles(IEnumerable<string> allFiles, IEnumerable<string> textFiles)
    {
        this.Log.Verbose("Determine binary files...");

        var result = allFiles.Except(textFiles).ToList();

        if (result.Count > 0)
        {
            this.Log.Debug(string.Join(Environment.NewLine, result));
        }

        this.Log.Verbose("Found {0} binary file(s)", result.Count);

        return result;
    }

    /// <summary>
    /// Determines binary files which are not tracked with LFS.
    /// </summary>
    /// <param name="binaryFiles">List of binary files in the repository.</param>
    /// <param name="lfsTrackedFiles">List of files tracked with LFS in the repository.</param>
    /// <returns>List of binary files in the repository which are not tracked with LFS.</returns>
    private List<string> DetermineBinaryFilesNotTrackedWithLfs(IEnumerable<string> binaryFiles, IEnumerable<string> lfsTrackedFiles)
    {
        this.Log.Verbose("Checking if binary files are tracked by LFS...");

        var binaryFilesNotTrackedWithLfs = binaryFiles.Except(lfsTrackedFiles).ToList();

        if (binaryFilesNotTrackedWithLfs.Count > 0)
        {
            this.Log.Debug(string.Join(Environment.NewLine, binaryFilesNotTrackedWithLfs));
        }

        this.Log.Verbose("Found {0} binary file(s) not tracked by LFS", binaryFilesNotTrackedWithLfs.Count);

        return binaryFilesNotTrackedWithLfs;
    }

    /// <summary>
    /// Determines if a file should be excluded from the specified check type.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="checkType">The type of check being performed.</param>
    /// <returns>True if the file should be excluded; otherwise, false.</returns>
    private bool IsFileExcluded(string filePath, CheckType checkType)
    {
        // Check global exclusions first
        if (FilePatternMatcher.IsMatch(filePath, this.IssueProviderSettings.FilesToExclude))
        {
            return true;
        }

        // Check specific exclusions based on check type
        return checkType switch
        {
            CheckType.BinaryFilesLfs => FilePatternMatcher.IsMatch(filePath, this.IssueProviderSettings.FilesToExcludeFromBinaryFilesLfsCheck),
            CheckType.FilePathLength => FilePatternMatcher.IsMatch(filePath, this.IssueProviderSettings.FilesToExcludeFromFilePathLengthCheck),
            _ => false,
        };
    }
}