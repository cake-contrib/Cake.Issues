using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

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

[TaskName("Read-Issues")]
public sealed class ReadIssuesTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var junitLogPath = @"../testdata/cpplint.xml";

        var issues = context.ReadIssues(
            context.JUnitIssuesFromFilePath(junitLogPath, context.CppLintJUnitLogFileFormat()),
            @"../");

        context.Information("Found {0} issues", issues.Count());

        // Validate that we found expected issues
        if (issues.Count() != 2)
        {
            throw new Exception($"Expected 2 issues but found {issues.Count()}");
        }

        context.Information("All validation checks passed!");
    }
}

[TaskName("Default")]
[IsDependentOn(typeof(ReadIssuesTask))]
public class DefaultTask : FrostingTask
{
}