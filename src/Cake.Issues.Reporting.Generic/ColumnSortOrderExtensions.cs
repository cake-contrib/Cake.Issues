﻿namespace Cake.Issues.Reporting.Generic
{
    using System;

    /// <summary>
    /// Extension methods for the <see cref="ColumnSortOrder"/> enumeration.
    /// </summary>
    public static class ColumnSortOrderExtensions
    {
        /// <summary>
        /// Returns the short identifier of the sort order.
        /// </summary>
        /// <param name="sortOrder">Sort order for which the identifier should be returned.</param>
        /// <returns>Short identifier of the sort order.</returns>
        public static string ToShortString(this ColumnSortOrder sortOrder)
        {
            return sortOrder switch
            {
                ColumnSortOrder.Ascending => "asc",
                ColumnSortOrder.Descending => "desc",
                _ => throw new ArgumentException("Unknown enumeration value", nameof(sortOrder)),
            };
        }
    }
}
