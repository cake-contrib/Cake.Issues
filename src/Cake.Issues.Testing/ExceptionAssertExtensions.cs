namespace Cake.Issues.Testing
{
    using System;

    /// <summary>
    /// Extensions for asserting exceptions.
    /// </summary>
    public static class ExceptionAssertExtensions
    {
        /// <summary>
        /// Checks if an exception is of type <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="parameterName">Expected name of the parameter which has caused the exception.</param>
        [AssertionMethod]
        public static void IsArgumentException(this Exception exception, string parameterName)
        {
            var argumentException = exception.CheckExceptionType<ArgumentException>();

            if (argumentException.ParamName != parameterName)
            {
                throw new Exception($"Expected parameter name to be '{parameterName}' but was '{argumentException.ParamName}'.");
            }
        }

        /// <summary>
        /// Checks if an exception is of type <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="parameterName">Expected name of the parameter which has caused the exception.</param>
        [AssertionMethod]
        public static void IsArgumentNullException(this Exception exception, string parameterName)
        {
            var argumentNullException = exception.CheckExceptionType<ArgumentNullException>();

            if (argumentNullException.ParamName != parameterName)
            {
                throw new Exception($"Expected parameter name to be '{parameterName}' but was '{argumentNullException.ParamName}'.");
            }
        }

        /// <summary>
        /// Checks if an exception is of type <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="parameterName">Expected name of the parameter which has caused the exception.</param>
        [AssertionMethod]
        public static void IsArgumentOutOfRangeException(this Exception exception, string parameterName)
        {
            var argumentOutOfRangeException = exception.CheckExceptionType<ArgumentOutOfRangeException>();

            if (argumentOutOfRangeException.ParamName != parameterName)
            {
                throw new Exception($"Expected parameter name to be '{parameterName}' but was '{argumentOutOfRangeException.ParamName}'.");
            }
        }

        /// <summary>
        /// Checks if an exception is of type <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="message">Expected exception message.</param>
        [AssertionMethod]
        public static void IsInvalidOperationException(this Exception exception, string message)
        {
            var invalidOperationException = exception.CheckExceptionType<InvalidOperationException>();

            if (invalidOperationException.Message != message)
            {
                throw new Exception($"Expected exception message to be '{message}' but was '{invalidOperationException.Message}'.");
            }
        }

        /// <summary>
        /// Validates and converts an exception type.
        /// </summary>
        /// <typeparam name="T">Type of expected exception.</typeparam>
        /// <param name="exception">Exception which should be checked.</param>
        /// <returns>Converted exception.</returns>
        private static T CheckExceptionType<T>(this Exception exception)
            where T : Exception =>
            exception != null
                ? exception is T typedException
                ? typedException
                : throw new Exception($"Expected exception of type '{typeof(T)}' but was '{exception.GetType()}'.")
                : throw new Exception($"Expected exception of type '{typeof(T)}' but no exception was thrown.");
    }
}
