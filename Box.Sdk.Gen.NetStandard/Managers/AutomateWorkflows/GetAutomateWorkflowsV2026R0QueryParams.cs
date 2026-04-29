using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class GetAutomateWorkflowsV2026R0QueryParams {
        /// <summary>
        /// The unique identifier that represent a folder.
        /// 
        /// The ID for any folder can be determined
        /// by visiting this folder in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/folder/123`
        /// the `folder_id` is `123`.
        /// 
        /// The root folder of a Box account is
        /// always represented by the ID `0`.
        /// </summary>
        public string FolderId { get; set; }

        /// <summary>
        /// The maximum number of items to return per page.
        /// </summary>
        public long? Limit { get; set; }

        /// <summary>
        /// Defines the position marker at which to begin returning results. This is
        /// used when paginating using marker-based pagination.
        /// </summary>
        public string Marker { get; set; }

        public GetAutomateWorkflowsV2026R0QueryParams(string folderId) {
            FolderId = folderId;
        }
    }
}