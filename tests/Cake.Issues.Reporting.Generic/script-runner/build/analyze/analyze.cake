Task("Analyze")
    .Description("Reads issues from test data")
    .Does<BuildData>(data =>
{
    data.Issues.AddRange(
        DeserializeIssuesFromJsonFile(
            data.RepoRootFolder
                .Combine("..")
                .Combine("..")
                .CombineWithFilePath("issues.json")));
});