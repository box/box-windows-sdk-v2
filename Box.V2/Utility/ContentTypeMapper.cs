namespace Box.V2.Utility
{
    public static class ContentTypeMapper
    {
        public static string GetContentTypeFromFilename(string filename)
        {
            const string DefaultContentType = "application/octet-stream";
            var contentType = DefaultContentType;

#if NETSTANDARD2_0
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();

            if (provider.TryGetContentType(filename, out var contentTypeFromFile))
            {
                contentType = contentTypeFromFile;
            }
#else
            contentType = System.Web.MimeMapping.GetMimeMapping(filename);
#endif

            return contentType;
        }
    }
}
