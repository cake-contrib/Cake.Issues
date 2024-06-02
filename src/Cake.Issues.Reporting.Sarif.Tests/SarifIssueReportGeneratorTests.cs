namespace Cake.Issues.Reporting.Sarif.Tests;

using Microsoft.CodeAnalysis.Sarif;
using Newtonsoft.Json;

public sealed class SarifIssueReportGeneratorTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() =>
                new SarifIssueReportGenerator(
                    null,
                    new SarifIssueReportFormatSettings()));

            // Then
            result.IsArgumentNullException("log");
        }

        [Fact]
        public void Should_Throw_If_Settings_Are_Null()
        {
            // Given / When
            var result = Record.Exception(() =>
                new SarifIssueReportGenerator(
                    new FakeLog(),
                    null));

            // Then
            result.IsArgumentNullException("settings");
        }
    }

    public sealed class TheInternalCreateReportMethod
    {
        [Fact]
        public void Should_Generate_Report()
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var issues =
                new List<IIssue>
                {
                    IssueBuilder
                        .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                        .InFile(@"src\Cake.Issues.Reporting.Sarif.Tests\SarifIssueReportGeneratorTests.cs", 10)
                        .InProjectFile(@"src\Cake.Issues.Reporting.Sarif.Tests\Cake.Issues.Reporting.Sarif.Tests.csproj")
                        .OfRule("Rule Foo")
                        .WithPriority(IssuePriority.Error)
                        .Create(),

                    // Issue to exercise creation of rule metadata with helpUri, and messages
                    // in Markdown format.
                    IssueBuilder
                        .NewIssue("Message Bar.", "ProviderType Bar", "ProviderName Bar")
                        .InFile(@"src\Cake.Issues.Reporting.Sarif.Tests\SarifIssueReportGeneratorTests.cs", 12, 5)
                        .OfRule("Rule Bar", new Uri("https://www.example.come/rules/bar.html"))
                        .WithPriority(IssuePriority.Warning)
                        .WithMessageInMarkdownFormat("Message Bar -- now in **Markdown**!")
                        .Create(),

                    // Issue to exercise the corner case where ruleId is absent (so no rule
                    // metadata is created) but helpUri is present (so it is stored in the
                    // result's property bag).
                    IssueBuilder
                        .NewIssue("Message Bar 2.", "ProviderType Bar", "ProviderName Bar")
                        .InFile(@"src\Cake.Issues.Reporting.Sarif.Tests\SarifIssueReportGeneratorTests.cs", 23, 42, 5, 10)
                        .OfRule(null, new Uri("https://www.example.come/rules/bar2.html"))
                        .WithPriority(IssuePriority.Warning)
                        .Create(),
                };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            // There are two runs because there are two issue providers.
            sarifLog.Runs.Count.ShouldBe(2);

            var run = sarifLog.Runs[0];
            run.Tool.Driver.Name.ShouldBe("ProviderType Foo");

            // This run doesn't have any rules that specify a help URI, so we didn't bother
            // adding rule metadata.
            run.Tool.Driver.Rules.ShouldBeNull();

            var result = run.Results.ShouldHaveSingleItem();
            result.RuleId.ShouldBe("Rule Foo");
            result.RuleIndex.ShouldBe(-1); // because there's no rule metadata to point to.
            result.Message.Text.ShouldBe("Message Foo.");
            result.Message.Markdown.ShouldBeNull();
            result.Level.ShouldBe(FailureLevel.Error);
            result.Kind.ShouldBe(ResultKind.Fail);

            var location = result.Locations.ShouldHaveSingleItem();
            var physicalLocation = location.PhysicalLocation;
            physicalLocation.ArtifactLocation.Uri.OriginalString.ShouldBe("src/Cake.Issues.Reporting.Sarif.Tests/SarifIssueReportGeneratorTests.cs");
            physicalLocation.Region.StartLine.ShouldBe(10);

            run.OriginalUriBaseIds.Count.ShouldBe(1);
            run.OriginalUriBaseIds[SarifIssueReportGenerator.RepoRootUriBaseId].Uri.LocalPath.ShouldBe(SarifIssueReportFixture.RepositoryRootPath);

            run = sarifLog.Runs[1];
            run.Tool.Driver.Name.ShouldBe("ProviderType Bar");

            // This run has a rule that specifies a help URI, so we added rule metadata.
            var rules = run.Tool.Driver.Rules;
            var rule = rules.ShouldHaveSingleItem();
            rule.Id.ShouldBe("Rule Bar");
            rule.HelpUri.OriginalString.ShouldBe("https://www.example.come/rules/bar.html");

            run.Results.Count.ShouldBe(2);

            result = run.Results[0];
            result.RuleId.ShouldBe("Rule Bar");
            result.RuleIndex.ShouldBe(0); // The index of the metadata for this rule in the rules array.
            result.Message.Text.ShouldBe("Message Bar.");
            result.Message.Markdown.ShouldBe("Message Bar -- now in **Markdown**!");
            result.Level.ShouldBe(FailureLevel.Warning);
            result.Kind.ShouldBe(ResultKind.Fail);

            location = result.Locations.ShouldHaveSingleItem();
            physicalLocation = location.PhysicalLocation;
            physicalLocation.ArtifactLocation.Uri.OriginalString.ShouldBe("src/Cake.Issues.Reporting.Sarif.Tests/SarifIssueReportGeneratorTests.cs");
            physicalLocation.Region.StartLine.ShouldBe(12);
            physicalLocation.Region.StartColumn.ShouldBe(5);

            // This run also includes an issue with a rule URL but no rule name, so we'll find
            // the rule URL in the result's property bag.
            result = run.Results[1];
            result.RuleId.ShouldBeNull();
            result.RuleIndex.ShouldBe(-1);
            result.GetProperty(SarifIssueReportGenerator.RuleUrlPropertyName).ShouldBe("https://www.example.come/rules/bar2.html");
            result.Message.Text.ShouldBe("Message Bar 2.");
            result.Level.ShouldBe(FailureLevel.Warning);
            result.Kind.ShouldBe(ResultKind.Fail);

            location = result.Locations.ShouldHaveSingleItem();
            physicalLocation = location.PhysicalLocation;
            physicalLocation.ArtifactLocation.Uri.OriginalString.ShouldBe("src/Cake.Issues.Reporting.Sarif.Tests/SarifIssueReportGeneratorTests.cs");
            physicalLocation.Region.StartLine.ShouldBe(23);
            physicalLocation.Region.EndLine.ShouldBe(42);
            physicalLocation.Region.StartColumn.ShouldBe(5);
            physicalLocation.Region.EndColumn.ShouldBe(10);

            run.OriginalUriBaseIds.Count.ShouldBe(1);
            run.OriginalUriBaseIds[SarifIssueReportGenerator.RepoRootUriBaseId].Uri.LocalPath.ShouldBe(SarifIssueReportFixture.RepositoryRootPath);
        }

        [Fact]
        public void Should_Have_Separate_Run_For_Every_Issue_Provider()
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var providerTypeA = "ProviderTypeA Foo";
            var providerTypeB = "ProviderTypeB Foo";
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", providerTypeA, "ProviderName Foo")
                            .Create(),
                        IssueBuilder
                            .NewIssue("Message Foo.", providerTypeB, "ProviderName Foo")
                            .Create(),
                        IssueBuilder
                            .NewIssue("Message Foo.", providerTypeA, "ProviderName Foo")
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            sarifLog.Runs.Count.ShouldBe(2);

            var runA = sarifLog.Runs[0];
            runA.Tool.Driver.Name.ShouldBe(providerTypeA);

            var runB = sarifLog.Runs[1];
            runB.Tool.Driver.Name.ShouldBe(providerTypeB);
        }

        [Fact]
        public void Should_Have_Separate_Run_For_Every_Issue_Provider_And_Run()
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var providerTypeA = "ProviderTypeA Foo";
            var providerTypeB = "ProviderTypeB Foo";
            var runDescription1 = "Run Foo";
            var runDescription2 = "Run Bar";
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", providerTypeA, "ProviderName Foo")
                            .ForRun(runDescription1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("Message Foo.", providerTypeB, "ProviderName Foo")
                            .ForRun(runDescription1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("Message Foo.", providerTypeA, "ProviderName Foo")
                            .ForRun(runDescription2)
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            sarifLog.Runs.Count.ShouldBe(3);

            var run1 = sarifLog.Runs[0];
            run1.Tool.Driver.Name.ShouldBe(providerTypeA);
            run1.AutomationDetails.Id.ShouldBe(runDescription1);

            var run2 = sarifLog.Runs[1];
            run2.Tool.Driver.Name.ShouldBe(providerTypeB);
            run2.AutomationDetails.Id.ShouldBe(runDescription1);

            var run3 = sarifLog.Runs[2];
            run3.Tool.Driver.Name.ShouldBe(providerTypeA);
            run3.AutomationDetails.Id.ShouldBe(runDescription2);
        }

        [Fact]
        public void Should_Set_Driver_Name()
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var providerType = "ProviderType Foo";
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", providerType, "ProviderName Foo")
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var run = sarifLog.Runs.ShouldHaveSingleItem();
            run.Tool.Driver.Name.ShouldBe(providerType);
        }

        [Fact]
        public void Should_Set_AutomationDetails_Id()
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var runDesciprtion = "Run Foo";
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .ForRun(runDesciprtion)
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var run = sarifLog.Runs.ShouldHaveSingleItem();
            run.AutomationDetails.Id.ShouldBe(runDesciprtion);
        }

        [Fact]
        public void Should_Set_AutomationDetails_Id_For_Different_Runs()
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var runDescription1 = "Run Foo";
            var runDescription2 = "Run Bar";
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .ForRun(runDescription1)
                            .Create(),
                        IssueBuilder
                            .NewIssue("Message Bar.", "ProviderType Bar", "ProviderName Bar")
                            .ForRun(runDescription2)
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            sarifLog.Runs.Count.ShouldBe(2);

            var run1 = sarifLog.Runs[0];
            run1.AutomationDetails.Id.ShouldBe(runDescription1);

            var run2 = sarifLog.Runs[1];
            run2.AutomationDetails.Id.ShouldBe(runDescription2);
        }


        [Fact]
        public void Should_Set_CorrelationGuid_If_Defined()
        {
            // Given
            var correlationGuid = Guid.NewGuid();
            var fixture = new SarifIssueReportFixture();
            fixture.SarifIssueReportFormatSettings.CorrelationGuid = correlationGuid;
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var run = sarifLog.Runs.ShouldHaveSingleItem();
            run.AutomationDetails.CorrelationGuid.ShouldBe(correlationGuid);
        }

        [Fact]
        public void Should_Not_Set_CorrelationGuid_If_Not_Defined()
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .ForRun("Run Foo")
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var run = sarifLog.Runs.ShouldHaveSingleItem();
            run.AutomationDetails.CorrelationGuid.ShouldBeNull();
        }

        [Fact]
        public void Should_Set_OriginalUriBaseIds()
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var run = sarifLog.Runs.ShouldHaveSingleItem();
            run.OriginalUriBaseIds.Count.ShouldBe(1);
            run.OriginalUriBaseIds[SarifIssueReportGenerator.RepoRootUriBaseId].Uri.LocalPath.ShouldBe(SarifIssueReportFixture.RepositoryRootPath);
        }

        [Fact]
        public void Should_Not_Set_Rules_If_No_RuleUrl()
        {
            // If run doesn't have any rules that specify a help URI, we shouldn't bother
            // adding rule metadata.

            // Given
            var fixture = new SarifIssueReportFixture();
            var providerType = "ProviderType Foo";
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", providerType, "ProviderName Foo")
                            .OfRule("Rule Foo")
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var run = sarifLog.Runs.ShouldHaveSingleItem();
            run.Tool.Driver.Rules.ShouldBeNull();
        }

        [Fact]
        public void Should_Set_Rules_If_One_RuleUrl()
        {
            // Runs which have a  rule that specifies a help URI should have added rule metadata.

            // Given
            var fixture = new SarifIssueReportFixture();
            var ruleId = "Rule Bar";
            var ruleUrl = "https://www.example.come/rules/bar.html";
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .OfRule(ruleId, new Uri(ruleUrl))
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var run = sarifLog.Runs.ShouldHaveSingleItem();
            var rule = run.Tool.Driver.Rules.ShouldHaveSingleItem();
            rule.Id.ShouldBe(ruleId);
            rule.HelpUri.OriginalString.ShouldBe(ruleUrl);
        }

        [Fact]
        public void Should_Set_Rules_If_Some_RuleUrl()
        {
            // Runs which have a  rule that specifies a help URI should have added rule metadata.

            // Given
            var fixture = new SarifIssueReportFixture();
            var ruleId = "Rule Bar";
            var ruleUrl = "https://www.example.come/rules/bar.html";
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .OfRule("Rule Foo")
                            .Create(),
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .OfRule(ruleId, new Uri(ruleUrl))
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var run = sarifLog.Runs.ShouldHaveSingleItem();
            var rule = run.Tool.Driver.Rules.ShouldHaveSingleItem();
            rule.Id.ShouldBe(ruleId);
            rule.HelpUri.OriginalString.ShouldBe(ruleUrl);
        }

        [Fact]
        public void Should_Set_Rules_If_RuleUrl_Without_RuleName()
        {
            // Run that include an issue with a rule URL but no rule name, should have rule URL in the result's property bag.

            // Given
            var fixture = new SarifIssueReportFixture();
            var ruleUrl = "https://www.example.come/rules/bar.html";
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .OfRule(null, new Uri(ruleUrl))
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var run = sarifLog.Runs.ShouldHaveSingleItem();
            run.Tool.Driver.Rules.ShouldBeNull();
            var result = run.Results.ShouldHaveSingleItem();
            result.RuleId.ShouldBeNull();
            result.RuleIndex.ShouldBe(-1);
            result.GetProperty(SarifIssueReportGenerator.RuleUrlPropertyName).ShouldBe(ruleUrl);
        }

        [Fact]
        public void Should_Set_Text_Message()
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var message = "Message Foo.";
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue(message, "ProviderType Foo", "ProviderName Foo")
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var result = sarifLog.Results().ShouldHaveSingleItem();
            result.Message.Text.ShouldBe(message);
        }

        [Fact]
        public void Should_Set_Markdown_Message()
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var message = "Message Foo -- now in **Markdown**!";
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .WithMessageInMarkdownFormat(message)
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var result = sarifLog.Results().ShouldHaveSingleItem();
            result.Message.Markdown.ShouldBe(message);
        }

        [Theory]
        [InlineData(IssuePriority.Undefined, FailureLevel.None)]
        [InlineData(IssuePriority.Hint, FailureLevel.Note)]
        [InlineData(IssuePriority.Suggestion, FailureLevel.Note)]
        [InlineData(IssuePriority.Warning, FailureLevel.Warning)]
        [InlineData(IssuePriority.Error, FailureLevel.Error)]
        public void Should_Set_Level(IssuePriority priority, FailureLevel level)
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .WithPriority(priority)
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var result = sarifLog.Results().ShouldHaveSingleItem();
            result.Level.ShouldBe(level);
        }

        [Theory]
        [InlineData(IssuePriority.Undefined, ResultKind.NotApplicable)]
        [InlineData(IssuePriority.Hint, ResultKind.Fail)]
        [InlineData(IssuePriority.Suggestion, ResultKind.Fail)]
        [InlineData(IssuePriority.Warning, ResultKind.Fail)]
        [InlineData(IssuePriority.Error, ResultKind.Fail)]
        public void Should_Set_Kind(IssuePriority priority, ResultKind kind)
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .WithPriority(priority)
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var result = sarifLog.Results().ShouldHaveSingleItem();
            result.Kind.ShouldBe(kind);
        }

        [Fact]
        public void Should_Set_ArtifactLocation()
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .InFile(@"src\Cake.Issues.Reporting.Sarif.Tests\SarifIssueReportGeneratorTests.cs")
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var result = sarifLog.Results().ShouldHaveSingleItem();
            var location = result.Locations.ShouldHaveSingleItem();
            location.PhysicalLocation.ArtifactLocation.Uri.OriginalString.ShouldBe("src/Cake.Issues.Reporting.Sarif.Tests/SarifIssueReportGeneratorTests.cs");
        }

        [Fact]
        public void Should_Set_StartLine()
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var startLine = 42;
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .InFile(@"src\Cake.Issues.Reporting.Sarif.Tests\SarifIssueReportGeneratorTests.cs", startLine)
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var result = sarifLog.Results().ShouldHaveSingleItem();
            var location = result.Locations.ShouldHaveSingleItem();
            location.PhysicalLocation.Region.StartLine.ShouldBe(startLine);
        }

        [Fact]
        public void Should_Set_EndLine()
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var endLine = 42;
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .InFile(@"src\Cake.Issues.Reporting.Sarif.Tests\SarifIssueReportGeneratorTests.cs", 10, endLine, 1, 2)
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var result = sarifLog.Results().ShouldHaveSingleItem();
            var location = result.Locations.ShouldHaveSingleItem();
            location.PhysicalLocation.Region.EndLine.ShouldBe(endLine);
        }

        [Fact]
        public void Should_Set_StartColumn()
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var startColumn = 42;
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .InFile(@"src\Cake.Issues.Reporting.Sarif.Tests\SarifIssueReportGeneratorTests.cs", 10, startColumn)
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var result = sarifLog.Results().ShouldHaveSingleItem();
            var location = result.Locations.ShouldHaveSingleItem();
            location.PhysicalLocation.Region.StartColumn.ShouldBe(startColumn);
        }

        [Fact]
        public void Should_Set_EndColumn()
        {
            // Given
            var fixture = new SarifIssueReportFixture();
            var endColumn = 42;
            var issues =
                 new List<IIssue>
                 {
                        IssueBuilder
                            .NewIssue("Message Foo.", "ProviderType Foo", "ProviderName Foo")
                            .InFile(@"src\Cake.Issues.Reporting.Sarif.Tests\SarifIssueReportGeneratorTests.cs", 10, 20, 1, endColumn)
                            .Create(),
                 };

            // When
            var logContents = fixture.CreateReport(issues);

            // Then
            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(logContents);

            var result = sarifLog.Results().ShouldHaveSingleItem();
            var location = result.Locations.ShouldHaveSingleItem();
            location.PhysicalLocation.Region.EndColumn.ShouldBe(endColumn);
        }
    }
}
