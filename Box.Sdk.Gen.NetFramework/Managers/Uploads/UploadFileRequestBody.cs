using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UploadFileRequestBody {
        /// <summary>
        /// The additional attributes of the file being uploaded. Mainly the
        /// name and the parent folder. These attributes are part of the multi
        /// part request body and are in JSON format.
        /// 
        /// <Message warning>
        /// 
        ///   The `attributes` part of the body must come **before** the
        ///   `file` part. Requests that do not follow this format when
        ///   uploading the file will receive a HTTP `400` error with a
        ///   `metadata_after_file_contents` error code.
        /// 
        /// </Message>
        /// </summary>
        public UploadFileRequestBodyAttributesField Attributes { get; set; }

        /// <summary>
        /// The content of the file to upload to Box.
        /// 
        /// <Message warning>
        /// 
        ///   The `attributes` part of the body must come **before** the
        ///   `file` part. Requests that do not follow this format when
        ///   uploading the file will receive a HTTP `400` error with a
        ///   `metadata_after_file_contents` error code.
        /// 
        /// </Message>
        /// </summary>
        public System.IO.Stream File { get; set; }

        public string FileFileName { get; set; }

        public string FileContentType { get; set; }

        public UploadFileRequestBody(UploadFileRequestBodyAttributesField attributes, System.IO.Stream file) {
            Attributes = attributes;
            File = file;
        }
    }
}