using Cake.Frosting;

[TaskName("Lint")]
[IsDependentOn(typeof(RunInspectCodeTask))]
[IsDependentOn(typeof(IssuesTask))]
public class LintTask : FrostingTask
{
}
