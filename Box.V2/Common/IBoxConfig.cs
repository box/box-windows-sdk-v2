using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Contracts
{
    public interface IBoxConfig
    {
        Uri BoxApiUri { get; }
        Uri BoxApiHostUri { get; }
        Uri BoxUploadApiUri { get; }

        string ClientId { get; }
        string ConsumerKey { get; }
        string ClientSecret { get; }
        string RedirectUri { get; }

        string DeviceId { get; set; }
        string DeviceName { get; set; }
        string UserAgent { get; set; }

        Uri FoldersEndpointUri { get; }
        Uri FilesEndpointUri { get; }
        Uri FilesUploadEndpointUri { get; }
    }
}
