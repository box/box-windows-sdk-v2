using System;

namespace Box.V2.Config
{
    public static class Constants
    {
        /*** Base API URIs ***/
        public const string BoxApiHostUriString = "https://api.box.com/";
        public const string BoxAccountApiHostUriString = "https://account.box.com/api/";
        public const string BoxUploadApiUriWithoutVersionString = "https://upload.box.com/api/";

        public const string BoxApiV2Version = @"2.0";
        public const string BoxApiCurrentVersionUriString = BoxApiV2Version + "/";

        public const string BoxApiUriString = "https://api.box.com/2.0/";
        public const string BoxUploadApiUriString = "https://upload.box.com/api/2.0/";
        public const string BoxAuthTokenApiUriString = "https://api.box.com/oauth2/token";

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
        public const string FilesUploadSessionString = @"files/upload_sessions";
        public const string FilesNewVersionString = @"files/{0}/content";
        public const string FilesNewVersionUploadSessionString = @"files/{0}/upload_sessions";
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
        public const string MetadataQueryString = @"metadata_queries/execute_read";
        public const string WebhooksString = @"webhooks/";
        public const string RecentItemsString = @"recent_items/";
        public const string EnterprisesString = @"enterprises/";
        public const string DevicePinString = @"device_pinners/";
        public const string CollaborationWhitelistEntryString = @"collaboration_whitelist_entries/";
        public const string CollaborationWhitelistTargetEntryString = @"collaboration_whitelist_exempt_targets/";
        public const string TermsOfServicesString = @"terms_of_services/";
        public const string TermsOfServiceUserStatusesString = @"terms_of_service_user_statuses/";
        public const string MetadataCascadePoliciesString = @"metadata_cascade_policies/";
        public const string StoragePoliciesString = @"storage_policies/";
        public const string StoragePolicyAssignmentsString = @"storage_policy_assignments/";
        public const string StoragePolicyAssignmentsForTargetString = @"storage_policy_assignments";
        public const string ZipDownloadsString = @"zip_downloads";
        public const string FolderLocksString = @"folder_locks/";
        public const string SignRequestsString = @"sign_requests";
        public const string SignRequestsWithPathString = @"sign_requests/";
        public const string SignTemplatesString = @"sign_templates";
        public const string SignTemplatesWithPathString = @"sign_templates/";
        public const string FileRequestsWithPathString = @"file_requests/";


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
        public const string AuthCodeEndpointString = BoxAccountApiHostUriString + AuthCodeString;
        public const string FoldersEndpointString = BoxApiUriString + FoldersString;
        public const string GroupsEndpointString = BoxApiUriString + GroupsString;
        public const string GroupMembershipEndpointString = BoxApiUriString + GroupMembershipString;
        public const string FilesEndpointString = BoxApiUriString + FilesString;
        public const string FilesUploadEndpointString = BoxUploadApiUriString + FilesUploadString;
        public const string FilesNewVersionEndpointString = BoxUploadApiUriString + FilesNewVersionString;
        public const string FilesNewVersionUploadSessionEndpointString = BoxUploadApiUriString + FilesNewVersionUploadSessionString;
        public const string FilesPreflightCheckNewVersionString = BoxApiUriString + FilesNewVersionString;
        public const string CommentsEndpointString = BoxApiUriString + CommentsString;
        public const string SearchEndpointString = BoxApiUriString + SearchString;
        public const string UserEndpointString = BoxApiUriString + UserString;
        public const string CollaborationsEndpointString = BoxApiUriString + CollaborationsString;
        public const string EventsEndpointString = BoxApiUriString + EventsString;
        public const string MetadataTemplatesEndpointString = BoxApiUriString + MetadataTemplatesString;
        public const string MetadataQueryEndpointString = BoxApiUriString + MetadataQueryString;
        public const string TaskAssignmentsEndpointString = BoxApiUriString + TaskAssignmentsString;
        public const string TasksEndpointString = BoxApiUriString + TasksString;
        public const string CollectionsEndpointString = BoxApiUriString + CollectionsString;
        public const string WebLinksEndpointString = BoxApiUriString + WebLinksString;
        public const string LegalHoldPoliciesEndpointString = BoxApiUriString + LegalHoldPoliciesString;
        public const string LegalHoldPolicyAssignmentsEndpointString = BoxApiUriString + LegalHoldPolicyAssignmentsString;
        public const string MetadataCascadePolicyEndpointString = BoxApiUriString + MetadataCascadePoliciesString;
        public const string StoragePoliciesEndpointString = BoxApiUriString + StoragePoliciesString;
        public const string StoragePolicyAssignmentsEndpointString = BoxApiUriString + StoragePolicyAssignmentsString;
        public const string StoragePolicyAssignmentsForTargetEndpointString = BoxApiUriString + StoragePolicyAssignmentsForTargetString;
        public const string FolderLocksEndpointString = BoxApiUriString + FolderLocksString;
        public const string SignRequestsEndpointString = BoxApiUriString + SignRequestsString;
        public const string SignRequestsWithPathEndpointString = BoxApiUriString + SignRequestsWithPathString;
        public const string SignTemplatesEndpointString = BoxApiUriString + SignTemplatesString;
        public const string SignTemplatesWithPathEndpointString = BoxApiUriString + SignTemplatesWithPathString;
        public const string FileRequestsWithPathEndpointString = BoxApiUriString + FileRequestsWithPathString;

        /*** Endpoint Paths ***/
        public const string ItemsPathString = @"{0}/items";
        public const string VersionsPathString = @"{0}/versions";
        public const string CopyPathString = @"{0}/copy";
        public const string CommentsPathString = @"{0}/comments";
        public const string ThumbnailPathString = @"{0}/thumbnail.png";
        public const string ThumbnailPathExtensionString = @"{0}/thumbnail.{1}";
        public const string PreviewPathString = @"{0}/preview.png";
        public const string TrashPathString = @"{0}/trash";
        public const string DiscussionsPathString = @"{0}/discussions";
        public const string CollaborationsPathString = @"{0}/collaborations";
        public const string TrashItemsPathString = @"trash/items";
        public const string TrashFolderPathString = @"{0}/trash";
        public const string GroupMembershipPathString = @"{0}/memberships";
        public const string ContentPathString = @"{0}/content";
        public const string RetentionPolicyAssignmentsEndpointString = @"{0}/assignments";
        public const string FilesUnderRetentionEndpointString = @"{0}/files_under_retention";
        public const string FileVersionsUnderRetentionEndpointString = @"{0}/file_versions_under_retention";
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
        public const string MetadataCascadePoliciesForceApplyPathString = @"{0}/apply";
        public const string SignRequestsCancelPathString = @"{0}/cancel";
        public const string SignRequestsResendPathString = @"{0}/resend";
        public const string FileRequestsCopyPathString = @"{0}/copy";

        /*** Auth ***/
        public const string AuthHeaderKey = "Authorization";
        public const string V2AuthString = "Bearer {0}";
        public const string BearerTokenType = "bearer";
        public const int AccessTokenExpirationTime = 3600; // seconds

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
        public const string TypeUploadSession = "upload_session";
        public const string TypeRecentItem = "recent_item";
        public const string TypeCollabWhitelistEntry = "collaboration_whitelist_entry";
        public const string TypeCollabWhitelistTargetEntry = "collaboration_whitelist_exempt_target";
        public const string TypeMetadataTemplate = "metadata_template";
        public const string TypeTermsOfService = "terms_of_service";
        public const string TypeTermsOfServiceUserStatuses = "terms_of_service_user_status";
        public const string TypeMetadataCascadePolicy = "metadata_cascade_policy";
        public const string TypeStoragePolicy = "storage_policy";
        public const string TypeStoragePolicyAssignment = "storage_policy_assignment";
        public const string TypeApplication = "application";
        public const string TypeFolderLock = "folder_lock";
        public const string TypeSignRequest = "sign-request";
        public const string TypeSignTemplate = "sign-template";
        public const string TypeFileRequest = "file_request";

        /*** File Preview ***/
        public const int DefaultRetryDelay = 1000; // milliseconds

        /*** Date Format ***/
        public const string RFC3339DateFormat = "yyyy-MM-ddTHH:mm:sszzz";
        public const string RFC3339DateFormat_UTC = "yyyy-MM-dd'T'HH:mm:ssK";

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
            public const string BoxPartId = "X-Box-Part-Id";

            public const string ContentMD5 = "Content-MD5";
            public const string ContentRange = "Content-Range";
            public const string ContentLength = "Content-Length";

            public const string Digest = "Digest";

            public const string SubjectType = "box_subject_type";
            public const string SubjectId = "box_subject_id";

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

            public const string AllManagedUsers = "all_managed_users";
            public const string AdminsAndMembers = "admins_and_members";
            public const string AdminsOnly = "admins_only";

            public const string Status = "status";
            public const string Pending = "pending";

            public const string IfMatch = "If-Match";

            public const string ClientCredentials = "client_credentials";
            public const string UserSubType = "user";
            public const string EnterpriseSubType = "enterprise";

            /*** Values for specifically representations endpoint ***/
            public const string XRepHints = "x-rep-hints";
            public const string SetContentDispositionType = "set_content_disposition_type";
            public const string SetContentDispositionFilename = "set_content_disposition_filename";
            public const string RepresentationField = "representations";
        }

        public static class ErrorCodes
        {
            public const string Conflict = "item_name_in_use";
        }

        /*** Sample values for frequently requested file representations***/
        public static class RepresentationTypes
        {
            /// <summary>
            ///  This requests a pdf representation of all document Box file types
            /// </summary>
            public const string Pdf = "[pdf]";
            /// <summary>
            /// This requests a text format of all document file types including text/code files supported by Box
            /// </summary>
            public const string ExtractedText = "[extracted_text]";
            /// <summary>
            /// This will request a small jpg thumbnail of all document, image, and video Box file types
            /// </summary>
            public const string ThumbnailSmall = "[jpg?dimensions=320x320]";
            /// <summary>
            /// This will request two images of type jpg and png with dimensions of 1024x1024 for all
            /// document, image, and video Box file types
            /// </summary>
            public const string ImageMedium = "[jpg?dimensions=1024x1024][png?dimensions=1024x1024]";
            /// <summary>
            /// This will request two images of type jpg and png with dimensions of 2048x2048 for all
            /// document, image, and video Box file types
            /// </summary>
            public const string ImageLarge = "[jpg?dimensions=2048x2048][png?dimenions=2048x2048]";
        }

        /*** optional set_content_disposition_types for representations endpoint. Can only be one of value: inline or attachment ***/
        public static class ContentDispositionTypes
        {
            /// <summary>
            /// Passing this value into set_content_disposition_type will ensure that the browser opens the representation
            /// in another window
            /// </summary>
            public const string Inline = "inline";
            /// <summary>
            /// Passing this value into set_content_disposition_type will ensure that the browser downloads the representation
            /// </summary>
            public const string Attachment = "attachment";
        }

        /*** stream of events that are logged from a Box Enterprise. ***/
        public static class UserEventTypes
        {
            /// <summary>
            /// A folder or file was created. 
            /// </summary>
            public const string ItemCreate = "ITEM_CREATE";

            /// <summary>
            /// A folder of file was uploaded.
            /// </summary>
            public const string ItemUpload = "ITEM_UPLOAD";

            /// <summary>
            /// A comment was created on a folder, file, or other comment. 
            /// </summary>
            public const string CommentCreate = "COMMENT_CREATE";

            /// <summary>
            /// A comment was deleted on folder, file, or other comment. 
            /// </summary>
            public const string CommentDelete = "COMMENT_DELETE";

            /// <summary>
            /// A file or folder was downloaded. 
            /// </summary>
            public const string ItemDownload = "ITEM_DOWNLOAD";

            /// <summary>
            /// a file was previewed. 
            /// </summary>
            public const string ItemPreview = "ITEM_PREVIEW";

            /// <summary>
            /// A file or folder was moved. 
            /// </summary>
            public const string ItemMove = "ITEM_MOVE";

            /// <summary>
            /// A file or folder was copied. 
            /// </summary>
            public const string ItemCopy = "ITEM_COPY";

            /// <summary>
            /// A task was assigned. 
            /// </summary>
            public const string TaskAssignmentCreate = "TASK_ASSIGNMENT_CREATE";

            /// <summary>
            /// A task was created. 
            /// </summary>
            public const string TaskCreate = "TASK_CREATE";

            /// <summary>
            /// A file was locked. 
            /// </summary>
            public const string LockCreate = "LOCK_CREATE";

            /// <summary>
            /// A file was unlocked. If a locked file is deleted, the source file will be null. 
            /// </summary>
            public const string LockDestroy = "LOCK_DESTROY";

            /// <summary>
            /// A file or folder was marked as deleted. 
            /// </summary>
            public const string ItemTrash = "ITEM_TRASH";

            /// <summary>
            /// A collaborator was added to a folder.
            /// </summary>
            public const string CollabAddCollaborator = "COLLAB_ADD_COLLABORATOR";

            /// <summary>
            /// A collaborator has their role changed. 
            /// </summary>
            public const string CollabRoleChange = "COLLAB_ROLE_CHANGE";

            /// <summary>
            /// A collaborator was invited on a folder. 
            /// </summary>
            public const string CollabInviteCollaborator = "COLLAB_INVITE_COLLABORATOR";

            /// <summary>
            /// A collaborator was removed from a folder. 
            /// </summary>
            public const string CollabRemoveCollaborator = "COLLAB_REMOVE_COLLABORATOR";

            /// <summary>
            /// A folder was marked for sync. 
            /// </summary>
            public const string ItemSync = "ITEM_SYNC";

            /// <summary>
            /// A folder was un-marked for sync. 
            /// </summary>
            public const string ItemUnsync = "ITEM_UNSYNC";

            /// <summary>
            /// A file or folder was renamed.
            /// </summary>
            public const string ItemRename = "ITEM_RENAME";

            /// <summary>
            /// A file or folder was enabled for sharing. 
            /// </summary>
            public const string ItemSharedCreate = "ITEM_SHARED_CREATE";

            /// <summary>
            /// A file or folder was disabled for sharing. 
            /// </summary>
            public const string ItemSharedUnshare = "ITEM_SHARED_UNSHARE";

            /// <summary>
            /// A folder was shared. 
            /// </summary>
            public const string ItemShared = "ITEM_SHARED";

            /// <summary>
            /// A previous version of a file was promoted to the current version. 
            /// </summary>
            public const string ItemMakeCurrentVersion = "ITEM_MAKE_CURRENT_VERSION";

            /// <summary>
            /// A Tag was added to a file or folder. 
            /// </summary>
            public const string TagItemCreate = "TAG_ITEM_CREATE";

            /// <summary>
            /// 2 factor authentication enabled by user. 
            /// </summary>
            public const string EnableTwoFactorAuth = "ENABLE_TWO_FACTOR_AUTH";

            /// <summary>
            /// Free user accepts invitation to become a managed user. 
            /// </summary>
            public const string AdminInviteAccept = "MASTER_INVITE_ACCEPT";

            /// <summary>
            /// Free user rejects invitation to become a managed user. 
            /// </summary>
            public const string AdminInviteReject = "MASTER_INVITE_REJECT";

            /// <summary>
            /// Revoke Box access to account. 
            /// </summary>
            public const string AccessGranted = "ACCESS_GRANTED";

            /// <summary>
            /// Added user to group. 
            /// </summary>
            public const string GroupAddUser = "GROUP_ADD_USER";

            /// <summary>
            /// Removed user from group.
            /// </summary>
            public const string GroupRemoveUser = "GROUP_REMOVE_USER";
        }

        /*** The following events are defined only for the admin_logs stream_type ***/
        public static class AdminEventTypes
        {
            /// <summary>
            /// Added user to group.
            /// </summary>
            public const string GroupAddUser = "GROUP_ADD_USER";

            /// <summary>
            /// Created user. 
            /// </summary>
            public const string NewUser = "NEW_USER";

            /// <summary>
            /// Created new group.
            /// </summary>
            public const string GroupCreation = "GROUP_CREATION";

            /// <summary>
            /// Deleted group. 
            /// </summary>
            public const string GroupDeletion = "GROUP_DELETION";

            /// <summary>
            /// Deleted group. 
            /// </summary>
            public const string DeleteUser = "DELETE_USER";

            /// <summary>
            /// Edited group.
            /// </summary>
            public const string GroupEdited = "GROUP_EDITED";

            /// <summary>
            /// Edited user.
            /// </summary>
            public const string EditUser = "EDIT_USER";

            /// <summary>
            /// Removed user from group.
            /// </summary>
            public const string GroupRemoveUser = "GROUP_REMOVE_USER";

            /// <summary>
            /// Admin login.
            /// </summary>
            public const string AdminLogin = "ADMIN_LOGIN";

            /// <summary>
            /// Added device association.
            /// </summary>
            public const string AddDeviceAssociation = "ADD_DEVICE_ASSOCIATION";

            /// <summary>
            /// Edit the permissions on a folder. 
            /// </summary>
            public const string ChangeFolderPermission = "CHANGE_FOLDER_PERMISSION";

            /// <summary>
            /// Failed login.
            /// </summary>
            public const string FailedLogin = "FAILED_LOGIN";

            /// <summary>
            /// Login.
            /// </summary>
            public const string Login = "LOGIN";

            /// <summary>
            /// Removed device association. 
            /// </summary>
            public const string RemoveDeviceAssociation = "REMOVE_DEVICE_ASSOCIATION";

            /// <summary>
            /// Agree to terms.
            /// </summary>
            public const string TermsOfServiceAgree = "TERMS_OF_SERVICE_AGREE";

            /// <summary>
            /// Rejected terms. 
            /// </summary>
            public const string TermsOfServiceReject = "TERMS_OF_SERVICE_REJECT";

            /// <summary>
            /// Virus found on a file. Event is only received by enterprises that have opted in to be notified. 
            /// </summary>
            public const string FileMarkedMalicious = "FILE_MARKED_MALICIOUS";

            /// <summary>
            /// Copied. 
            /// </summary>
            public const string Copy = "COPY";

            /// <summary>
            /// Deleted.
            /// </summary>
            public const string Delete = "DELETE";

            /// <summary>
            /// Downloaded. 
            /// </summary>
            public const string Download = "DOWNLOAD";

            /// <summary>
            /// Edited 
            /// </summary>
            public const string Edit = "EDIT";

            /// <summary>
            /// Edited. 
            /// </summary>
            public const string Lock = "LOCK";

            /// <summary>
            /// Moved.
            /// </summary>
            public const string Move = "MOVE";

            /// <summary>
            /// Previewed. 
            /// </summary>
            public const string Preview = "PREVIEW";

            /// <summary>
            /// A file or folder name or description is changed. 
            /// </summary>
            public const string Rename = "RENAME";

            /// <summary>
            /// Set file auto-delete.
            /// </summary>
            public const string StorageExpiration = "STORAGE_EXPIRATION";

            /// <summary>
            /// Undeleted. 
            /// </summary>
            public const string Undelete = "UNDELETE";

            /// <summary>
            /// Unlocked. 
            /// </summary>
            public const string Unlock = "UNLOCK";

            /// <summary>
            /// Uploaded. 
            /// </summary>
            public const string Upload = "UPLOAD";

            /// <summary>
            /// Enabled shared links. 
            /// </summary>
            public const string Share = "SHARE";

            /// <summary>
            /// Share links settings updated. 
            /// </summary>
            public const string ItemSharedUpdate = "ITEM_SHARED_UPDATE";

            /// <summary>
            /// Extend shared link expiration. 
            /// </summary>
            public const string UpdateShareExpiration = "UPDATE_SHARE_EXPIRATION";

            /// <summary>
            /// Set shared link expiration. 
            /// </summary>
            public const string ShareExpiration = "SHARE_EXPIRATION";

            /// <summary>
            /// Unshared links. 
            /// </summary>
            public const string Unshare = "UNSHARE";

            /// <summary>
            /// Accepted invites. 
            /// </summary>
            public const string CollaborationAccept = "COLLABORATION_ACCEPT";

            /// <summary>
            /// Changed user roles. 
            /// </summary>
            public const string CollaborationRoleChange = "COLLABORATION_ROLE_CHANGE";

            /// <summary>
            /// Extend collaborator expiration. 
            /// </summary>
            public const string UpdateCollaborationExpiration = "UPDATE_COLLABORATION_EXPIRATION";

            /// <summary>
            /// Removed collaborators. 
            /// </summary>
            public const string CollaborationRemove = "COLLABORATION_REMOVE";

            /// <summary>
            /// Invited. 
            /// </summary>
            public const string CollaborationInvite = "COLLABORATION_INVITE";

            /// <summary>
            /// Set collaborator expiration. 
            /// </summary>
            public const string CollaborationExpiration = "COLLABORATION_EXPIRATION";

            /// <summary>
            /// Synced folder. 
            /// </summary>
            public const string ItemSync = "ITEM_SYNC";

            /// <summary>
            /// Un-synced folder. 
            /// </summary>
            public const string ItemUnsync = "ITEM_UNSYNC";

            /// <summary>
            /// A user is logging in from a device we haven't see before. 
            /// </summary>
            public const string AddLoginActivityDevice = "ADD_LOGIN_ACTIVITY_DEVICE";

            /// <summary>
            /// We invalidated a user session associated with an app. 
            /// </summary>
            public const string RemoveLoginActivityDevice = "REMOVE_LOGIN_ACTIVITY_DEVICE";

            /// <summary>
            /// When an admin role changes for a user. 
            /// </summary>
            public const string ChangeAdminRole = "CHANGE_ADMIN_ROLE";

            /// <summary>
            /// A collaborator violated an admin-set upload policy. 
            /// </summary>
            public const string ContentWorkflowUploadPolicyViolation = "CONTENT_WORKFLOW_UPLOAD_POLICY_VIOLATION";

            /// <summary>
            /// Creation of metadata instance. 
            /// </summary>
            public const string MetadataInstanceCreate = "METADATA_INSTANCE_CREATE";

            /// <summary>
            /// Update of metadata instance. 
            /// </summary>
            public const string MetadataInstanceUpdate = "METADATA_INSTANCE_UPDATE";

            /// <summary>
            /// Deletion of metadata instance. 
            /// </summary>
            public const string MetadataInstanceDelete = "METADATA_INSTANCE_DELETE";

            /// <summary>
            /// Update of a task assignment. 
            /// </summary>
            public const string TaskAssignmentUpdate = "TASK_ASSIGNMENT_UPDATE";

            /// <summary>
            /// A task assignment is created.  
            /// </summary>
            public const string TaskAssignmentCreate = "TASK_ASSIGNMENT_CREATE";

            /// <summary>
            /// A task assignment is deleted. 
            /// </summary>
            public const string TaskAssignmentDelete = "TASK_ASSIGNMENT_DELETE";

            /// <summary>
            /// A task is created. 
            /// </summary>
            public const string TaskCreate = "TASK_CREATE";

            /// <summary>
            /// A comment is created on a file. 
            /// </summary>
            public const string CommentCreate = "COMMENT_CREATE";

            /// <summary>
            /// Retention is removed. 
            /// </summary>
            public const string DateRetentionRemoveRetention = "DATA_RETENTION_REMOVE_RETENTION";

            /// <summary>
            /// Retention is created. 
            /// </summary>
            public const string DataRetentionCreateRetention = "DATA_RETENTION_CREATE_RETENTION";

            /// <summary>
            /// A retention policy assignment is added. 
            /// </summary>
            public const string RetentionPolicyAssignmentAdd = "RETENTION_POLICY_ASSIGNMENT_ADD";

            /// <summary>
            /// A legal hold assignment is created. 
            /// </summary>
            public const string LegalHoldAssignmentCreate = "LEGAL_HOLD_ASSIGNMENT_CREATE";

            /// <summary>
            /// A legal hold assignment is deleted. 
            /// </summary>
            public const string LegalHoldAssignmentDelete = "LEGAL_HOLD_ASSIGNMENT_CREATE";

            /// <summary>
            /// A legal hold policy is deleted. 
            /// </summary>
            public const string LegalHoldPolicyDelete = "LEGAL_HOLD_POLICY_DELETE";

            /// <summary>
            /// There is a sharing policy violation. 
            /// </summary>
            public const string ContentWorkflowSharingPolicyViolation = "CONTENT_WORKFLOW_SHARING_POLICY_VIOLATION";

            /// <summary>
            /// An application public key is added. 
            /// </summary>
            public const string ApplicationPublicKeyAdded = "APPLICATION_PUBLIC_KEY_ADDED";

            /// <summary>
            /// An application public key is deleted. 
            /// </summary>
            public const string ApplicationPublicKeyDeleted = "APPLICATION_PUBLIC_KEY_DELETED";

            /// <summary>
            /// A content policy is added. 
            /// </summary>
            public const string ContentWorkflowPolicyAdd = "CONTENT_WORKFLOW_POLICY_ADD";

            /// <summary>
            /// An automation is added. 
            /// </summary>
            public const string ContentWorkflowAutomationAdd = "CONTENT_WORKFLOW_AUTOMATION_ADD";

            /// <summary>
            /// An automation is deleted. 
            /// </summary>
            public const string ContentWorkflowAutomationDelete = "CONTENT_WORKFLOW_AUTOMATION_DELETE";

            /// <summary>
            /// A user email alias is confirmed. 
            /// </summary>
            public const string EmailAliasConfirm = "EMAIL_ALIAS_CONFIRM";

            /// <summary>
            /// A user email alias is removed. 
            /// </summary>
            public const string EmailAliasRemove = "EMAIL_ALIAS_REMOVE";

            /// <summary>
            /// A watermark is added to a file. 
            /// </summary>
            public const string WatermarkLabelCreate = "WATERMARK_LABEL_CREATE";

            /// <summary>
            /// A watermark is removed from a file. 
            /// </summary>
            public const string WatermarkLabelDelete = "WATERMARK_LABEL_DELETE";

            /// <summary>
            /// A user has granted Box access to their account. 
            /// </summary>
            public const string AccessGranted = "ACCESS_GRANTED";

            /// <summary>
            /// A user has revoked Box access to their account. 
            /// </summary>
            public const string AccessRevoked = "ACCESS_REVOKED";

            /// <summary>
            /// Creation of metadata template instance. 
            /// </summary>
            public const string MetadataTemplateCreate = "METADATA_TEMPLATE_CREATE";

            /// <summary>
            /// Update of metadata template instance. 
            /// </summary>
            public const string MatadataTemplateUpdate = "METADATA_TEMPLATE_UPDATE";

            /// <summary>
            /// Deletion of metadata template instance. 
            /// </summary>
            public const string MetadataTemplateDelete = "METADATA_TEMPLATE_DELETE";

            /// <summary>
            /// Item was opened. 
            /// </summary>
            public const string ItemOpen = "ITEM_OPEN";

            /// <summary>
            /// Item was modified. 
            /// </summary>
            public const string ItemModify = "ITEM_MODIFY";

            /// <summary>
            /// When a policy set the Admin console is triggered. 
            /// </summary>
            public const string ContentWorkflowAbnormalDownloadActivity = "CONTENT_WORKFLOW_ABNORMAL_DOWNLOAD_ACTIVITY";

            /// <summary>
            /// Folders were removed from a group in the admin console. 
            /// </summary>
            public const string GroupRemoveItem = "GROUP_REMOVE_ITEM";

            /// <summary>
            /// Folders were added to a group in the Admin console. 
            /// </summary>
            public const string GroupAddItem = "GROUP_ADD_ITEM";

            /// <summary>
            /// An OAuth2 access token was generated for a user.
            /// </summary>
            public const string UserAuthenticateOAuth2AccessTokenCreate = "USER_AUTHENTICATE_OAUTH2_ACCESS_TOKEN_CREATE";

            /// <summary>
            /// A content was accessed by a user.
            /// </summary>
            public const string ContentAccess = "CONTENT_ACCESS";

            /// <summary>
            /// A file version was made current.
            /// </summary>
            public const string FileVersionRestore = "FILE_VERSION_RESTORE";
        }

        /*** required direction for collaboration whitelists endpoint. Can only be one of value: inbound, outbound, or both ***/
        public static class WhitelistDirections
        {
            /// <summary>
            /// Passing this value into direction will limit the collaboration whitelisting to collaborations inside an enterprise only. 
            /// </summary>
            public const string Inbound = "inbound";
            /// <summary>
            /// Passing this value into direction will limit the collaboration whitelisting to collaborations outside of an enterprise only.
            /// </summary>
            public const string Outbound = "outbound";
            /// <summary>
            /// Passing this value into direction will limit the collaboration whitelisting to both collaborations inside and outside of an enterprise.
            /// </summary>
            public const string Both = "both";
        }

        /*** The desired conflict-resolution if a template already exists on a given file or folder.***/
        public static class ConflictResolution
        {
            /// <summary>
            /// This will preserve the existing value on the file.
            /// </summary>
            public const string None = "none";

            /// <summary>
            /// This will force-apply the cascade policy's value over any existing value.
            /// </summary>
            public const string Overwrite = "overwrite";
        }
    }
}
