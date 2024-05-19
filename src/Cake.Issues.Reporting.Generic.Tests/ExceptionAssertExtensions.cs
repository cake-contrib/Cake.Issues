namespace Cake.Issues.Reporting.Generic.Tests
{
    using Microsoft.CSharp.RuntimeBinder;

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
        public static void IsRuntimeBinderException(this Exception exception, string message)
        {
            Assert.IsType<RuntimeBinderException>(exception);
            message.ShouldBe(exception.Message);
        }
    }
}
