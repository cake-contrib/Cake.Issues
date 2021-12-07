using Cake.Frosting;
using Cake.Issues;

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