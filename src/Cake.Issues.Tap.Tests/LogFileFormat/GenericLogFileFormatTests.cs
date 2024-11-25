namespace Cake.Issues.Tap.Tests.LogFileFormat;

using Cake.Issues.Tap.LogFileFormat;

public sealed class GenericLogFileFormatTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given / When
            var result = Record.Exception(() => new GenericLogFileFormat(null));

            // Then
            result.IsArgumentNullException("log");
        }
    }

    public sealed class TheReadIssuesMethod
    {
        [Fact]
        public void Should_Read_Issue_Correct_For_Specification_File()
        {
            // Given
            var fixture = new TapIssuesProviderFixture<GenericLogFileFormat>("specification.tap");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);

            var issue = issues[0];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "First line of the input valid",
                    "Cake.Issues.Tap.TapIssuesProvider",
                    "TAP")
                    .Create());

            issue = issues[1];
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    "Summarized correctly",
                    "Cake.Issues.Tap.TapIssuesProvider",
                    "TAP")
                    .Create());
        }

        [Fact]
        public void Should_Read_Issue_Correct_For_Kubeconform_File_From_Invalid_Yaml()
        {
            // Given
            var fixture = new TapIssuesProviderFixture<GenericLogFileFormat>("kubeconform-invalid.tap");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);

            var issue = issues.Single();
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    ".\\invalid.yaml (v1/ReplicationController//bob): problem validating schema. Check JSON formatting: jsonschema: '/spec/replicas' does not validate with https://raw.githubusercontent.com/yannh/kubernetes-json-schema/master/master-standalone/replicationcontroller-v1.json#/properties/spec/properties/replicas/type: expected integer or null, but got string",
                    "Cake.Issues.Tap.TapIssuesProvider",
                    "TAP")
                    .Create());
        }

        [Fact]
        public void Should_Read_Issue_Correct_For_Kubeconform_File_From_Valid_Yaml()
        {
            // Given
            var fixture = new TapIssuesProviderFixture<GenericLogFileFormat>("kubeconform-valid.tap");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.ShouldBeEmpty();
        }

        [Fact]
        public void Should_Read_Issue_Correct_For_Kubeconform_File_From_Yaml_With_Invalid_List()
        {
            // Given
            var fixture = new TapIssuesProviderFixture<GenericLogFileFormat>("kubeconform-list-invalid.tap");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);

            var issue = issues.Single();
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    ".\\list_invalid.yaml (v1/ReplicationController//bob): problem validating schema. Check JSON formatting: jsonschema: '/spec/replicas' does not validate with https://raw.githubusercontent.com/yannh/kubernetes-json-schema/master/master-standalone/replicationcontroller-v1.json#/properties/spec/properties/replicas/type: expected integer or null, but got string",
                    "Cake.Issues.Tap.TapIssuesProvider",
                    "TAP")
                    .Create());
        }

        [Fact]
        public void Should_Read_Issue_Correct_For_Kubeconform_File_From_Yaml_With_Missing_Api_Version()
        {
            // Given
            var fixture = new TapIssuesProviderFixture<GenericLogFileFormat>("kubeconform-missing-apiversion.tap");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);

            var issue = issues.Single();
            IssueChecker.Check(
                issue,
                IssueBuilder.NewIssue(
                    ".\\missing_apiversion.yaml: error while parsing: missing 'apiVersion' key",
                    "Cake.Issues.Tap.TapIssuesProvider",
                    "TAP")
                    .Create());
        }
    }
}
