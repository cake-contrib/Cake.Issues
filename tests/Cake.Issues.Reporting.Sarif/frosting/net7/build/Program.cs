using Cake.Common.IO;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;
using Cake.Issues;
using Cake.Issues.Reporting;
using Cake.Issues.Reporting.Sarif;
using System.Collections.Generic;

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
    public DirectoryPath RepoRootFolder { get; }
    public List<IIssue> Issues { get; }

    public BuildContext(ICakeContext context)
        : base(context)
    {
        this.RepoRootFolder = context.MakeAbsolute(context.Directory("./../.."));
        this.Issues = new List<IIssue>();
    }
}

[TaskName("Create-CustomIssues")]
public class CreateCustomIssuesTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.Issues.Add(
            context.NewIssue(
                "Something went wrong",
                "MyCakeScript",
                "My Cake Script")
                .WithMessageInHtmlFormat("Something went <b>wrong</b>")
                .WithMessageInMarkdownFormat("Something went **wrong**")
                .InFile("myfile.txt", 42)
                .WithPriority(IssuePriority.Warning)
                .Create()
        );
    }
}

[TaskName("Analyze")]
[IsDependentOn(typeof(CreateCustomIssuesTask))]
public class AnalyzeTask : FrostingTask
{
}

[TaskName("Create-Report")]
[IsDependentOn(typeof(AnalyzeTask))]
public class CreateReport : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var outputFolder = context.RepoRootFolder.Combine("output");
        context.EnsureDirectoryExists(outputFolder);

        context.CreateIssueReport(
            context.Issues,
            context.SarifIssueReportFormat(),
            context.RepoRootFolder,
            outputFolder.CombineWithFilePath("issues.sarif")); 
    }
}

[TaskName("Default")]
[IsDependentOn(typeof(CreateReport))]
public class DefaultTask : FrostingTask
{
}