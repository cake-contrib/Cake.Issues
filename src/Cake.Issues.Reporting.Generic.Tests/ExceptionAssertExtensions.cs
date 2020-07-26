namespace Cake.Issues.Reporting.Generic.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.CSharp.RuntimeBinder;
    using Xunit;

    /// <summary>
    /// Extensions for asserting exceptions.
    /// </summary>
    internal static class ExceptionAssertExtensions
    {
        /// <summary>
        /// Checks if an exception is of type <see cref="RuntimeBinderException" />.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="message">Expected exception message.</param>
        [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global", Justification = "By design for assertions")]
        public static void IsRuntimeBinderException(this Exception exception, string message)
        {
            Assert.IsType<RuntimeBinderException>(exception);
            Assert.Equal(message, exception.Message);
        }
    }
}
