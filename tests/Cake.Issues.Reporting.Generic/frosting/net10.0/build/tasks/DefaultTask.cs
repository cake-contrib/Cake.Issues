using Cake.Frosting;

[TaskName("Default")]
[IsDependentOn(typeof(CreateReportsTask))]
public class DefaultTask : FrostingTask
{
}