﻿namespace Cake.Issues.Reporting.Generic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Cake.Core.Diagnostics;
    using Cake.Testing;
    using Shouldly;

    internal class GenericIssueReportFixture
    {
        public GenericIssueReportFixture(GenericIssueReportTemplate template)
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };
            this.GenericIssueReportFormatSettings =
                GenericIssueReportFormatSettings.FromEmbeddedTemplate(template);
        }

        public GenericIssueReportFixture(string templateContent)
        {
            this.Log = new FakeLog { Verbosity = Verbosity.Normal };
            this.GenericIssueReportFormatSettings =
                GenericIssueReportFormatSettings.FromContent(templateContent);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public FakeLog Log { get; set; }

        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public GenericIssueReportFormatSettings GenericIssueReportFormatSettings { get; set; }

        public string CreateReport(IEnumerable<IIssue> issues)
        {
            var generator =
                new GenericIssueReportGenerator(this.Log, this.GenericIssueReportFormatSettings);

            var reportFile = Path.GetTempFileName();
            try
            {
                var createIssueReportSettings =
                    new CreateIssueReportSettings(@"c:\Source\Cake.Issues.Reporting.Generic", reportFile);
                generator.Initialize(createIssueReportSettings);
                generator.CreateReport(issues);

                using (var stream = new FileStream(reportFile, FileMode.Open, FileAccess.Read))
                {
                    using (var sr = new StreamReader(stream))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            finally
            {
                if (File.Exists(reportFile))
                {
                    File.Delete(reportFile);
                }
            }
        }

        public void TestReportCreation(Action<GenericIssueReportFormatSettings> settings)
        {
            // Given
            settings(this.GenericIssueReportFormatSettings);

            // When
            var result =
                this.CreateReport(
                    new List<IIssue>
                    {
                            IssueBuilder
                                .NewIssue("Message Foo", "ProviderType Foo", "ProviderName Foo")
                                .InFile(@"src\Cake.Issues.Reporting.Generic.Tests\Foo.cs", 10)
                                .OfRule("Rule Foo")
                                .WithPriority(IssuePriority.Warning)
                                .Create(),
                    });

            // Then
            // Currently only checks if generation failed or not without checking actual output.
            result.ShouldNotBeNull();
        }
    }
}