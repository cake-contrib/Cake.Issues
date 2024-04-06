#load dupfinder.cake
#load inspectcode.cake
#load markdownlint.cake
#load custom-issue.cake

Task("Analyze")
    .IsDependentOn("Build")
    .IsDependentOn("Run-DupFinder")
    .IsDependentOn("Run-InspectCode")
    .IsDependentOn("Lint-Documentation")
    .IsDependentOn("Create-CustomIssues");