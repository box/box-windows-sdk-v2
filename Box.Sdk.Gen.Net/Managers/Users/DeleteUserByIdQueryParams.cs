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
        public bool? Notify { get; init; }

        /// <summary>
        /// Specifies whether to delete the user even if they still own files,
        /// were recently active, or recently joined the enterprise from a free account.
        /// </summary>
        public bool? Force { get; init; }

        public DeleteUserByIdQueryParams() {
            
        }
    }
}