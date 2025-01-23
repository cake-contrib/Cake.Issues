using Cake.Frosting;

[TaskName("Default")]
[IsDependentOn(typeof(LintTask))]
[IsDependentOn(typeof(PackageTask))]
[IsDependentOn(typeof(TestTask))]
public sealed class DefaultTask : FrostingTask
{
}
