using Cake.Frosting;

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