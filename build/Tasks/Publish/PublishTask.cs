using Cake.Frosting;

[TaskName("Publish")]
[IsDependentOn(typeof(PublishCodeCoverageTask))]
[IsDependentOn(typeof(PublishNuGetPackagesTask))]
public sealed class PublishTask : FrostingTask
{
}
