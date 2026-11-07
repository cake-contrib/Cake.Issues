#addin "Cake.Issues&prerelease"
#addin "Cake.Issues.Reporting&prerelease"
#addin "Cake.Issues.Reporting.Console&prerelease"

using Spectre.Console;

var target = Argument("target", "Default");

void CreateConsoleExample(ConsoleIssueReportFormatSettings settings)
{
    var commonDirectory = MakeAbsolute(Directory(@"../../common"));
    var issues = DeserializeIssuesFromJsonFile(commonDirectory.CombineWithFilePath("sample-issues.json")).ToList();
    var repositoryRoot = commonDirectory.Combine("repository");
    foreach (var issue in issues)
    {
        var source = System.IO.File.ReadAllLines(repositoryRoot.CombineWithFilePath(issue.AffectedFileRelativePath).FullPath);
        if (!issue.Line.HasValue || !issue.Column.HasValue ||
            issue.Line.Value < 1 || issue.Line.Value > source.Length ||
            issue.Column.Value < 1 || issue.Column.Value > source[issue.Line.Value - 1].Length ||
            (issue.EndColumn.HasValue && (issue.EndColumn.Value < issue.Column.Value ||
                issue.EndColumn.Value > source[issue.Line.Value - 1].Length + 1)))
        {
            throw new Exception($"Invalid fixture location for {issue.Identifier}");
        }
    }

    using (var writer = new System.IO.StringWriter())
    {
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
            CreateIssueReport(issues, ConsoleIssueReportFormat(settings), repositoryRoot.FullPath, string.Empty);
        }
        finally
        {
            AnsiConsole.Console = originalConsole;
        }

        var output = writer.ToString();
        var plainOutput = System.Text.RegularExpressions.Regex.Replace(output, @"\x1b\[[0-9;]*m", "");
        var expectedCount = settings.GroupByRule ? 1 : 2;
        if (plainOutput.Contains("Errata Error") || plainOutput.Contains("An error occurred when rendering a diagnostic") ||
            plainOutput.Contains("Exception") ||
            System.Text.RegularExpressions.Regex.Matches(plainOutput, @"Warning: CS0219").Count != expectedCount ||
            System.Text.RegularExpressions.Regex.Matches(plainOutput, @"Suggestion: CA1822").Count != expectedCount ||
            plainOutput.Contains("Issues per provider & run") != settings.ShowProviderSummary ||
            plainOutput.Contains("Issues per priority") != settings.ShowPrioritySummary)
        {
            throw new Exception("Console example did not render the expected diagnostics successfully:\n" + output);
        }

        var outputFile = Argument<string>("report-output", null);
        if (outputFile != null)
        {
            System.IO.File.WriteAllText(outputFile, output, new System.Text.UTF8Encoding(false));
        }
        else
        {
            System.Console.Write(output);
        }
    }
}

Task("Print-Issues")
    .Does(() =>
{
    var issues = DeserializeIssuesFromJsonFile(@"../../../issues.json");
    Information("Read {0} issues", issues.Count());
    CreateIssueReport(
        issues,
        ConsoleIssueReportFormat(
            new ConsoleIssueReportFormatSettings
            {
                ShowProviderSummary = true,
                ShowPrioritySummary = true
            }),
        @".",
        string.Empty);
});

Task("Create-Reports-Console-Default")
    .Does(() => CreateConsoleExample(new ConsoleIssueReportFormatSettings()));

Task("Create-Reports-Console-Grouped")
    .Does(() => CreateConsoleExample(new ConsoleIssueReportFormatSettings
    {
        GroupByRule = true,
    }));

Task("Create-Reports-Console-Summaries")
    .Does(() => CreateConsoleExample(new ConsoleIssueReportFormatSettings
    {
        ShowProviderSummary = true,
        ShowPrioritySummary = true,
    }));

Task("Create-Reports-Console-Combined")
    .Does(() => CreateConsoleExample(new ConsoleIssueReportFormatSettings
    {
        GroupByRule = true,
        ShowProviderSummary = true,
        ShowPrioritySummary = true,
    }));

Task("Create-Reports-Console-Examples")
    .IsDependentOn("Create-Reports-Console-Default")
    .IsDependentOn("Create-Reports-Console-Grouped")
    .IsDependentOn("Create-Reports-Console-Summaries")
    .IsDependentOn("Create-Reports-Console-Combined");

Task("Default")
    .IsDependentOn("Print-Issues")
    .IsDependentOn("Create-Reports-Console-Examples");

RunTarget(target);
