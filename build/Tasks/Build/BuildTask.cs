using Cake.Frosting;

[TaskName("Build")]
[IsDependentOn(typeof(BuildSolutionTask))]
public sealed class BuildTask : FrostingTask
{
}
