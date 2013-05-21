using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2
{
    public static class Constants
    {
        /*** Base API URIs ***/
        public const string BoxApiV1UriString = "https://www.box.com/api/";
        public const string BoxUploadApiUriString = "https://upload.box.com/api/2.0";
        public const string BoxApiUriString = "https://api.box.com/2.0/";


        /*** API Endpoints ***/
        public const string AuthCodeEndpointString = @"oauth2/authorize";
        public const string AuthTokenEndpointString = @"oauth2/token";
        public const string RevokeEndpointString = @"oauth2/revoke";

        public const string FoldersEndpointString = BoxApiUriString +  @"folders/";
        public const string FilesEndpointString = BoxApiUriString + @"files/";

        /*** Endpoint Paths ***/

        public const string ItemsPathString = @"{0}/items";
        public const string CopyPathString = @"copy";
        public const string DiscussionsPathString = @"{0}/discussions";
        public const string CollaborationsPathString = @"{0}/collaborations";
        public const string TrashItemsPathString = @"trash/items";
        public const string TrashFolderPathString = @"{0}/trash";

        public const string ContentPathString = @"{0}/content";
    }
}
