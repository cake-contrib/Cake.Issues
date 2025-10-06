namespace Cake.Issues;

using System;
using System.IO;
using System.Text;

/// <summary>
/// Utility methods for converting between different location coordinate formats.
/// </summary>
public static class LocationCoordinateConverter
{
    /// <summary>
    /// Converts line and column to character offset.
    /// </summary>
    /// <param name="fileContent">The content of the file.</param>
    /// <param name="line">The line number (1-based).</param>
    /// <param name="column">The column number (1-based). If null, returns offset to start of line.</param>
    /// <returns>The character offset (0-based) or null if conversion fails.</returns>
    public static int? LineColumnToOffset(string fileContent, int? line, int? column = null)
    {
        if (string.IsNullOrEmpty(fileContent) || !line.HasValue || line.Value < 1)
        {
            return null;
        }

        try
        {
            var currentLine = 1;
            
            for (var i = 0; i < fileContent.Length; i++)
            {
                if (currentLine == line.Value)
                {
                    if (!column.HasValue || column.Value < 1)
                    {
                        return i; // Return current position as offset
                    }

                    // Count characters until we reach the desired column or end of line
                    var currentColumn = 1;
                    var startOffset = i;
                    while (i < fileContent.Length && currentColumn < column.Value && 
                           fileContent[i] != '\r' && fileContent[i] != '\n')
                    {
                        i++;
                        currentColumn++;
                    }
                    
                    return startOffset + (currentColumn - 1);
                }

                if (fileContent[i] == '\r')
                {
                    // Handle Windows line ending (\r\n)
                    if (i + 1 < fileContent.Length && fileContent[i + 1] == '\n')
                    {
                        i++; // Skip the \n
                    }
                    currentLine++;
                }
                else if (fileContent[i] == '\n')
                {
                    // Handle Unix line ending (\n)
                    currentLine++;
                }
            }
        }
        catch
        {
            // Return null on any error
        }

        return null;
    }

    /// <summary>
    /// Converts line and column to character offset using byte array content.
    /// </summary>
    /// <param name="fileContent">The content of the file as byte array.</param>
    /// <param name="line">The line number (1-based).</param>
    /// <param name="column">The column number (1-based). If null, returns offset to start of line.</param>
    /// <param name="encoding">The encoding to use. If null, UTF-8 is used.</param>
    /// <returns>The character offset (0-based) or null if conversion fails.</returns>
    public static int? LineColumnToOffset(byte[] fileContent, int? line, int? column = null, Encoding encoding = null)
    {
        if (fileContent == null || fileContent.Length == 0)
        {
            return null;
        }

        try
        {
            var content = (encoding ?? Encoding.UTF8).GetString(fileContent);
            return LineColumnToOffset(content, line, column);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Converts character offset to line and column.
    /// </summary>
    /// <param name="fileContent">The content of the file.</param>
    /// <param name="offset">The character offset (0-based).</param>
    /// <returns>A tuple containing line (1-based) and column (1-based), or null if conversion fails.</returns>
    public static (int Line, int Column)? OffsetToLineColumn(string fileContent, int? offset)
    {
        if (string.IsNullOrEmpty(fileContent) || !offset.HasValue || offset.Value < 0)
        {
            return null;
        }

        try
        {
            var line = 1;
            var column = 1;
            var currentOffset = 0;

            for (var i = 0; i < Math.Min(offset.Value, fileContent.Length); i++)
            {
                if (fileContent[i] == '\r')
                {
                    // Handle Windows line ending (\r\n)
                    if (i + 1 < fileContent.Length && fileContent[i + 1] == '\n')
                    {
                        if (currentOffset == offset.Value)
                        {
                            return (line, column);
                        }
                        i++; // Skip the \n
                    }
                    line++;
                    column = 1;
                }
                else if (fileContent[i] == '\n')
                {
                    // Handle Unix line ending (\n)
                    line++;
                    column = 1;
                }
                else
                {
                    column++;
                }

                currentOffset++;
            }

            return (line, column);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Converts character offset to line and column using byte array content.
    /// </summary>
    /// <param name="fileContent">The content of the file as byte array.</param>
    /// <param name="offset">The character offset (0-based).</param>
    /// <param name="encoding">The encoding to use. If null, UTF-8 is used.</param>
    /// <returns>A tuple containing line (1-based) and column (1-based), or null if conversion fails.</returns>
    public static (int Line, int Column)? OffsetToLineColumn(byte[] fileContent, int? offset, Encoding encoding = null)
    {
        if (fileContent == null || fileContent.Length == 0)
        {
            return null;
        }

        try
        {
            var content = (encoding ?? Encoding.UTF8).GetString(fileContent);
            return OffsetToLineColumn(content, offset);
        }
        catch
        {
            return null;
        }
    }
}