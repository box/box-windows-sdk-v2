using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Box.Sdk.Gen.Internal
{
    class ReusableContent : HttpContent
    {
        // should be a seekable stream
        private Stream? _innerContent;
        private long _contentLength;

        public ReusableContent(Stream stream)
        {
            _innerContent = stream;
            _contentLength = stream.Length;
        }

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext? context)
        {
            if (_innerContent == null)
            {
                throw new Exception("Cannot serialize empty stream");
            }
            _innerContent.Position = 0;
            await _innerContent.CopyToAsync(stream);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = _contentLength;
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            // Don't call dispose on stream content as it will close the base stream.
            _innerContent = null;
            base.Dispose(disposing);
        }
    }
}