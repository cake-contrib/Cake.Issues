using Cake.Frosting;

[TaskName("Package")]
[IsDependentOn(typeof(CreateNuGetPackagesTask))]
public sealed class PackageTask : FrostingTask
{
}
