#addin "Cake.Issues&prerelease"
#addin "Cake.Issues.Reporting&prerelease"
#addin "Cake.Issues.Reporting.Sarif&prerelease"

#load build/analyze/analyze.cake
#load build/create-reports/create-reports.cake

var target = Argument("target", "Default");

public class BuildData
{
    public DirectoryPath RepoRootFolder { get; }
    public DirectoryPath OutputFolder { get; }
    public List<IIssue> Issues { get; }

    public BuildData(ICakeContext context)
    {
        this.RepoRootFolder = context.MakeAbsolute(context.Directory("./"));
        this.OutputFolder = this.RepoRootFolder.Combine("output");

        this.Issues = new List<IIssue>();
    }
}

Setup<BuildData>(setupContext =>
{
    return new BuildData(setupContext);
});

Task("Default")
    .IsDependentOn("Create-Reports");

RunTarget(target);
