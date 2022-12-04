using Cake.Frosting;

[TaskName("Analyze")]
[IsDependentOn(typeof(CreateCustomIssuesTask))]
public class AnalyzeTask : FrostingTask
{
}