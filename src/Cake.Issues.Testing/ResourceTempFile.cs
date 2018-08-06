namespace Cake.Issues.Testing
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Creates a temporary file from an embedded resource.
    /// </summary>
    public sealed class ResourceTempFile : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceTempFile"/> class.
        /// </summary>
        /// <param name="resourceName">Full name of the embedded resource in the calling assembly.</param>
        public ResourceTempFile(string resourceName)
        {
            resourceName.NotNullOrWhiteSpace(nameof(resourceName));

            using (var ms = new MemoryStream())
            using (var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new ArgumentException(
                        $"Could not find resource {resourceName}", nameof(resourceName));
                }

                stream.CopyTo(ms);
                var data = ms.ToArray();

                using (var file = new FileStream(this.FileName, FileMode.Create, FileAccess.Write))
                {
                    file.Write(data, 0, data.Length);
                }
            }
        }

        /// <summary>
        /// Gets the name of the temporary file.
        /// </summary>
        public string FileName { get; } = Path.GetTempFileName();

        /// <inheritdoc/>
        public void Dispose()
        {
            if (File.Exists(this.FileName))
            {
                File.Delete(this.FileName);
            }
        }
    }
}
