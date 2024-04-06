using Cake.Frosting;

[TaskName("Create-Reports-HtmlDiagnostic")]
[IsDependentOn(typeof(CreateReportsHtmlDiagnosticDefaultTask))]
public class CreateReportsHtmlDiagnosticTask : FrostingTask
{
}