Task("Create-CustomIssues")
    .Description("Creates additional issues in the build script")
    .Does<BuildData>(data =>
{
    data.Issues.Add(
        NewIssue(
            "Something went wrong",
            "MyCakeScript",
            "My Cake Script")
            .InFile("myfile.txt", 42)
            .WithPriority(IssuePriority.Warning)
            .Create()
    );
});