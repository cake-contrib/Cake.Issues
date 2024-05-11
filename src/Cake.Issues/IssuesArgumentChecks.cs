namespace Cake.Issues
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using JetBrains.Annotations;

    /// <summary>
    /// Common runtime checks that throw <see cref="ArgumentException"/> upon failure.
    /// </summary>
    public static class IssuesArgumentChecks
    {
        /// <summary>
        /// Throws an exception if the specified parameter's value is null.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
        [DebuggerStepThrough]
        public static void NotNull<T>(
            [ValidatedNotNull][NoEnumeration] this T value,
            [CallerArgumentExpression(nameof(value))] string parameterName = null)
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null, empty or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is empty or consists only of white-space characters.</exception>
        [DebuggerStepThrough]
        public static void NotNullOrWhiteSpace(
            [ValidatedNotNull]this string value,
            [CallerArgumentExpression(nameof(value))] string parameterName = null)
        {
            value.NotNull(parameterName);

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is negative.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is negative.</exception>
        [DebuggerStepThrough]
        public static void NotNegative(
            this int value,
            [CallerArgumentExpression(nameof(value))] string parameterName = null)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is negative or zero.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is negative or zero.</exception>
        [DebuggerStepThrough]
        public static void NotNegativeOrZero(
            this int value,
            [CallerArgumentExpression(nameof(value))] string parameterName = null)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is empty.</exception>
        [DebuggerStepThrough]
        public static void NotNullOrEmpty<T>(
            [NoEnumeration] this IEnumerable<T> value,
            [CallerArgumentExpression(nameof(value))] string parameterName = null)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            value.NotNull(parameterName);

            // ReSharper disable once PossibleMultipleEnumeration
            if (!value.Any())
            {
                throw new ArgumentException("Empty list.", parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null, empty or contains an empty element.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> contains an empty element.</exception>
        [DebuggerStepThrough]
        public static void NotNullOrEmptyElement<T>(
            [NoEnumeration] this IEnumerable<T> value,
            [CallerArgumentExpression(nameof(value))] string parameterName = null)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            value.NotNull(parameterName);

            // ReSharper disable once PossibleMultipleEnumeration
            if (value.Any(x => x == null))
            {
                throw new ArgumentOutOfRangeException(parameterName, "List contains.");
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null, empty or contains an empty element.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> contains an empty element.</exception>
        [DebuggerStepThrough]
        public static void NotNullOrEmptyOrEmptyElement<T>(
            [NoEnumeration] this IEnumerable<T> value,
            [CallerArgumentExpression(nameof(value))] string parameterName = null)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            value.NotNullOrEmpty(parameterName);

            // ReSharper disable once PossibleMultipleEnumeration
            value.NotNullOrEmptyElement(parameterName);
        }
    }
}
