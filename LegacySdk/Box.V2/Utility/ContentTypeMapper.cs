using System.Collections.Generic;
using System.IO;

namespace Box.V2.Utility
{
    /// <summary>
    /// Utility class for determining Content-Type header. Should only be used internally by the SDK.
    /// </summary>
    public static class ContentTypeMapper
    {
        private static readonly Dictionary<string, string> _mimeMapping = new Dictionary<string, string>()
        {
            { ".jpe", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".jpg", "image/jpeg" },
            { ".bmp", "image/bmp" },
            { ".png", "image/png" },
            { ".gif", "image/gif" },
            { ".webp", "image/webp" },
        };

        /// <summary>
        /// Get Content-Type header from a filename. Supports most common image formats. Should only be used internally by the SDK.
        /// </summary>
        /// <param name="filename">full filename with extension</param>
        /// <returns>Content-Type header as a string</returns>
        public static string GetContentTypeFromFilename(string filename)
        {
            const string DefaultContentType = "application/octet-stream";
            var contentType = DefaultContentType;

            if (TryGetContentType(filename, out var contentTypeFromFile))
            {
                contentType = contentTypeFromFile;
            }

            return contentType;
        }

        private static bool TryGetContentType(string path, out string contentType)
        {
            var extension = Path.GetExtension(path);
            if (extension == null)
            {
                contentType = null;
                return false;
            }
            return _mimeMapping.TryGetValue(extension, out contentType);
        }
    }
}
