using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Config
{
    public static class Constants
    {
        /*** Base API URIs ***/
        public const string BoxApiHostUriString = "https://www.box.com/api/";
        public const string BoxApiUriString = "https://api.box.com/2.0/";
        public const string BoxUploadApiUriString = "https://upload.box.com/api/2.0/";

        /*** API Endpoints ***/
        public const string AuthCodeString = @"oauth2/authorize";
        public const string AuthCodeEndpointString = BoxApiHostUriString + @"oauth2/authorize";
        public const string AuthTokenEndpointString = @"oauth2/token";
        public const string RevokeEndpointString = @"oauth2/revoke";

        public const string FoldersEndpointString = BoxApiUriString +  @"folders/";
        public const string GroupsEndpointString = BoxApiUriString + @"groups/";
        public const string GroupMembershipEndpointString = BoxApiUriString + @"group_memberships/";
        
        public const string FilesEndpointString = BoxApiUriString + @"files/";
        public const string FilesUploadEndpointString = BoxUploadApiUriString + @"files/content";
        public const string FilesNewVersionEndpointString = BoxUploadApiUriString + @"files/{0}/content";

        public const string CommentsEndpointString = BoxApiUriString + @"comments/";

        public const string SearchEndpointString = BoxApiUriString + @"search";

        public const string UserEndpointString = BoxApiUriString + @"users/";

        public const string CollaborationsEndpointString = BoxApiUriString + @"collaborations/";

        /*** Endpoint Paths ***/

        public const string ItemsPathString = @"{0}/items";
        public const string VersionsPathString = @"{0}/versions";
        public const string CopyPathString = @"{0}/copy";
        public const string CommentsPathString = @"{0}/comments";
        public const string ThumbnailPathString = @"{0}/thumbnail.png";
        public const string PreviewPathString = @"{0}/preview.png";
        public const string TrashPathString = @"{0}/trash";
         
        public const string DiscussionsPathString = @"{0}/discussions";
        public const string CollaborationsPathString = @"{0}/collaborations";
        public const string TrashItemsPathString = @"trash/items";
        public const string TrashFolderPathString = @"{0}/trash";
        public const string GroupMembershipPathString = @"{0}/memberships";

        public const string ContentPathString = @"{0}/content";

        /*** Auth ***/

        public const string AuthHeaderKey = "Authorization";
        public const string V1AuthString = "BoxAuth api_key={0}&auth_token={1}";
        public const string V2AuthString = "Bearer {0}";

        // Return types
        public const string TypeFile = "file";
        public const string TypeFolder = "folder";
        public const string TypeComment = "comment";
        public const string TypeWebLink = "web_link";
        public const string TypeCollaboration = "collaboration";
        public const string TypeFileVersion = "file_version";
        public const string TypeGroup = "group";
        public const string TypeGroupMembership = "group_membership";
        public const string TypeUser = "user";
        public const string TypeEnterprise = "enterprise";
    }
}
