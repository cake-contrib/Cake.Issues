namespace Cake.Issues.GitRepository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary>
/// Utility class for matching file paths against glob-style patterns.
/// </summary>
internal static class FilePatternMatcher
{
    /// <summary>
    /// Checks if a file path matches any of the specified patterns.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="patterns">The list of patterns to match against.</param>
    /// <returns>True if the file path matches any pattern; otherwise, false.</returns>
    public static bool IsMatch(string filePath, IEnumerable<string> patterns) =>
        !string.IsNullOrEmpty(filePath) &&
        patterns != null &&
        patterns.Any(pattern => IsMatch(filePath, pattern));

    /// <summary>
    /// Checks if a file path matches a specific pattern.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <param name="pattern">The pattern to match against.</param>
    /// <returns>True if the file path matches the pattern; otherwise, false.</returns>
    public static bool IsMatch(string filePath, string pattern)
    {
        if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(pattern))
        {
            return false;
        }

        // Normalize path separators to forward slashes for consistent matching
        var normalizedPath = filePath.Replace('\\', '/');
        
        // Convert glob pattern to regex (don't normalize backslashes in pattern as they may be escape characters)
        var regexPattern = ConvertGlobToRegex(pattern);

        try
        {
            return Regex.IsMatch(normalizedPath, regexPattern, RegexOptions.IgnoreCase);
        }
        catch (ArgumentException)
        {
            // If regex is invalid, fall back to exact string comparison
            return string.Equals(normalizedPath, pattern, StringComparison.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    /// Converts a glob pattern to a regular expression pattern.
    /// </summary>
    /// <param name="globPattern">The glob pattern to convert.</param>
    /// <returns>A regular expression pattern.</returns>
    private static string ConvertGlobToRegex(string globPattern)
    {
        var regexPattern = "^";

        for (var i = 0; i < globPattern.Length; i++)
        {
            var c = globPattern[i];

            switch (c)
            {
                case '*':
                    if (i + 1 < globPattern.Length && globPattern[i + 1] == '*')
                    {
                        // ** matches zero or more directories
                        if (i + 2 < globPattern.Length && (globPattern[i + 2] == '/' || globPattern[i + 2] == '\\'))
                        {
                            regexPattern += "(?:[^/\\\\]+[/\\\\])*";
                            i += 2; // Skip the next * and separator
                        }
                        else
                        {
                            regexPattern += ".*";
                            i++; // Skip the next *
                        }
                    }
                    else
                    {
                        // * matches any characters except path separators
                        regexPattern += "[^/\\\\]*";
                    }

                    break;

                case '?':
                    // ? matches any single character except path separators
                    regexPattern += "[^/\\\\]";
                    break;

                case '\\':
                    // Handle escaped characters in glob patterns
                    if (i + 1 < globPattern.Length)
                    {
                        var nextChar = globPattern[i + 1];
                        // If the next character is not a path separator, treat this as an escape
                        if (nextChar != '/' && nextChar != '\\')
                        {
                            // Escape the next character for regex
                            regexPattern += "\\" + nextChar;
                            i++; // Skip the next character
                        }
                        else
                        {
                            // This is a path separator, normalize to forward slash in regex
                            regexPattern += "/";
                        }
                    }
                    else
                    {
                        // Backslash at end of pattern, treat as literal path separator
                        regexPattern += "/";
                    }
                    break;

                case '/':
                    // Always normalize path separators to forward slash in regex
                    regexPattern += "/";
                    break;

                case '.':
                case '^':
                case '$':
                case '+':
                case '{':
                case '}':
                case '[':
                case ']':
                case '(':
                case ')':
                case '|':
                    // Escape regex special characters
                    regexPattern += "\\" + c;
                    break;

                default:
                    regexPattern += c;
                    break;
            }
        }

        regexPattern += "$";
        return regexPattern;
    }
}