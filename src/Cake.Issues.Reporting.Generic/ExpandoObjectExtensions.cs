namespace Cake.Issues.Reporting.Generic
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using LitJson;

    /// <summary>
    /// Extension for <see cref="ExpandoObject"/>.
    /// </summary>
    public static class ExpandoObjectExtensions
    {
        /// <summary>
        /// Serializes an <see cref="ExpandoObject"/> to a JSON string.
        /// </summary>
        /// <param name="expandoObject">Object which should be serialized.</param>
        /// <returns>Serialized object.</returns>
        public static string SerializeToJsonString(this ExpandoObject expandoObject)
        {
            expandoObject.NotNull(nameof(expandoObject));

            return
                JsonMapper
                    .ToJson(new Dictionary<string, object>(expandoObject));
        }

        /// <summary>
        /// Serializes an <see cref="IEnumerable{ExpandoObject}"/> to a JSON string.
        /// </summary>
        /// <param name="expandoObjects">Objects which should be serialized.</param>
        /// <returns>Serialized objects.</returns>
        public static string SerializeToJsonString(this IEnumerable<ExpandoObject> expandoObjects)
        {
            expandoObjects.NotNull(nameof(expandoObjects));

            return
                JsonMapper
                    .ToJson(
                        expandoObjects
                            .Select(x => new Dictionary<string, object>(x))
                            .ToArray());
        }
    }
}
