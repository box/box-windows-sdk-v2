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
        /// Whether the user should be deleted even if this user
        /// still own files.
        /// </summary>
        public bool? Force { get; set; }

        public DeleteUserByIdQueryParams() {
            
        }
    }
}