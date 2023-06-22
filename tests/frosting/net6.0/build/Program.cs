using System.Linq;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Frosting;
using Cake.Issues;
using Cake.Issues.Reporting;
using Cake.Issues.Reporting.Console;

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

[TaskName("Print-Issues")]
public sealed class PrintIssuesTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var issues = context.DeserializeIssuesFromJsonFile(@"../../../../src/Cake.Issues.Reporting.Console.Tests/Testfiles/issues.json");
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

[TaskName("Default")]
[IsDependentOn(typeof(PrintIssuesTask))]
public class DefaultTask : FrostingTask
{
}