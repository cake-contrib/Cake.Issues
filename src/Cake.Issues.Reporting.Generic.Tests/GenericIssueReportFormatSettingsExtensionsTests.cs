namespace Cake.Issues.Reporting.Generic.Tests
{
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class GenericIssueReportFormatSettingsExtensionsTests
    {
        public sealed class TheWithOptionWithStringKeyMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                GenericIssueReportFormatSettings settings = null;

                // When
                var result = Record.Exception(() =>
                    settings.WithOption("Foo", "Bar"));

                // Then
                result.IsArgumentNullException("settings");
            }

            [Fact]
            public void Should_Add_Option()
            {
                // Given
                var key = "Foo";
                var value = "Bar";
                var settings = GenericIssueReportFormatSettings.FromContent("Foo");

                // When
                var result = settings.WithOption(key, value);

                // Then
                result.Options.Count.ShouldBe(1);
                result.Options.ShouldContainKeyAndValue(key, value);
            }
        }

        public sealed class TheWithOptionWithEnumgKeyMethod
        {
            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                GenericIssueReportFormatSettings settings = null;

                // When
                var result = Record.Exception(() =>
                    settings.WithOption(HtmlDxDataGridOption.Theme, "Bar"));

                // Then
                result.IsArgumentNullException("settings");
            }

            [Fact]
            public void Should_Add_Option()
            {
                // Given
                var key = HtmlDxDataGridOption.Title;
                var value = "Bar";
                var settings = GenericIssueReportFormatSettings.FromContent("Foo");

                // When
                var result = settings.WithOption(key, value);

                // Then
                result.Options.Count.ShouldBe(1);
                result.Options.ShouldContainKeyAndValue(key.ToString(), value);
            }
        }
    }
}
