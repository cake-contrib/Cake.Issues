using Cake.Frosting;

[TaskName("Create-ReportsTask")]
[IsDependentOn(typeof(CreateReportsHtmlDiagnosticTask))]
[IsDependentOn(typeof(CreateReportsHtmlDataTableTask))]
[IsDependentOn(typeof(CreateReportsHtmlDxDataGridTask))]
public class CreateReportsTask : FrostingTask
{
}