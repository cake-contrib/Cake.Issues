#addin "Cake.Issues&prerelease"
#addin "Cake.Issues.MsBuild&prerelease"
#addin "Cake.Issues.Markdownlint&prerelease"
#addin "Cake.Issues.InspectCode&prerelease"
#addin "Cake.Issues.Reporting&prerelease"
#addin "Cake.Issues.Reporting.Generic&prerelease"

#load build/build/build.cake
#load build/analyze/analyze.cake
#load build/create-reports/create-reports.cake

var target = Argument("target", "Default");

public class BuildData
{
	public DirectoryPath RepoRootFolder { get; }
	public DirectoryPath SourceFolder { get; }
	public DirectoryPath DocsFolder { get; }
	public DirectoryPath TemplateGalleryFolder { get; }
	public List<IIssue> Issues { get; }

	public BuildData(ICakeContext context)
	{
        this.RepoRootFolder = context.MakeAbsolute(context.Directory("./"));
        this.SourceFolder = this.RepoRootFolder.Combine("src");
        this.DocsFolder = this.RepoRootFolder.Combine("docs");
        this.TemplateGalleryFolder = this.RepoRootFolder.Combine("../docs/templates");
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
