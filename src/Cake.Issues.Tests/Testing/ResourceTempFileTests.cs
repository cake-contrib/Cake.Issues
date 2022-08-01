namespace Cake.Issues.Tests.Testing
{
    using System.IO;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class ResourceTempFileTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_ResourceName_Is_Null()
            {
                // Given
                string resourceName = null;

                // When
                var result = Record.Exception(() => new ResourceTempFile(resourceName));

                // Then
                result.IsArgumentNullException("resourceName");
            }

            [Fact]
            public void Should_Throw_If_ResourceName_Is_Empty()
            {
                // Given
                var resourceName = string.Empty;

                // When
                var result = Record.Exception(() => new ResourceTempFile(resourceName));

                // Then
                result.IsArgumentOutOfRangeException("resourceName");
            }

            [Fact]
            public void Should_Throw_If_ResourceName_Is_WhiteSpace()
            {
                // Given
                var resourceName = " ";

                // When
                var result = Record.Exception(() => new ResourceTempFile(resourceName));

                // Then
                result.IsArgumentOutOfRangeException("resourceName");
            }

            [Fact]
            public void Should_Throw_If_ResourceName_Does_Not_Exist()
            {
                // Given
                var resourceName = "foo";

                // When
                var result = Record.Exception(() => new ResourceTempFile(resourceName));

                // Then
                result.IsArgumentException("resourceName");
            }

            [Fact]
            public void Should_Create_Temp_File()
            {
                // Given
                var resourceName = "Cake.Issues.Tests.Testfiles.Build.log";

                // When
                using (var tempFile = new ResourceTempFile(resourceName))
                {
                    // Then
                    File.Exists(tempFile.FileName).ShouldBeTrue();
                }
            }

            [Fact]
            public void Should_Write_Content()
            {
                // Given
                var resourceName = "Cake.Issues.Tests.Testfiles.Build.log";

                // When
                using (var tempFile = new ResourceTempFile(resourceName))
                {
                    // Then
                    tempFile.Content.ShouldNotBeEmpty();
                }
            }
        }

        public sealed class TheDisposeMethod
        {
            [Fact]
            public void Should_Remove_Temp_File()
            {
                // Given
                var resourceName = "Cake.Issues.Tests.Testfiles.Build.log";
                string tempFileName;

                // When
                using (var tempFile = new ResourceTempFile(resourceName))
                {
                    tempFileName = tempFile.FileName;
                }

                // Then
                File.Exists(tempFileName).ShouldBeFalse();
            }
        }
    }
}
