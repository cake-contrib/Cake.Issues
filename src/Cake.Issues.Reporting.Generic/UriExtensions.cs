namespace Cake.Issues.Reporting.Generic
{
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Extensions for the <see cref="Uri"/> class.
    /// </summary>
    internal static class UriExtensions
    {
        /// <summary>
        /// Appends paths to an URI.
        /// </summary>
        /// <param name="uri">URI to which the paths should be appended.</param>
        /// <param name="paths">Paths to append.</param>
        /// <returns>URI with appended paths.</returns>
        public static Uri Append(this Uri uri, params string[] paths)
        {
            uri.NotNull(nameof(uri));

            return
                new Uri(
                    paths
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Aggregate(
                            uri.AbsoluteUri,
                            (current, path) =>
                                string.Format(
                                    CultureInfo.InvariantCulture,
                                    "{0}/{1}",
                                    current.TrimEnd('/'),
                                    path.TrimStart('/'))));
        }
    }
}
