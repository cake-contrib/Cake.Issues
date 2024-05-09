namespace Cake.Issues.GitRepository.Tests
{
    using Cake.Testing.Fixtures;

    internal class GitRunnerFixture : ToolFixture<GitRunnerSettings>
    {
        private readonly List<string> standardOutput = [];

        public GitRunnerFixture()
            : base("git")
        {
        }

        public IEnumerable<string> StandardOutput => this.standardOutput;

        protected override void RunTool()
        {
            this.standardOutput.Clear();

            var tool = new GitRunner(this.FileSystem, this.Environment, this.ProcessRunner, this.Tools);

            var output = tool.RunCommand(this.Settings);

            if (output != null)
            {
                this.standardOutput.AddRange(output);
            }
        }
    }
}