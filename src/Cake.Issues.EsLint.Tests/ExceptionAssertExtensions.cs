namespace Cake.Issues.EsLint.Tests
{
    public static class ExceptionAssertExtensions
    {
        public static void IsArgumentNullException(this Exception exception, string parameterName)
        {
            var ex = Assert.IsType<ArgumentNullException>(exception);
            Assert.Equal(parameterName, ex.ParamName);
        }

        public static void IsArgumentOutOfRangeException(this Exception exception, string parameterName)
        {
            var ex = Assert.IsType<ArgumentOutOfRangeException>(exception);
            Assert.Equal(parameterName, ex.ParamName);
        }
    }
}
