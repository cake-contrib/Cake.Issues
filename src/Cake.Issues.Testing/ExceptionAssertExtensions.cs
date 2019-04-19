namespace Cake.Issues.Testing
{
    using System;

    /// <summary>
    /// Extensions for asserting exceptions.
    /// </summary>
    public static class ExceptionAssertExtensions
    {
        /// <summary>
        /// Checks if an execption is of type <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="parameterName">Expected name of the parameter which has caused the exception.</param>
        public static void IsArgumentException(this Exception exception, string parameterName)
        {
            if (!(exception is ArgumentException argumentException))
            {
                throw new Exception($"Expected exception of type '{typeof(ArgumentException)}' but was '{exception.GetType()}'");
            }

            if (argumentException.ParamName != parameterName)
            {
                throw new Exception($"Expected parameter name to be '{parameterName}' but was '{argumentException.ParamName}'.");
            }
        }

        /// <summary>
        /// Checks if an execption is of type <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="parameterName">Expected name of the parameter which has caused the exception.</param>
        public static void IsArgumentNullException(this Exception exception, string parameterName)
        {
            if (!(exception is ArgumentNullException argumentNullException))
            {
                throw new Exception($"Expected exception of type '{typeof(ArgumentNullException)}' but was '{exception.GetType()}'");
            }

            if (argumentNullException.ParamName != parameterName)
            {
                throw new Exception($"Expected parameter name to be '{parameterName}' but was '{argumentNullException.ParamName}'.");
            }
        }

        /// <summary>
        /// Checks if an execption is of type <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="parameterName">Expected name of the parameter which has caused the exception.</param>
        public static void IsArgumentOutOfRangeException(this Exception exception, string parameterName)
        {
            if (!(exception is ArgumentOutOfRangeException argumentOutOfRangeException))
            {
                throw new Exception($"Expected exception of type '{typeof(ArgumentOutOfRangeException)}' but was '{exception.GetType()}'");
            }

            if (argumentOutOfRangeException.ParamName != parameterName)
            {
                throw new Exception($"Expected parameter name to be '{parameterName}' but was '{argumentOutOfRangeException.ParamName}'.");
            }
        }

        /// <summary>
        /// Checks if an execption is of type <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="message">Expected exception message.</param>
        public static void IsInvalidOperationException(this Exception exception, string message)
        {
            if (!(exception is InvalidOperationException invalidOperationException))
            {
                throw new Exception($"Expected exception of type '{typeof(InvalidOperationException)}' but was '{exception.GetType()}'");
            }

            if (invalidOperationException.Message != message)
            {
                throw new Exception($"Expected exception message to be '{message}' but was '{invalidOperationException.Message}'.");
            }
        }
    }
}
