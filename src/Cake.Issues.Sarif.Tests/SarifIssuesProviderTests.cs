namespace Cake.Issues.Sarif.Tests;

public sealed class SarifIssuesProviderTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() =>
                new SarifIssuesProvider(
                    null,
                    new SarifIssuesSettings("Foo".ToByteArray())));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Throw_If_IssueProviderSettings_Are_Null()
        {
            // Given / When
            var result = Record.Exception(() => new SarifIssuesProvider(new FakeLog(), null));

            // Then
            result.IsArgumentNullException("issueProviderSettings");
        }
    }

    public sealed class TheReadIssuesMethod
    {
        [Fact]
        public void Should_Read_Issue_Correct_For_Minimal_File()
        {
            // Given
            var fixture = new SarifIssuesProviderFixture("minimal.sarif");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(0);
        }

        [Fact]
        public void Should_Read_Issue_Correct_For_Recommended_File_Without_Source()
        {
            // Given
            var fixture = new SarifIssuesProviderFixture("recommended-without-source.sarif");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            var issue = issues.Single();
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "The insecure method \"Crypto.Sha1.Encrypt\" should not be used.",
                    "Cake.Issues.Sarif.SarifIssuesProvider",
                    "BinaryScanner")
                    .OfRule("B6412")
                    .WithPriority(IssuePriority.Warning)
                    .Create());
        }

        [Fact]
        public void Should_Read_Issue_Correct_For_Recommended_File_With_Source()
        {
            // Given
            var fixture = new SarifIssuesProviderFixture("recommended-with-source.sarif");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            var issue = issues.Single();
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Variable \"count\" was used without being initialized.",
                    "Cake.Issues.Sarif.SarifIssuesProvider",
                    "CodeScanner")
                    .InFile(@"src\collections\list.cpp", 15)
                    .OfRule("C2001")
                    .WithPriority(IssuePriority.Warning)
                    .Create());
        }

        [Fact]
        public void Should_Read_Issue_Correct_For_Comprehensive_File()
        {
            // Given
            var fixture = new SarifIssuesProviderFixture("comprehensive.sarif");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            var issue = issues.Single();
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Variable \"ptr\" was used without being initialized." + Environment.NewLine +
                    "                           It was declared [here](0).",
                    "Cake.Issues.Sarif.SarifIssuesProvider",
                    "CodeScanner")
                    .WithMessageInMarkdownFormat(
                    "Variable `ptr` was used without being initialized." + Environment.NewLine +
                    "                           It was declared [here](0).")
                    .InFile(@"collections\list.h", 15, 15, 9, 10)
                    .OfRule("C2001")
                    .WithPriority(IssuePriority.Error)
                    .Create());
        }

        [Fact]
        public void Should_Consider_UseToolNameAsIssueProviderName()
        {
            // Given
            var fixture = new SarifIssuesProviderFixture("recommended-without-source.sarif")
            {
                UseToolNameAsIssueProviderName = false
            };

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            var issue = issues.Single();
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "The insecure method \"Crypto.Sha1.Encrypt\" should not be used.",
                    "Cake.Issues.Sarif.SarifIssuesProvider",
                    "SARIF")
                    .OfRule("B6412")
                    .WithPriority(IssuePriority.Warning)
                    .Create());
        }

        [Fact]
        public void Should_Read_Issue_Correct_For_File_Generated_By_CakeIssuesReportingSarif()
        {
            // Given
            var fixture = new SarifIssuesProviderFixture("cake.issues.reporting.sarif.sarif");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(3);

            var issue = issues[0];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Message Foo.",
                    "Cake.Issues.Sarif.SarifIssuesProvider",
                    "ProviderType Foo")
                    .InFile("src/Cake.Issues.Reporting.Sarif.Tests/SarifIssueReportGeneratorTests.cs", 10)
                    .OfRule("Rule Foo")
                    .WithPriority(IssuePriority.Error)
                    .Create());

            issue = issues[1];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Message Bar.",
                    "Cake.Issues.Sarif.SarifIssuesProvider",
                    "ProviderType Bar")
                    .WithMessageInMarkdownFormat("Message Bar -- now in **Markdown**!")
                    .InFile("src/Cake.Issues.Reporting.Sarif.Tests/SarifIssueReportGeneratorTests.cs", 12)
                    .OfRule("Rule Bar", new Uri("https://www.example.come/rules/bar.html"))
                    .WithPriority(IssuePriority.Warning)
                    .Create());

            issue = issues[2];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Message Bar 2.",
                    "Cake.Issues.Sarif.SarifIssuesProvider",
                    "ProviderType Bar")
                    .InFile("src/Cake.Issues.Reporting.Sarif.Tests/SarifIssueReportGeneratorTests.cs", 42)
                    .WithPriority(IssuePriority.Warning)
                    .Create());
        }

        [Fact]
        public void Should_Read_Issue_Correct_For_File_Generated_By_TFLint()
        {
            // Given
            var fixture = new SarifIssuesProviderFixture("tflint.sarif");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(4);

            var issue = issues[0];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Missing version constraint for provider \"azurerm\" in `required_providers`",
                    "Cake.Issues.Sarif.SarifIssuesProvider",
                    "tflint")
                    .InFile(@"modules\my-module\main.tf", 1, 1, 1, 41)
                    .OfRule(
                        "terraform_required_providers",
                        new Uri("https://github.com/terraform-linters/tflint-ruleset-terraform/blob/v0.8.0/docs/rules/terraform_required_providers.md"))
                    .WithPriority(IssuePriority.Warning)
                    .Create());

            issue = issues[1];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "terraform \"required_version\" attribute is required",
                    "Cake.Issues.Sarif.SarifIssuesProvider",
                    "tflint")
                    .InFile(@"modules\my-module\main.tf")
                    .OfRule(
                        "terraform_required_version",
                        new Uri("https://github.com/terraform-linters/tflint-ruleset-terraform/blob/v0.8.0/docs/rules/terraform_required_version.md"))
                    .WithPriority(IssuePriority.Warning)
                    .Create());

            issue = issues[2];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "`foo` variable has no type",
                    "Cake.Issues.Sarif.SarifIssuesProvider",
                    "tflint")
                    .InFile(@"modules\my-module\variables.tf", 96, 96, 1, 20)
                    .OfRule(
                        "terraform_typed_variables",
                        new Uri("https://github.com/terraform-linters/tflint-ruleset-terraform/blob/v0.8.0/docs/rules/terraform_typed_variables.md"))
                    .WithPriority(IssuePriority.Warning)
                    .Create());

            issue = issues[3];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "variable \"foo\" is declared but not used",
                    "Cake.Issues.Sarif.SarifIssuesProvider",
                    "tflint")
                    .InFile(@"modules\my-module\variables.tf", 16, 16, 1, 34)
                    .OfRule(
                        "terraform_unused_declarations",
                        new Uri("https://github.com/terraform-linters/tflint-ruleset-terraform/blob/v0.8.0/docs/rules/terraform_unused_declarations.md"))
                    .WithPriority(IssuePriority.Warning)
                    .Create());
        }

        [Fact]
        public void Should_Read_Issue_Correct_For_File_Generated_By_InspectCode()
        {
            // Given
            var fixture = new SarifIssuesProviderFixture("inspectcode.sarif");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1106);
        }
    }
}
