using Cake.Common.IO;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

public class BuildContext : FrostingContext
{
    public DirectoryPath RepoRootFolder { get; }
    public DirectoryPath TemplateGalleryFolder { get; }
    public List<IIssue> Issues { get; }

    public BuildContext(ICakeContext context)
        : base(context)
    {
        this.RepoRootFolder = context.MakeAbsolute(context.Directory("./../.."));
        this.TemplateGalleryFolder = this.RepoRootFolder.Combine("../../docs/input/documentation/report-formats/generic/templates");
 
        this.Issues = new List<IIssue>();
    }
}