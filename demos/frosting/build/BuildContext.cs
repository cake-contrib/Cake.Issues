using System.Collections.Generic;
using Cake.Common.IO;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;
using Cake.Issues;

public class BuildContext : FrostingContext
{
    public DirectoryPath RepoRootFolder { get; }
    public DirectoryPath SourceFolder { get; }
    public DirectoryPath DocsFolder { get; }
    public DirectoryPath TemplateGalleryFolder { get; }
    public List<IIssue> Issues { get; }

    public BuildContext(ICakeContext context)
        : base(context)
    {
        this.RepoRootFolder = context.MakeAbsolute(context.Directory("./.."));
        this.SourceFolder = this.RepoRootFolder.Combine("src");
        this.DocsFolder = this.RepoRootFolder.Combine("docs");
        this.TemplateGalleryFolder = this.RepoRootFolder.Combine("../../docs/templates");

        this.Issues = new List<IIssue>();
    }
}