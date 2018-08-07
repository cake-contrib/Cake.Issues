namespace Cake.Issues.DocFx.Tests
{
    using System.Collections.Generic;
    using Cake.Core.IO;
    using Cake.Issues.Testing;

    internal class DocFxProviderFixture : BaseConfigurableIssueProviderFixture<DocFxIssuesProvider, DocFxIssuesSettings>
    {
        private readonly DirectoryPath docRootPath;

        public DocFxProviderFixture(string fileResourceName, DirectoryPath docRootPath)
            : base(fileResourceName)
        {
            docRootPath.NotNull(nameof(docRootPath));

            this.docRootPath = docRootPath;
            this.RepositorySettings =
                new RepositorySettings(@"c:\Source\Cake.Issues");
        }

        protected override string FileResourceNamespace => "Cake.Issues.DocFx.Tests.Testfiles.";

        protected override IList<object> GetCreateIssueProviderSettingsArguments()
        {
            var result = base.GetCreateIssueProviderSettingsArguments();
            result.Add(this.docRootPath);
            return result;
        }
    }
}
