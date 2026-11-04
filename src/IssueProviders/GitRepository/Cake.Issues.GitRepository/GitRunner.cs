namespace Cake.Issues.GitRepository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

/// <summary>
/// A wrapper around the Git CLI.
/// </summary>
/// <param name="fileSystem">The file system.</param>
/// <param name="environment">The Cake environment.</param>
/// <param name="processRunner">The process runner.</param>
/// <param name="toolLocator">The tool locator.</param>
internal class GitRunner(
    IFileSystem fileSystem,
    ICakeEnvironment environment,
    IProcessRunner processRunner,
    IToolLocator toolLocator)
    : Tool<GitRunnerSettings>(fileSystem, environment, processRunner, toolLocator)
{
    /// <summary>
    /// Runs Git with specified settings.
    /// </summary>
    /// <param name="settings">Settings for running Git.</param>
    /// <returns>Output of command.</returns>
    public IEnumerable<string> RunCommand(GitRunnerSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        var args = new ProcessArgumentBuilder();
        settings.Evaluate(args);

        var processSettings = new ProcessSettings
        {
            WorkingDirectory = settings.WorkingDirectory,
            Arguments = args.Render(),
            RedirectStandardOutput = true,
        };

        var currentEncoding = Console.OutputEncoding;
        Console.OutputEncoding = Encoding.UTF8;
        try
        {
            IEnumerable<string> result = null;
            this.Run(
                settings,
                null,
                processSettings,
                process =>
                {
                    result = process.GetStandardOutput();
                });
            return result?.ToList();
        }
        finally
        {
            Console.OutputEncoding = currentEncoding;
        }
    }

    /// <summary>
    /// Gets the name of the tool.
    /// </summary>
    /// <returns>The name of the tool.</returns>
    protected override string GetToolName() => "Git";

    /// <summary>
    /// Gets the names of the tool executables.
    /// </summary>
    /// <returns>The tool executable names.</returns>
    protected override IEnumerable<string> GetToolExecutableNames()
    {
        yield return "git.exe";
        yield return "git";
    }
}
