namespace Cake.Issues.Tap.Parser;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// Parser for issues reported in TAP format.
/// </summary>
internal partial class TapParser
{
    /// <summary>
    /// Gets the start of the test plan.
    /// </summary>
    public int? PlanStart { get; private set; }

    /// <summary>
    /// Gets the end of the test plan.
    /// </summary>
    public int? PlanEnd { get; private set; }

    /// <summary>
    /// Gets the test points of the test plan.
    /// </summary>
    public List<TapTestPoint> Results { get; private set; } = [];

    /// <summary>
    /// Gets the comments in the TAP file.
    /// </summary>
    public List<string> Comments { get; private set; } = [];

    /// <summary>
    /// Parses the content of a TAP file.
    /// </summary>
    /// <param name="tapContent">Content of the TAP file.</param>
    public void Parse(string tapContent)
    {
        tapContent.NotNullOrEmpty();

        var lines = tapContent.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
        var firstLine = lines[0];

        if (!VersionRegEx().IsMatch(firstLine))
        {
            throw new Exception($"Missing or invalid TAP version declaration. First line is: {firstLine}");
        }

        var match = VersionRegEx().Match(firstLine);
        var version = int.Parse(match.Groups[1].Value);

        switch (version)
        {
            case 14:
                this.ParseVersion14(lines);
                break;
            case 13:
                // TAP version 13 should be compatible with version 14.
                this.ParseVersion14(lines);
                break;
            default:
                throw new Exception($"Unsupported TAP version {version}. Expected version 14.");
        }
    }

    [GeneratedRegex(@"^TAP version (\d+)$")]
    private static partial Regex VersionRegEx();

    [GeneratedRegex(@"^\d+\.\.\d+$")]
    private static partial Regex PlanRegEx14();

    [GeneratedRegex(@"^(ok|not ok)\s*(\d+)?\s*-?\s*(.*?)(# (TODO|SKIP)(\S*)\s+(.*))?$")]
    private static partial Regex ResultRegEx14();

    [GeneratedRegex(@"^#\s*(.*)$")]
    private static partial Regex CommentRegEx14();

    /// <summary>
    /// Parses the content of a TAP file.
    /// </summary>
    /// <param name="lines">Content of the TAP file.</param>
    private void ParseVersion14(string[] lines)
    {
        var planParsed = false;
        var insideYaml = false;
        List<string> yamlLines = null;

        foreach (var line in lines)
        {
            if (insideYaml)
            {
                if (line.Trim() == "...")
                {
                    insideYaml = false;
                    this.ParseYamlVersion14(yamlLines);
                    yamlLines = null;
                }
                else
                {
                    yamlLines.Add(line);
                }

                continue;
            }

            if (PlanRegEx14().IsMatch(line))
            {
                if (planParsed)
                {
                    throw new Exception("Multiple plans found");
                }

                var parts = line.Split("..");
                this.PlanStart = int.Parse(parts[0]);
                this.PlanEnd = int.Parse(parts[1]);
                planParsed = true;
            }
            else if (ResultRegEx14().IsMatch(line))
            {
                var match = ResultRegEx14().Match(line);
                var directive = match.Groups[5].Value;
                var explanation = match.Groups[7].Value.Trim();
                var result = new TapTestPoint
                {
                    TestStatus = match.Groups[1].Value == "ok",
                    TestPointID = match.Groups[2].Success ? int.Parse(match.Groups[2].Value) : null,
                    Description = match.Groups[3].Value.Trim(),
                    IsTodo = directive == "TODO",
                    TodoExplanation = directive == "TODO" ? explanation : null,
                    IsSkip = directive == "SKIP",
                    SkipExplanation = directive == "SKIP" ? explanation : null,
                };
                this.Results.Add(result);
            }
            else if (CommentRegEx14().IsMatch(line))
            {
                var commentMatch = CommentRegEx14().Match(line);
                this.Comments.Add(commentMatch.Groups[1].Value.Trim());
            }
            else if (line.Trim() == "---")
            {
                insideYaml = true;
                yamlLines = [];
            }
            else
            {
                Console.WriteLine($"Warning: Unrecognized line: {line}");
            }
        }

        if (!planParsed)
        {
            throw new Exception("No test plan found");
        }
    }

    private void ParseYamlVersion14(List<string> yamlLines)
    {
        if (this.Results.Count == 0)
        {
            return;
        }

        var lastResult = this.Results[^1];
        var yamlContent = string.Join("\n", yamlLines);

        try
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            var yamlData = deserializer.Deserialize<Dictionary<string, object>>(yamlContent);
            foreach (var entry in yamlData)
            {
                lastResult.Diagnostics[entry.Key] = entry.Value;
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error parsing YAML diagnostics: {ex.Message}", ex);
        }
    }
}