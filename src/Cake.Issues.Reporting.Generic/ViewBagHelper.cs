namespace Cake.Issues.Reporting.Generic
{
    /// <summary>
    /// Helper for working with the ViewBag in templates.
    /// </summary>
    public static class ViewBagHelper
    {
        /// <summary>
        /// Returns the value or a default value if <paramref name="value"/> is null.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="value">Value which should be returned.</param>
        /// <param name="defaultValue">Value which should be returned if <paramref name="value"/> is null.</param>
        /// <returns><paramref name="value"/> or <paramref name="defaultValue"/> if <paramref name="value"/> is null.</returns>
        public static T ValueOrDefault<T>(object value, T defaultValue)
        {
            if (value != null)
            {
                return (T)value;
            }

            return defaultValue;
        }
    }
}
