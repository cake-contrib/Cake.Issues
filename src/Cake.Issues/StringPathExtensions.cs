namespace Cake.Issues
{
    using System;
    using System.IO;

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

            return path.IndexOfAny([.. Path.GetInvalidPathChars()]) == -1;
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
            // Based on http://stackoverflow.com/a/31941159/566901
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
        public static string WithEnding(this string value, string ending)
        {
            if (value == null)
            {
                return ending;
            }

            var result = value;

            // Right() is 1-indexed, so include these cases
            // * Append no characters
            // * Append up to N characters, where N is ending length
            for (var i = 0; i <= ending.Length; i++)
            {
                var tmp = result + ending.Right(i);
                if (tmp.EndsWith(ending, StringComparison.OrdinalIgnoreCase))
                {
                    return tmp;
                }
            }

            return result;
        }

        /// <summary>
        /// Validates if a file path is a valid path below <see cref="IRepositorySettings.RepositoryRoot"/>.
        /// </summary>
        /// <param name="filePath">Full file path.</param>
        /// <param name="repositorySettings">Repository settings.</param>
        /// <returns>Tuple containing a value if validation was successful,
        /// and file path relative to <see cref="IRepositorySettings.RepositoryRoot"/>.</returns>
        public static (bool Valid, string FilePath) IsValidRepositoryFilePath(this string filePath, IRepositorySettings repositorySettings)
        {
            filePath.NotNullOrWhiteSpace(nameof(filePath));
            repositorySettings.NotNull(nameof(repositorySettings));

            // Ignore files from outside the repository.
            if (!filePath.IsInRepository(repositorySettings))
            {
                return (false, string.Empty);
            }

            // Make path relative to repository root.
            filePath = filePath.MakeFilePathRelativeToRepositoryRoot(repositorySettings);

            return (true, filePath);
        }

        /// <summary>
        /// Checks if a file is part of the repository.
        /// </summary>
        /// <param name="filePath">Full file path.</param>
        /// <param name="repositorySettings">Repository settings.</param>
        /// <returns>True if file is in repository, false otherwise.</returns>
        public static bool IsInRepository(this string filePath, IRepositorySettings repositorySettings)
        {
            filePath.NotNullOrWhiteSpace(nameof(filePath));
            repositorySettings.NotNull(nameof(repositorySettings));

            return filePath.IsSubpathOf(repositorySettings.RepositoryRoot.FullPath);
        }

        /// <summary>
        /// Make path relative to repository root.
        /// </summary>
        /// <param name="filePath">Full file path.</param>
        /// <param name="repositorySettings">Repository settings.</param>
        /// <returns>File path relative to the repository root.</returns>
        public static string MakeFilePathRelativeToRepositoryRoot(this string filePath, IRepositorySettings repositorySettings)
        {
            filePath.NotNullOrWhiteSpace(nameof(filePath));
            repositorySettings.NotNull(nameof(repositorySettings));

            filePath = filePath[repositorySettings.RepositoryRoot.FullPath.Length..];

            // Remove leading directory separator.
            return filePath.RemoveLeadingDirectorySeparator();
        }

        /// <summary>
        /// Remove the leading directory separator from a file path.
        /// </summary>
        /// <param name="filePath">Full file path.</param>
        /// <returns>File path without leading directory separator.</returns>
        public static string RemoveLeadingDirectorySeparator(this string filePath)
        {
            filePath.NotNullOrWhiteSpace(nameof(filePath));

            if (filePath.StartsWith('\\') ||
                filePath.StartsWith('/'))
            {
                return filePath[1..];
            }

            return filePath;
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

            return (length < value.Length) ? value[^length..] : value;
        }
    }
}
