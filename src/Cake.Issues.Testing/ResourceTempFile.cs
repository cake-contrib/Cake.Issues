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
            resourceName.NotNullOrWhiteSpace();

            using (var ms = new MemoryStream())
            using (var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new ArgumentException(
                        $"Could not find resource {resourceName}", nameof(resourceName));
                }

                stream.CopyTo(ms);
                this.Content = ms.ToArray();

                using (var file = new FileStream(this.FileName, FileMode.Create, FileAccess.Write))
                {
                    file.Write(this.Content, 0, this.Content.Length);
                }
            }
        }

        /// <summary>
        /// Gets the name of the temporary file.
        /// </summary>
        public string FileName { get; } = Path.GetTempFileName();

        /// <summary>
        /// Gets the content which was written to the temporary file.
        /// </summary>
        public byte[] Content { get; }

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
