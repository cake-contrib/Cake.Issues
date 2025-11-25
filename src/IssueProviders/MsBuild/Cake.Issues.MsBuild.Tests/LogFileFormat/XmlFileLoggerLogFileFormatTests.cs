namespace Cake.Issues.MsBuild.Tests.LogFileFormat;

using System.Runtime.InteropServices;
using Cake.Core.Diagnostics;
using Cake.Issues.MsBuild.LogFileFormat;

public sealed class XmlFileLoggerLogFileFormatTests
{
    public sealed class TheCtor
    {
        [Fact]
        public void Should_Throw_If_Log_Is_Null()
        {
            // Given
            const ICakeLog log = null;

            // When
            var result = Record.Exception(() => new XmlFileLoggerLogFileFormat(log));

            // Then
            result.IsArgumentNullException("log");
        }
    }

    public sealed class TheReadIssuesMethod
    {
        [SkippableFact]
        public void Should_Read_Full_Log_Correct()
        {
            // Uses Windows specific paths.
            Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

            // Given
            var fixture = new MsBuildIssuesProviderFixture<XmlFileLoggerLogFileFormat>("FullLog.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(19);
            IssueChecker.Check(
                issues[0],
                IssueBuilder.NewIssue(
                    "The variable 'foo' is assigned but its value is never used",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Class1.cs", 13, 17)
                    .OfRule("CS0219")
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[1],
                IssueBuilder.NewIssue(
                    "Enable XML documentation output",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Class1.cs", 1, 1)
                    .OfRule("SA1652", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1652.md"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[2],
                IssueBuilder.NewIssue(
                    "The file header is missing or not located at the top of the file.",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Class1.cs", 1, 1)
                    .OfRule("SA1633", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1633.md"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[3],
                IssueBuilder.NewIssue(
                    "Using directive must appear within a namespace declaration",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Class1.cs", 1, 1)
                    .OfRule("SA1200", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1200.md"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[4],
                IssueBuilder.NewIssue(
                    "Using directive must appear within a namespace declaration",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Class1.cs", 2, 1)
                    .OfRule("SA1200", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1200.md"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[5],
                IssueBuilder.NewIssue(
                    "Using directive must appear within a namespace declaration",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Class1.cs", 3, 1)
                    .OfRule("SA1200", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1200.md"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[6],
                IssueBuilder.NewIssue(
                    "Using directive must appear within a namespace declaration",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Class1.cs", 4, 1)
                    .OfRule("SA1200", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1200.md"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[7],
                IssueBuilder.NewIssue(
                    "Using directive must appear within a namespace declaration",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Class1.cs", 5, 1)
                    .OfRule("SA1200", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1200.md"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[8],
                IssueBuilder.NewIssue(
                    "Enable XML documentation output",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Properties\AssemblyInfo.cs", 1, 1)
                    .OfRule("SA1652", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1652.md"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[9],
                IssueBuilder.NewIssue(
                    "The file header is missing or not located at the top of the file.",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Properties\AssemblyInfo.cs", 1, 1)
                    .OfRule("SA1633", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1633.md"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[10],
                IssueBuilder.NewIssue(
                    "Code must not contain trailing whitespace",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Properties\AssemblyInfo.cs", 5, 77)
                    .OfRule("SA1028", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1028.md"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[11],
                IssueBuilder.NewIssue(
                    "Code must not contain trailing whitespace",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Properties\AssemblyInfo.cs", 17, 76)
                    .OfRule("SA1028", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1028.md"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[12],
                IssueBuilder.NewIssue(
                    "Code must not contain trailing whitespace",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Properties\AssemblyInfo.cs", 18, 74)
                    .OfRule("SA1028", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1028.md"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[13],
                IssueBuilder.NewIssue(
                    "Code must not contain trailing whitespace",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Properties\AssemblyInfo.cs", 28, 22)
                    .OfRule("SA1028", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1028.md"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[14],
                IssueBuilder.NewIssue(
                    "Code must not contain trailing whitespace",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Properties\AssemblyInfo.cs", 32, 84)
                    .OfRule("SA1028", new Uri("https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1028.md"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[15],
                IssueBuilder.NewIssue(
                    "Microsoft.Design : Sign 'ClassLibrary1.dll' with a strong name key.",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .OfRule("CA2210", new Uri("https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/CA2210"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[16],
                IssueBuilder.NewIssue(
                    "Microsoft.Design : Mark 'ClassLibrary1.dll' with CLSCompliant(true) because it exposes externally visible types.",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .OfRule("CA1014", new Uri("https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/CA1014"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[17],
                IssueBuilder.NewIssue(
                    "Microsoft.Performance : The 'this' parameter (or 'Me' in Visual Basic) of 'Class1.Foo()' is never used. Mark the member as static (or Shared in Visual Basic) or use 'this'/'Me' in the method body or at least one property accessor, if appropriate.",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Class1.cs", 12)
                    .OfRule("CA1822", new Uri("https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/CA1822"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[18],
                IssueBuilder.NewIssue(
                    "Microsoft.Performance : 'Class1.Foo()' declares a variable, 'foo', of type 'string', which is never used or is only assigned to. Use this variable or remove it.",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject(@"src\ClassLibrary1\ClassLibrary1.csproj", "ClassLibrary1")
                    .InFile(@"src\ClassLibrary1\Class1.cs", 13)
                    .OfRule("CA1804", new Uri("https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/CA1804"))
                    .WithPriority(IssuePriority.Warning));
        }

        [SkippableFact]
        public void Should_Read_Issue_With_File_Correct()
        {
            // Uses Windows specific paths.
            Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

            // Given
            var fixture = new MsBuildIssuesProviderFixture<XmlFileLoggerLogFileFormat>("IssueWithFile.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            IssueChecker.Check(
                issues.Single(),
                IssueBuilder.NewIssue(
                    "Microsoft.Usage : 'ConfigurationManager.GetSortedConfigFiles(String)' creates an exception of type 'ApplicationException', an exception type that is not sufficiently specific and should never be raised by user code. If this exception instance might be thrown, use a different exception type.",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProjectOfName(string.Empty)
                    .InFile(@"src\Cake.Issues.MsBuild.Tests\MsBuildIssuesProviderTests.cs", 1311)
                    .OfRule("CA2201", new Uri("https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/CA2201"))
                    .WithPriority(IssuePriority.Warning));
        }

        [SkippableFact]
        public void Should_Read_Issue_With_File_Without_Path_Correct()
        {
            // Uses Windows specific paths.
            Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

            // Given
            var fixture = new MsBuildIssuesProviderFixture<XmlFileLoggerLogFileFormat>("IssueWithOnlyFileName.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            IssueChecker.Check(
                issues.Single(),
                IssueBuilder.NewIssue(
                    "The variable 'foo' is assigned but its value is never used",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProjectOfName(string.Empty)
                    .InFile(@"src\Cake.Issues.MsBuild.Tests\MsBuildIssuesProviderTests.cs", 13, 17)
                    .OfRule("CS0219")
                    .WithPriority(IssuePriority.Warning));
        }

        [SkippableFact]
        public void Should_Read_Issue_With_Line_Zero_Correct()
        {
            // Uses Windows specific paths.
            Skip.IfNot(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));

            // Given
            var fixture = new MsBuildIssuesProviderFixture<XmlFileLoggerLogFileFormat>("IssueWithLineZero.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            IssueChecker.Check(
                issues.Single(),
                IssueBuilder.NewIssue(
                    @"Unable to locate any documentation sources for 'c:\Source\Cake.Prca\Cake.Prca..csproj' (Configuration: Debug Platform: AnyCPU)",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProject("Cake.Prca.shfbproj", "Cake.Prca")
                    .InFile("SHFB")
                    .OfRule("BE0006")
                    .WithPriority(IssuePriority.Warning));
        }

        [Fact]
        public void Should_Read_Issue_Without_File_Correct()
        {
            // Given
            var fixture = new MsBuildIssuesProviderFixture<XmlFileLoggerLogFileFormat>("IssueWithoutFile.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            IssueChecker.Check(
                issues.Single(),
                IssueBuilder.NewIssue(
                    "Microsoft.Naming : Rename type name 'UniqueQueue(Of T)' so that it does not end in 'Queue'.",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProjectOfName(string.Empty)
                    .OfRule("CA1711", new Uri("https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/CA1711"))
                    .WithPriority(IssuePriority.Warning));
        }

        [Fact]
        public void Should_Read_Issue_Without_Code_Correct()
        {
            // Given
            var fixture = new MsBuildIssuesProviderFixture<XmlFileLoggerLogFileFormat>("IssueWithoutCode.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            IssueChecker.Check(
                issues.Single(),
                IssueBuilder.NewIssue(
                    "SA1300 : CSharp.Naming : namespace names begin with an upper-case letter: foo.",
                    "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                    "MSBuild")
                    .InProjectOfName(string.Empty)
                    .InFile(@"src\Cake.Issues.MsBuild.Tests\MsBuildIssuesProviderTests.cs", 21, 1)
                    .WithPriority(IssuePriority.Warning));
        }

        [Fact]
        public void Should_Ignore_Issue_Without_Message()
        {
            // Given
            var fixture = new MsBuildIssuesProviderFixture<XmlFileLoggerLogFileFormat>("IssueWithoutMessage.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(0);
        }

        [Fact]
        public void Should_Filter_Control_Chars_From_Log_Content()
        {
            // Given
            var fixture = new MsBuildIssuesProviderFixture<XmlFileLoggerLogFileFormat>("LogWithControlChars.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(0);
        }

        [Fact]
        public void Should_Read_Issue_With_Absolute_FileName_And_Without_Task()
        {
            var fixture = new MsBuildIssuesProviderFixture<XmlFileLoggerLogFileFormat>("IssueWithAbsoluteFileNameAndWithoutTask.xml");

            var repoRootCreated = !Directory.Exists(fixture.ReadIssuesSettings.RepositoryRoot.FullPath);
            _ = Directory.CreateDirectory(fixture.ReadIssuesSettings.RepositoryRoot.FullPath);
            try
            {
                var oldWorkingDirectory = Directory.GetCurrentDirectory();
                try
                {
                    Directory.SetCurrentDirectory(fixture.ReadIssuesSettings.RepositoryRoot.FullPath);

                    // When
                    var issues = fixture.ReadIssues().ToList();

                    // Then
                    issues.Count.ShouldBe(1);
                }
                finally
                {
                    Directory.SetCurrentDirectory(oldWorkingDirectory);
                }
            }
            finally
            {
                if (repoRootCreated)
                {
                    Directory.Delete(fixture.ReadIssuesSettings.RepositoryRoot.FullPath);
                }
            }
        }

        [Fact]
        public void Should_Read_Errors()
        {
            // Given
            var fixture = new MsBuildIssuesProviderFixture<XmlFileLoggerLogFileFormat>("IssueWithError.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(1);
            IssueChecker.Check(
                issues[0],
                IssueBuilder.NewIssue(
                        "'ConfigurationManager.GetSortedConfigFiles(String)': not all code paths return a value",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                    .InProjectOfName(string.Empty)
                    .InFile(@"src\Cake.Issues.MsBuild.Tests\MsBuildIssuesProviderTests.cs", 1311)
                    .OfRule("CS0161")
                    .WithPriority(IssuePriority.Error));
        }

        [Fact]
        public void Should_Read_Both_Warnings_And_Errors()
        {
            // Given
            var fixture = new MsBuildIssuesProviderFixture<XmlFileLoggerLogFileFormat>("IssueWithBothWarningAndErrors.xml");

            // When
            var issues = fixture.ReadIssues().ToList();

            // Then
            issues.Count.ShouldBe(2);
            IssueChecker.Check(
                issues[0],
                IssueBuilder.NewIssue(
                        "Microsoft.Usage : 'ConfigurationManager.GetSortedConfigFiles(String)' creates an exception of type 'ApplicationException', an exception type that is not sufficiently specific and should never be raised by user code. If this exception instance might be thrown, use a different exception type.",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                    .InProjectOfName(string.Empty)
                    .InFile(@"src\Cake.Issues.MsBuild.Tests\MsBuildIssuesProviderTests.cs", 1311)
                    .OfRule("CA2201", new Uri("https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/CA2201"))
                    .WithPriority(IssuePriority.Warning));
            IssueChecker.Check(
                issues[1],
                IssueBuilder.NewIssue(
                        "'ConfigurationManager.GetSortedConfigFiles(String)': not all code paths return a value",
                        "Cake.Issues.MsBuild.MsBuildIssuesProvider",
                        "MSBuild")
                    .InProjectOfName(string.Empty)
                    .InFile(@"src\Cake.Issues.MsBuild.Tests\MsBuildIssuesProviderTests.cs", 1311)
                    .OfRule("CS0161")
                    .WithPriority(IssuePriority.Error));
        }
    }
}
