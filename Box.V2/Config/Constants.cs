using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Config
{
    public static class Constants
    {
        /*** Base API URIs ***/
        public const string BoxApiHostUriString = "https://app.box.com/api/";
        public const string BoxApiUriString = "https://api.box.com/2.0/";
        public const string BoxUploadApiUriString = "https://upload.box.com/api/2.0/";
        

        /*** API Endpoints ***/
        public const string TransactionalEndpointString = @"/api/oauth2/token";

        public const string AuthCodeString = @"oauth2/authorize";
        public const string AuthTokenEndpointString = @"oauth2/token";
        public const string RevokeEndpointString = @"oauth2/revoke";

        public const string FoldersString = @"folders/";
        public const string GroupsString = @"groups/";
        public const string GroupMembershipString = @"group_memberships/";
        public const string FilesString = @"files/";
        public const string FilesUploadString = @"files/content";
        public const string FilesNewVersionString = @"files/{0}/content";
        public const string CommentsString = @"comments/";
        public const string SearchString = @"search";
        public const string UserString = @"users/";
        public const string InviteString = @"invites/";
        public const string CollaborationsString = @"collaborations/";
        public const string RetentionPoliciesString = @"retention_policies/";
        public const string RetentionPolicyAssignmentsString = @"retention_policy_assignments/";
        public const string FileVersionRetentionsString = @"file_version_retentions/";
        public const string EventsString = @"events";
        public const string MetadataTemplatesString = @"metadata_templates/";
        public const string CreateMetadataTemplateString = @"metadata_templates/schema";
        public const string WebhooksString = @"webhooks/";
        public const string EnterprisesString = @"enterprises/";
        public const string DevicePinString = @"device_pinners/";

        /// <summary>
        /// The shared items constant
        /// </summary>
        public const string SharedItemsString = @"shared_items";
        public const string TaskAssignmentsString = @"task_assignments/";
        public const string TasksString = @"tasks/";
        /// <summary>
        /// The collections endpoint constant
        /// </summary>
        public const string CollectionsString = @"collections/";
        /// <summary>
        /// The web links endpoint constant
        /// </summary>
        public const string WebLinksString = @"web_links/";
        /// <summary>
        /// The legal hold policies endpoint constant
        /// </summary>
        public const string LegalHoldPoliciesString = @"legal_hold_policies/";
        /// <summary>
        /// The legal hold policy assignments endpoint constant
        /// </summary>
        public const string LegalHoldPolicyAssignmentsString = @"legal_hold_policy_assignments/";
        /// <summary>
        /// The legal hold policy assignments endpoint constant
        /// </summary>
        public const string FileVersionLegalHoldsString = @"file_version_legal_holds/";

        /*** API Full Endpoint Strings ***/
        public const string AuthCodeEndpointString = BoxApiHostUriString + AuthCodeString;
        public const string FoldersEndpointString = BoxApiUriString + FoldersString;
        public const string GroupsEndpointString = BoxApiUriString + GroupsString;
        public const string GroupMembershipEndpointString = BoxApiUriString + GroupMembershipString;
        public const string FilesEndpointString = BoxApiUriString + FilesString;
        public const string FilesUploadEndpointString = BoxUploadApiUriString + FilesUploadString;
        public const string FilesNewVersionEndpointString = BoxUploadApiUriString + FilesNewVersionString;
        public const string FilesPreflightCheckNewVersionString = BoxApiUriString + FilesNewVersionString;
        public const string CommentsEndpointString = BoxApiUriString + CommentsString;
        public const string SearchEndpointString = BoxApiUriString + SearchString;
        public const string UserEndpointString = BoxApiUriString + UserString;
        public const string CollaborationsEndpointString = BoxApiUriString + CollaborationsString;
        public const string EventsEndpointString = BoxApiUriString + EventsString;
        public const string MetadataTemplatesEndpointString = BoxApiUriString + MetadataTemplatesString;
        public const string TaskAssignmentsEndpointString = BoxApiUriString + TaskAssignmentsString;
        public const string TasksEndpointString = BoxApiUriString + TasksString;
        public const string CollectionsEndpointString = BoxApiUriString + CollectionsString;
        public const string WebLinksEndpointString = BoxApiUriString + WebLinksString;
        public const string LegalHoldPoliciesEndpointString = BoxApiUriString + LegalHoldPoliciesString;
        public const string LegalHoldPolicyAssignmentsEndpointString = BoxApiUriString + LegalHoldPolicyAssignmentsString;

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
        public const string RetentionPolicyAssignmentsEndpointString = @"{0}/assignments";
        public const string MetadataPathString = @"{0}/metadata/{1}/{2}";
        public const string AllFileMetadataPathString = @"{0}/metadata";
        public const string AllFolderMetadataPathString = @"{0}/metadata";
        public const string MetadataTemplatesPathString = @"{0}/{1}/schema";
        public const string EnterpriseMetadataTemplatesPathString = @"{0}";
        public const string TasksPathString = @"{0}/tasks";
        public const string UserEmailAliasesPathString = @"{0}/email_aliases/";
        public const string WatermarkPathString = @"{0}/watermark";
        public const string TaskAssignmentsPathString = @"{0}/assignments";
        public const string DeleteOldVersionPathString = @"{0}/versions/{1}";
        public const string PromoteVersionPathString = @"{0}/versions/current";
        public const string MoveUserFolderPathString = @"{0}/folders/{1}";
        public const string GroupMembershipForUserPathString = @"{0}/memberships";
        public const string DeleteEmailAliasPathString = "{0}/email_aliases/{1}";
        public const string GetEnterpriseDevicePinsPathString = @"{0}/device_pinners";
        public const string LegalHoldPolicyAssignmentsPathString = @"{0}/assignments";

        /*** Auth ***/
        public const string AuthHeaderKey = "Authorization";
        public const string V1AuthString = "BoxAuth api_key={0}&auth_token={1}";
        public const string V2AuthString = "Bearer {0}";

        /*** Return types ***/
        public const string TypeFile = "file";
        public const string TypeFolder = "folder";
        public const string TypeComment = "comment";
        public const string TypeWebLink = "web_link";
        public const string TypeRetentionPolicy = "retention_policy";
        public const string TypeRetentionPolicyAssignment = "retention_policy_assignment";
        public const string TypeFileVersionRetention = "file_version_retention";
        public const string TypeCollaboration = "collaboration";
        public const string TypeFileVersion = "file_version";
        public const string TypeGroup = "group";
        public const string TypeGroupMembership = "group_membership";
        public const string TypeUser = "user";
        public const string TypeInvite = "invite";
        public const string TypeEnterprise = "enterprise";
        public const string TypeLock = "lock";
        public const string TypeWebhook = "webhook";
        public const string TypeTask = "task";
        public const string TypeEmailAlias = "email_alias";
        public const string TypeTaskAssignment = "task_assignment";
        public const string TypeCollection = "collection";
        public const string TypeDevicePin = "device_pinner";
        public const string TypeLegalHoldPolicy = "legal_hold_policy";
        public const string TypeLegalHoldPolicyAssignment = "legal_hold_policy_assignment";

        /*** File Preview ***/
        public const int DefaultRetryDelay = 1000; // milliseconds

        /*** Date Format ***/
        public const string RFC3339DateFormat = "yyyy-MM-ddTHH:mm:sszzz";
        public const string RFC3339DateFormat_UTC = "yyyy-MM-dd'T'HH:mm:ss.fffK";

        public static class RequestParameters
        {
            /*** Keys ***/
            public const string GrantType = "grant_type";
            public const string Code = "code";
            public const string ClientId = "client_id";
            public const string ClientSecret = "client_secret";
            public const string Token = "token";
            public const string BoxDeviceId = "box_device_id";
            public const string BoxDeviceName = "box_device_name";

            public const string Assertion = "assertion";

            public const string UserAgent = "User-Agent";
            public const string AcceptEncoding = "Accept-Encoding";

            public const string AsUser = "As-User";

            public const string BoxNotifications = "Box-Notifications";

            public const string ContentMD5 = "Content-MD5";

            /*** Values ***/
            public const string RefreshToken = "refresh_token";
            public const string AuthorizationCode = "authorization_code";
            public const string JWTAuthorizationCode = "urn:ietf:params:oauth:grant-type:jwt-bearer";
            public const string ContentTypeJson = "application/json";
            public const string ContentTypeJsonPatch = "application/json-patch+json";

            public const string SubjectToken = "subject_token";
            public const string SubjectTokenType = "subject_token_type";
            public const string ActorToken = "actor_token";
            public const string ActorTokenType = "actor_token_type";
            public const string AccessTokenTypeValue = "urn:ietf:params:oauth:token-type:access_token";
            public const string IdTokenTypeValue = "urn:ietf:params:oauth:token-type:id_token";
            public const string Scope = "scope";
            public const string ScopeDefaultValue = "item_preview";
            public const string Resource = "resource";
            public const string TokenExchangeGrantTypeValue = "urn:ietf:params:oauth:grant-type:token-exchange";


            public const string Status = "status";
            public const string Pending = "pending";

            public const string IfMatch = "If-Match";
        }

        public static class ErrorCodes
        {
            public const string Conflict = "item_name_in_use";
        }
    }
}
