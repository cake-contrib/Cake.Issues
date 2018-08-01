namespace Cake.Issues.Reporting.Generic.Tests
{
    using System;
    using Microsoft.CSharp.RuntimeBinder;
    using Xunit;

    /// <summary>
    /// Extensions for asserting exceptions.
    /// </summary>
    internal static class ExceptionAssertExtensions
    {
        /// <summary>
        /// Checks if an execption is of type <see cref="T:Microsoft.CSharp.RuntimeBinder.RuntimeBinderException" />.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="message">Expected exception message.</param>
        public static void IsRuntimeBinderException(this Exception exception, string message)
        {
            Assert.IsType<RuntimeBinderException>(exception);
            Assert.Equal(message, exception.Message);
        }
    }
}
