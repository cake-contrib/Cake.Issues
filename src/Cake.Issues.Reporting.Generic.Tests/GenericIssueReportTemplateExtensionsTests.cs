namespace Cake.Issues.Reporting.Generic.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Instantiated by test runner")]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull", Justification = "By design for null tests")]
    public sealed class GenericIssueReportTemplateExtensionsTests
    {
        public sealed class TheGetTemplateResourceNameMethod
        {
            [Fact]
            public void Should_Return_ResourceName()
            {
                // Given
                var template = GenericIssueReportTemplate.HtmlDiagnostic;

                // When
                var result = template.GetTemplateResourceName();

                // Then
                result.ShouldBe("Diagnostic.cshtml");
            }
        }
    }
}
