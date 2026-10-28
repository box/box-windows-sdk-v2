using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class DeleteUserByIdQueryParams {
        /// <summary>
        /// Whether the user will receive email notification of
        /// the deletion.
        /// </summary>
        public bool? Notify { get; set; }

        /// <summary>
        /// Specifies whether to delete the user even if they still own files.
        /// 
        /// If the user owns content and the enterprise has a retention policy,
        /// including a retired one, or a legal hold that is not fully released,
        /// setting `force` to `true` doesn't delete the user. The request returns
        /// a `403` error, and you must transfer the user's content before deleting
        /// the user.
        /// </summary>
        public bool? Force { get; set; }

        public DeleteUserByIdQueryParams() {
            
        }
    }
}