using Cake.Frosting;

[TaskName("Test")]
[IsDependentOn(typeof(TestSolutionTask))]
public sealed class TestTask : FrostingTask
{
}
