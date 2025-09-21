using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataQuery : ISerializable {
        /// <summary>
        /// Specifies the template used in the query. Must be in the form
        /// `scope.templateKey`. Not all templates can be used in this field,
        /// most notably the built-in, Box-provided classification templates
        /// can not be used in a query.
        /// </summary>
        [JsonPropertyName("from")]
        public string From { get; }

        /// <summary>
        /// The query to perform. A query is a logical expression that is very similar
        /// to a SQL `SELECT` statement. Values in the search query can be turned into
        /// parameters specified in the `query_param` arguments list to prevent having
        /// to manually insert search values into the query string.
        /// 
        /// For example, a value of `:amount` would represent the `amount` value in
        /// `query_params` object.
        /// </summary>
        [JsonPropertyName("query")]
        public string? Query { get; init; }

        /// <summary>
        /// Set of arguments corresponding to the parameters specified in the
        /// `query`. The type of each parameter used in the `query_params` must match
        /// the type of the corresponding metadata template field.
        /// </summary>
        [JsonPropertyName("query_params")]
        public Dictionary<string, object>? QueryParams { get; init; }

        /// <summary>
        /// The ID of the folder that you are restricting the query to. A
        /// value of zero will return results from all folders you have access
        /// to. A non-zero value will only return results found in the folder
        /// corresponding to the ID or in any of its subfolders.
        /// </summary>
        [JsonPropertyName("ancestor_folder_id")]
        public string AncestorFolderId { get; }

        /// <summary>
        /// A list of template fields and directions to sort the metadata query
        /// results by.
        /// 
        /// The ordering `direction` must be the same for each item in the array.
        /// </summary>
        [JsonPropertyName("order_by")]
        public IReadOnlyList<MetadataQueryOrderByField>? OrderBy { get; init; }

        /// <summary>
        /// A value between 0 and 100 that indicates the maximum number of results
        /// to return for a single request. This only specifies a maximum
        /// boundary and will not guarantee the minimum number of results
        /// returned.
        /// </summary>
        [JsonPropertyName("limit")]
        public long? Limit { get; init; }

        /// <summary>
        /// Marker to use for requesting the next page.
        /// </summary>
        [JsonPropertyName("marker")]
        public string? Marker { get; init; }

        /// <summary>
        /// By default, this endpoint returns only the most basic info about the items for
        /// which the query matches. This attribute can be used to specify a list of
        /// additional attributes to return for any item, including its metadata.
        /// 
        /// This attribute takes a list of item fields, metadata template identifiers,
        /// or metadata template field identifiers.
        /// 
        /// For example:
        /// 
        /// * `created_by` will add the details of the user who created the item to
        /// the response.
        /// * `metadata.<scope>.<templateKey>` will return the mini-representation
        /// of the metadata instance identified by the `scope` and `templateKey`.
        /// * `metadata.<scope>.<templateKey>.<field>` will return all the mini-representation
        /// of the metadata instance identified by the `scope` and `templateKey` plus
        /// the field specified by the `field` name. Multiple fields for the same
        /// `scope` and `templateKey` can be defined.
        /// </summary>
        [JsonPropertyName("fields")]
        public IReadOnlyList<string>? Fields { get; init; }

        public MetadataQuery(string from, string ancestorFolderId) {
            From = from;
            AncestorFolderId = ancestorFolderId;
        }
        internal string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}