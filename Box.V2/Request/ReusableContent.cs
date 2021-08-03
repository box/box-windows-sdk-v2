using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Box.V2.Request
{
    public class ReusableContent : HttpContent
    {
        private HttpContent _innerContent;

        public ReusableContent(Stream stream)
        {
            _innerContent = new StreamContent(stream);
        }

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            await _innerContent.CopyToAsync(stream);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            // Don't call dispose on stream content as it will close the base stream.
            _innerContent = null;
            base.Dispose(disposing);    
        }
    }
}
