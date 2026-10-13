using Cake.Frosting;
using Cake.Frosting.Issues.Recipe;

[TaskName("CI")]
[IsDependentOn(typeof(TestTask))]
[IsDependentOn(typeof(CreateNuGetPackagesTask))]
[IsDependentOn(typeof(IssuesTask))]
[IsDependentOn(typeof(PublishCodeCoverageTask))]
public sealed class ContinuousIntegrationTask : FrostingTask
{
}

[TaskName("Default")]
[IsDependentOn(typeof(ContinuousIntegrationTask))]
public sealed class DefaultTask : FrostingTask
{
}
