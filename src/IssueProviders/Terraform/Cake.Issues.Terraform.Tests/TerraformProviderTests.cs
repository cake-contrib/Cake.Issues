namespace Cake.Issues.Terraform.Tests;

using System.Runtime.InteropServices;

public sealed class TerraformProviderTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() =>
                new TerraformIssuesProvider(
                    null,
                    new TerraformIssuesSettings("Foo".ToByteArray(), @"c:\Source\Cake.Issues")));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Throw_If_IssueProviderSettings_Are_Null()
        {
            var result = Record.Exception(() =>
                new TerraformIssuesProvider(
                    new FakeLog(),
                    null));

            // Then
            result.IsArgumentNullException("issueProviderSettings");
        }
    }

    // Test cases based on https://github.com/hashicorp/terraform-json/blob/master/validate_test.go
    public sealed class TheReadIssuesMethod
    {
        [SkippableFact]
        public void Should_Read_Basic_Issues_Correct()
        {
            // Uses Windows specific paths.
            Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

            // Given
            var fixture = new TerraformProviderFixture("basic.json", "./");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);
            IssueChecker.Check(
                issues[0],
                IssueBuilder.NewIssue(
                    "\"anonymous\": [DEPRECATED] For versions later than 3.0.0, absence of a token enables this mode",
                    "Cake.Issues.Terraform.TerraformIssuesProvider",
                    "Terraform")
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[1],
                IssueBuilder.NewIssue(
                    "The argument \"name\" is required, but no definition was found.",
                    "Cake.Issues.Terraform.TerraformIssuesProvider",
                    "Terraform")
                    .InFile("main.tf", 14, 14, 37, 37)
                    .OfRule("Missing required argument")
                    .WithPriority(IssuePriority.Error));
        }

        [SkippableFact]
        public void Should_Read_Error_Correct()
        {
            // Uses Windows specific paths.
            Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

            // Given
            var fixture = new TerraformProviderFixture("error.json", "./");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            IssueChecker.Check(
                issues[0],
                IssueBuilder.NewIssue(
                    "\nPlugin reinitialization required...",
                    "Cake.Issues.Terraform.TerraformIssuesProvider",
                    "Terraform")
                    .OfRule("Could not load plugin")
                    .WithPriority(IssuePriority.Error));
        }

        [SkippableFact]
        public void Should_Read_Warning_Across_Multiple_Lines_Correct()
        {
            // Uses Windows specific paths.
            Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

            // Given
            var fixture = new TerraformProviderFixture("warning-across-multiple-lines.json", "./");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            IssueChecker.Check(
                issues[0],
                IssueBuilder.NewIssue(
                    "This feature is deprecated and will be removed in a major release. Please use the `lifecycle.ignore_changes` argument to specify the fields in `body` to ignore.",
                    "Cake.Issues.Terraform.TerraformIssuesProvider",
                    "Terraform")
                    .InFile("modules\\deployment-ring\\storage-account.tf", 84, 86, 25, 4)
                    .OfRule("Attribute Deprecated")
                    .WithPriority(IssuePriority.Warning));
        }
    }
}
