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
        /// <returns>Converted string.</returns>
        public static string ToStringUsingEncoding(this byte[] value)
        {
            value.NotNull(nameof(value));

            return value.ToStringUsingEncoding(false);
        }

        /// <summary>
        /// Converts a byte array of an UTF-8 encoded string to a string.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="skipPreamble">True if <paramref name="value"/> contains a BOM which should not be returned.</param>
        /// <returns>Converted string.</returns>
        public static string ToStringUsingEncoding(this byte[] value, bool skipPreamble)
        {
            value.NotNull(nameof(value));

            return value.ToStringUsingEncoding(Encoding.UTF8, skipPreamble);
        }

        /// <summary>
        /// Converts a byte array to a string.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="encoding">Encoding of the string.</param>
        /// <param name="skipPreamble">True if <paramref name="value"/> contains a preamble which should not be returned.</param>
        /// <returns>Converted string.</returns>
        public static string ToStringUsingEncoding(this byte[] value, Encoding encoding, bool skipPreamble)
        {
            value.NotNull(nameof(value));

            if (value.Any() && skipPreamble)
            {
                var preamble = encoding.GetPreamble();

                if (preamble.Where((p, i) => p != value[i]).Any())
                {
                    throw new ArgumentException("Value contains no preamble.", nameof(value));
                }

                value = value.Skip(preamble.Length).ToArray();
            }

            return encoding.GetString(value);
        }

        /// <summary>
        /// Converts a string to a byte array using UTF-8 encoding.
        /// </summary>
        /// <param name="value">String value to convert.</param>
        /// <returns>Byte array with string value in UTF-8 encoding.</returns>
        public static byte[] ToByteArray(this string value)
        {
            value.NotNull(nameof(value));

            return value.ToByteArray(Encoding.UTF8);
        }

        /// <summary>
        /// Converts a string to a byte array using a specific encoding.
        /// </summary>
        /// <param name="value">String value to convert.</param>
        /// <param name="encoding">Encoding to use.</param>
        /// <returns>Byte array with string value in specified encoding.</returns>
        public static byte[] ToByteArray(this string value, Encoding encoding)
        {
            value.NotNull(nameof(value));
            encoding.NotNull(nameof(encoding));

            return encoding.GetBytes(value);
        }
    }
}
