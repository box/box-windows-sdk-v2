using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Config
{
    public interface IBoxConfig
    {
        Uri BoxApiHostUri { get; }
        Uri BoxApiUri { get; }
        Uri BoxUploadApiUri { get; }

        string ClientId { get; }
        string ConsumerKey { get; }
        string ClientSecret { get; }
        Uri RedirectUri { get; }

        string EnterpriseId { get; }
        string JWTPrivateKey { get; }
        string JWTPrivateKeyPassword { get; }
        string JWTPublicKeyId { get; }

        string DeviceId { get; set; }
        string DeviceName { get; set; }
        string UserAgent { get; set; }
        
        /// <summary>
        /// Sends compressed responses from Box for faster response times
        /// </summary>
        CompressionType? AcceptEncoding { get; }

        Uri AuthCodeBaseUri { get; }
        Uri AuthCodeUri { get; }
        Uri FoldersEndpointUri { get; }
        Uri FilesEndpointUri { get; }
        Uri FilesUploadEndpointUri { get; }
        Uri CommentsEndpointUri { get; }
        Uri SearchEndpointUri { get; }
        Uri UserEndpointUri { get; }
        Uri CollaborationsEndpointUri { get; }
        Uri GroupsEndpointUri { get; }
        Uri GroupMembershipEndpointUri { get; }
        Uri RetentionPoliciesEndpointUri { get; }
        Uri RetentionPolicyAssignmentsUri { get; }
        Uri FileVersionRetentionsUri { get; }
        
    }
}
