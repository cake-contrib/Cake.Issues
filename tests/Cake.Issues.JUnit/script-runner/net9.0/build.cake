#load "buildData.cake"

#addin "Cake.Issues&prerelease"
#addin "Cake.Issues.JUnit&prerelease"

//////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////
// SETUP / TEARDOWN
//////////////////////////////////////////////////

Setup<BuildData>(setupContext =>
{
    return new BuildData();
});

var repoRootFolder = MakeAbsolute(Directory("./"));

//////////////////////////////////////////////////
// TARGETS
//////////////////////////////////////////////////

Task("ReadIssues")
    .Does<BuildData>((data) =>
{
    var junitLogPath = repoRootFolder.Combine("testdata").CombineWithFilePath("cpplint.xml");

    data.AddIssues(
        ReadIssues(
            JUnitIssuesFromFilePath(junitLogPath),
            repoRootFolder)
    );

    Information("Found {0} issues", data.Issues.Count());
    
    // Validate that we found expected issues
    if (data.Issues.Count() != 2)
    {
        throw new Exception($"Expected 2 issues but found {data.Issues.Count()}");
    }

    Information("All validation checks passed!");
});

// Run ReadIssues task by default.
Task("Default")
    .IsDependentOn("ReadIssues");

//////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////

RunTarget(target);