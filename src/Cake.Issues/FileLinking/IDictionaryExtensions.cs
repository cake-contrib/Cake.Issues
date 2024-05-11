namespace Cake.Issues.FileLinking
{
    using System.Collections.Generic;

    /// <summary>
    /// Extensions for <see cref="IDictionary{TKey, TValue}"/>.
    /// </summary>
    internal static class IDictionaryExtensions
    {
        /// <summary>
        /// Gets the value associated with a key or the default value.
        /// </summary>
        /// <typeparam name="TKey">Type of the key in the dictionary.</typeparam>
        /// <typeparam name="TValue">Type of the value in the dictionary.</typeparam>
        /// <param name="dictionary">Dictionary to read the key from.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="defaultValue">Value to return if key does not exist.</param>
        /// <returns>The value associated with the key if it exists or <paramref name="defaultValue"/>.</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue)
        {
            dictionary.NotNull();

            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }
    }
}
