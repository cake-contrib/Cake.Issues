namespace Cake.Issues.Terraform.Tests
{
    using System;
    using Cake.Core.IO;
    using Cake.Issues.Testing;
    using Shouldly;
    using Xunit;

    public sealed class TerraformSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_ValidateOutputFilePath_Is_Null()
            {
                // Given
                FilePath validateOutputFilePath = null;
                var terraformRootPath = @"c:\Source\Cake.Issues\docs";

                // When
                var result = Record.Exception(() =>
                    new TerraformIssuesSettings(validateOutputFilePath, terraformRootPath));

                // Then
                result.IsArgumentNullException("logFilePath");
            }

            [Fact]
            public void Should_Throw_If_TerraformRootPath_For_ValidateOutputFilePath_Is_Null()
            {
                // Given
                DirectoryPath terraformRootPath = null;

                using (var tempFile = new ResourceTempFile("Cake.Issues.Terraform.Tests.Testfiles.basic.json"))
                {
                    // When
                    var result = Record.Exception(() =>
                        new TerraformIssuesSettings(tempFile.FileName, terraformRootPath));

                    // Then
                    result.IsArgumentNullException("terraformRootPath");
                }
            }

            [Fact]
            public void Should_Throw_If_ValidateOutput_Is_Null()
            {
                // Given
                byte[] validateOutput = null;
                var terraformRootPath = @"c:\Source\Cake.Issues\docs";

                // When
                var result = Record.Exception(() => new TerraformIssuesSettings(validateOutput, terraformRootPath));

                // Then
                result.IsArgumentNullException("logFileContent");
            }

            [Fact]
            public void Should_Throw_If_TerraformRootPath_For_ValidateOutput_Is_Null()
            {
                // Given
                var validateOutput = "foo".ToByteArray();
                DirectoryPath terraformRootPath = null;

                // When
                var result = Record.Exception(() =>
                    new TerraformIssuesSettings(validateOutput, terraformRootPath));

                // Then
                result.IsArgumentNullException("terraformRootPath");
            }

            [Fact]
            public void Should_Set_ValidateOutput()
            {
                // Given
                var validateOutput = "Foo".ToByteArray();
                var terraformRootPath = @"c:\Source\Cake.Issues\docs";

                // When
                var settings = new TerraformIssuesSettings(validateOutput, terraformRootPath);

                // Then
                settings.LogFileContent.ShouldBe(validateOutput);
            }

            [Fact]
            public void Should_Set_ValidateOutput_If_Empty()
            {
                // Given
                byte[] validateOutput = Array.Empty<byte>();
                var terraformRootPath = @"c:\Source\Cake.Issues\docs";

                // When
                var settings = new TerraformIssuesSettings(validateOutput, terraformRootPath);

                // Then
                settings.LogFileContent.ShouldBe(validateOutput);
            }

            [Fact]
            public void Should_Set_TerraformRootPath()
            {
                // Given
                var validateOutput = "Foo".ToByteArray();
                var terraformRootPath = @"c:/Source/Cake.Issues/docs";

                // When
                var settings = new TerraformIssuesSettings(validateOutput, terraformRootPath);

                // Then
                settings.TerraformRootPath.ToString().ShouldBe(terraformRootPath);
            }

            [Fact]
            public void Should_Set_ValidateOutput_From_ValidateOutputFilePath()
            {
                // Given
                var terraformRootPath = @"c:\Source\Cake.Issues\docs";
                using (var tempFile = new ResourceTempFile("Cake.Issues.Terraform.Tests.Testfiles.basic.json"))
                {
                    // When
                    var settings = new TerraformIssuesSettings(tempFile.FileName, terraformRootPath);

                    // Then
                    settings.LogFileContent.ShouldBe(tempFile.Content);
                }
            }

            [Fact]
            public void Should_Set_TerraformRootPath_From_ValidateOutputFilePath()
            {
                // Given
                var terraformRootPath = @"c:/Source/Cake.Issues/docs";
                using (var tempFile = new ResourceTempFile("Cake.Issues.Terraform.Tests.Testfiles.basic.json"))
                {
                    // When
                    var settings = new TerraformIssuesSettings(tempFile.FileName, terraformRootPath);

                    // Then
                    settings.TerraformRootPath.ToString().ShouldBe(terraformRootPath);
                }
            }
        }
    }
}
