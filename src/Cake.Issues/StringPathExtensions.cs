// Based on http://stackoverflow.com/a/31941159/566901

namespace Cake.Issues
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Extensions for <see cref="string"/> for handling paths.
    /// </summary>
    public static class StringPathExtensions
    {
        /// <summary>
        /// Checks if a string containing a path is a valid path string.
        /// </summary>
        /// <param name="path">Path to check.</param>
        /// <returns><c>True</c> if valid path.</returns>
        public static bool IsValidPath(this string path)
        {
            path.NotNullOrWhiteSpace(nameof(path));

            return path.IndexOfAny(Path.GetInvalidPathChars().ToArray()) == -1;
        }

        /// <summary>
        /// Checks if a string containing a path is a full path.
        /// </summary>
        /// <param name="path">Path which should be checked.</param>
        /// <returns><c>True</c> if full path.</returns>
        public static bool IsFullPath(this string path)
        {
            path.NotNullOrWhiteSpace(nameof(path));

            if (!path.IsValidPath())
            {
                throw new ArgumentException($"Invalid path '{path}'", nameof(path));
            }

            // ReSharper disable once PossibleNullReferenceException
            return
                Path.IsPathRooted(path) &&
                !Path.GetPathRoot(path).Equals(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal);
        }

        /// <summary>
        /// Checks if a path is a sub path of another path.
        /// The comparison is case-insensitive, handles <c>/</c> and <c>\</c> slashes as folder separators and
        /// only matches if the base dir folder name is matched exactly
        /// (<c>c:\foobar\file.txt</c> is not a sub path of <c>c:\foo</c>).
        /// </summary>
        /// <param name="path">Full path which should be checked if it is a sub path.</param>
        /// <param name="baseDirPath">Path for which should be checked if <paramref name="path"/>is a sub path of.</param>
        /// <returns>Returns true if <paramref name="path"/> starts with the path <paramref name="baseDirPath"/>.</returns>
        public static bool IsSubpathOf(this string path, string baseDirPath)
        {
            path.NotNullOrWhiteSpace(nameof(path));
            baseDirPath.NotNullOrWhiteSpace(nameof(baseDirPath));

            if (!path.IsValidPath())
            {
                throw new ArgumentException($"Invalid path '{path}'", nameof(path));
            }

            if (!baseDirPath.IsValidPath())
            {
                throw new ArgumentException($"Invalid path '{baseDirPath}'", nameof(baseDirPath));
            }

            // TODO There are edge cases where GetFullPath can lead to wrong results. See https://github.com/cake-contrib/Cake.Prca/pull/2#discussion_r106646030
            var normalizedPath =
                Path.GetFullPath(path.NormalizePath().WithEnding("\\"));

            var normalizedBaseDirPath =
                Path.GetFullPath(baseDirPath.NormalizePath().WithEnding("\\"));

            return normalizedPath.StartsWith(normalizedBaseDirPath, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Normalize path string.
        /// All <c>/</c> are changed to <c>\</c>.
        /// </summary>
        /// <param name="path">Path string to normalize.</param>
        /// <returns>Normalized path string.</returns>
        public static string NormalizePath(this string path)
        {
            path.NotNullOrWhiteSpace(nameof(path));

            if (!path.IsValidPath())
            {
                throw new ArgumentException($"Invalid path '{path}'", nameof(path));
            }

            return path.Replace('/', '\\');
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the minimal concatenation of <paramref name="ending"/> (starting from end) that
        /// results in satisfying .EndsWith(ending).
        /// </summary>
        /// <example>"hel".WithEnding("llo") returns "hello", which is the result of "hel" + "lo".</example>
        /// <param name="value">String to which <paramref name="ending"/> should be added.</param>
        /// <param name="ending">String which should be added to <paramref name="value"/>.</param>
        /// <returns><paramref name="value"/> with the minimal concatenation of <paramref name="ending"/>.</returns>
        internal static string WithEnding(this string value, string ending)
        {
            if (value == null)
            {
                return ending;
            }

            string result = value;

            // Right() is 1-indexed, so include these cases
            // * Append no characters
            // * Append up to N characters, where N is ending length
            for (int i = 0; i <= ending.Length; i++)
            {
                string tmp = result + ending.Right(i);
                if (tmp.EndsWith(ending, StringComparison.OrdinalIgnoreCase))
                {
                    return tmp;
                }
            }

            return result;
        }

        /// <summary>Gets the rightmost <paramref name="length" /> characters from a string.</summary>
        /// <param name="value">The string to retrieve the substring from.</param>
        /// <param name="length">The number of characters to retrieve.</param>
        /// <returns>The substring.</returns>
        internal static string Right(this string value, int length)
        {
            value.NotNull(nameof(value));

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, "Length is less than zero");
            }

            return (length < value.Length) ? value.Substring(value.Length - length) : value;
        }
    }
}
