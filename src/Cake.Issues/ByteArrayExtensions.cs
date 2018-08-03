namespace Cake.Issues
{
    using System;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Extensions for working with byte arrays.
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Converts a byte array of an UTF-8 encoded string to a string.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="skipByteOrderMark">True if <paramref name="value"/> contains a BOM which should not be returned.</param>
        /// <returns>Converted string.</returns>
        public static string Utf8ToString(this byte[] value, bool skipByteOrderMark)
        {
            value.NotNullOrEmpty(nameof(value));

            var encoding = new UTF8Encoding(skipByteOrderMark);

            if (skipByteOrderMark)
            {
                var preamble = encoding.GetPreamble();

                if (preamble.Where((p, i) => p != value[i]).Any())
                {
                    throw new ArgumentException("Value contains no UTF-8 byte order mark.", nameof(value));
                }

                value = value.Skip(preamble.Length).ToArray();
            }

            return encoding.GetString(value);
        }
    }
}
