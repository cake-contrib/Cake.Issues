namespace Cake.Issues.Reporting.Generic
{
    using System;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Extensions for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Sanitizes a string to be a valid HTML ID.
        /// </summary>
        /// <param name="input">String which should be sanitized.</param>
        /// <returns><paramref name="input"/> as valid HTML ID.</returns>
        public static string SanitizeHtmlIdAttribute(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            // Cutoff illegal characters in the beginning
            var firstLegalCharacter = GetIndexOfFirstLetter(input);
            input = input.Substring(firstLegalCharacter);

            return Regex.Replace(input, @"/^[^a-z]+|[^\w:.-]+", "-");
        }

        /// <summary>
        /// Returns the index of the first letter in a string.
        /// </summary>
        /// <param name="input">String to search for a letter.</param>
        /// <returns>Index of the first letter in <paramref name="input"/>.</returns>
        private static int GetIndexOfFirstLetter(string input)
        {
            var index = 0;
            foreach (var c in input)
            {
                if (char.IsLetter(c))
                {
                    return index;
                }

                index++;
            }

            return input.Length;
        }
    }
}
