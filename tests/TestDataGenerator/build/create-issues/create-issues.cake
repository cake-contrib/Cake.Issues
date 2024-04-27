Task("Create-Issues")
    .Description("Creates serialized issues file")
    .IsDependentOn("Analyze")
    .Does<BuildData>(data =>
{
    SerializeIssuesToJsonFile(
        data.Issues, 
        data.RepoRootFolder
            .Combine("..")
            .CombineWithFilePath("issues.json"));
});