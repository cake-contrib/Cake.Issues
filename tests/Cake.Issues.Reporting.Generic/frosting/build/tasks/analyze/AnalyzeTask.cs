using Cake.Frosting;
using Cake.Issues;

[TaskName("Analyze")]
public class AnalyzeTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.Issues.AddRange(
            context.DeserializeIssuesFromJsonFile(
                context.RepoRootFolder
                    .Combine("..")
                    .CombineWithFilePath("issues.json"))
        );
    }
}