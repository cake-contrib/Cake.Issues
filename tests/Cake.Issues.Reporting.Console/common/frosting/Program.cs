using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Frosting;
using Spectre.Console;
using System.Text.RegularExpressions;

public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<BuildContext>()
            .Run(args);
    }
}

public class BuildContext : FrostingContext
{
    public BuildContext(ICakeContext context)
        : base(context)
    {
    }
}

public static class ConsoleExample
{
    public static void CreateReport(BuildContext context, ConsoleIssueReportFormatSettings settings)
    {
        var commonDirectory = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../common"));
        var issues = context.DeserializeIssuesFromJsonFile(Path.Combine(commonDirectory, "sample-issues.json")).ToList();
        var repositoryRoot = Path.Combine(commonDirectory, "repository");
        foreach (var issue in issues)
        {
            var source = File.ReadAllLines(Path.Combine(repositoryRoot, issue.AffectedFileRelativePath.FullPath));
            if (!issue.Line.HasValue || !issue.Column.HasValue ||
                issue.Line.Value < 1 || issue.Line.Value > source.Length ||
                issue.Column.Value < 1 || issue.Column.Value > source[issue.Line.Value - 1].Length ||
                (issue.EndColumn.HasValue && (issue.EndColumn.Value < issue.Column.Value ||
                    issue.EndColumn.Value > source[issue.Line.Value - 1].Length + 1)))
            {
                throw new InvalidOperationException($"Invalid fixture location for {issue.Identifier}");
            }
        }

        using var writer = new StringWriter();
        var originalConsole = AnsiConsole.Console;
        try
        {
            var capture = AnsiConsole.Create(new AnsiConsoleSettings
            {
                Ansi = AnsiSupport.Yes,
                ColorSystem = ColorSystemSupport.TrueColor,
                Out = new AnsiConsoleOutput(writer),
            });
            capture.Profile.Width = 100;
            AnsiConsole.Console = capture;
            context.CreateIssueReport(issues, context.ConsoleIssueReportFormat(settings), repositoryRoot, string.Empty);
        }
        finally
        {
            AnsiConsole.Console = originalConsole;
        }

        var output = writer.ToString();
        var plainOutput = Regex.Replace(output, @"\x1b\[[0-9;]*m", "");
        var expectedCount = settings.GroupByRule ? 1 : 2;
        if (plainOutput.Contains("Errata Error") || plainOutput.Contains("An error occurred when rendering a diagnostic") ||
            plainOutput.Contains("Exception") ||
            Regex.Matches(plainOutput, @"Warning: CS0219").Count != expectedCount ||
            Regex.Matches(plainOutput, @"Suggestion: CA1822").Count != expectedCount ||
            plainOutput.Contains("Issues per provider & run") != settings.ShowProviderSummary ||
            plainOutput.Contains("Issues per priority") != settings.ShowPrioritySummary)
        {
            throw new InvalidOperationException("Console example did not render the expected diagnostics successfully:\n" + output);
        }

        Console.Write(output);
    }
}

[TaskName("Print-Issues")]
public sealed class PrintIssuesTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var issues = context.DeserializeIssuesFromJsonFile(@"../../../../issues.json");
        context.Information("Read {0} issues", issues.Count());
        context.CreateIssueReport(
            issues,
            context.ConsoleIssueReportFormat(
                new ConsoleIssueReportFormatSettings
                {
                    ShowProviderSummary = true,
                    ShowPrioritySummary = true
                }),
            @"../",
            string.Empty);
    }
}

[TaskName("Create-Reports-Console-Default")]
public sealed class CreateReportsConsoleDefaultTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context) =>
        ConsoleExample.CreateReport(context, new ConsoleIssueReportFormatSettings());
}

[TaskName("Create-Reports-Console-Grouped")]
public sealed class CreateReportsConsoleGroupedTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context) =>
        ConsoleExample.CreateReport(context, new ConsoleIssueReportFormatSettings { GroupByRule = true });
}

[TaskName("Create-Reports-Console-Summaries")]
public sealed class CreateReportsConsoleSummariesTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context) =>
        ConsoleExample.CreateReport(context, new ConsoleIssueReportFormatSettings
        {
            ShowProviderSummary = true,
            ShowPrioritySummary = true,
        });
}

[TaskName("Create-Reports-Console-Combined")]
public sealed class CreateReportsConsoleCombinedTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context) =>
        ConsoleExample.CreateReport(context, new ConsoleIssueReportFormatSettings
        {
            GroupByRule = true,
            ShowProviderSummary = true,
            ShowPrioritySummary = true,
        });
}

[TaskName("Create-Reports-Console-Examples")]
[IsDependentOn(typeof(CreateReportsConsoleDefaultTask))]
[IsDependentOn(typeof(CreateReportsConsoleGroupedTask))]
[IsDependentOn(typeof(CreateReportsConsoleSummariesTask))]
[IsDependentOn(typeof(CreateReportsConsoleCombinedTask))]
public sealed class CreateReportsConsoleExamplesTask : FrostingTask
{
}

[TaskName("Default")]
[IsDependentOn(typeof(PrintIssuesTask))]
[IsDependentOn(typeof(CreateReportsConsoleExamplesTask))]
public sealed class DefaultTask : FrostingTask
{
}
