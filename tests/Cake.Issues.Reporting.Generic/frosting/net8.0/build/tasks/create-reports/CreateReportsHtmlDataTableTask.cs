using Cake.Frosting;

[TaskName("Create-Reports-HtmlDataTable")]
[IsDependentOn(typeof(CreateReportsHtmlDataTableDefaultTask))]
public class CreateReportsHtmlDataTableTask : FrostingTask
{
}