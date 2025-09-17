using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetFolderByIdQueryParams {
        /// <summary>
        /// A comma-separated list of attributes to include in the
        /// response. This can be used to request fields that are
        /// not normally returned in a standard response.
        /// 
        /// Be aware that specifying this parameter will have the
        /// effect that none of the standard fields are returned in
        /// the response unless explicitly specified, instead only
        /// fields for the mini representation are returned, additional
        /// to the fields requested.
        /// 
        /// Additionally this field can be used to query any metadata
        /// applied to the file by specifying the `metadata` field as well
        /// as the scope and key of the template to retrieve, for example
        /// `?fields=metadata.enterprise_12345.contractTemplate`.
        /// </summary>
        public IReadOnlyList<string>? Fields { get; init; }

        /// <summary>
        /// Defines the **second** attribute by which items
        /// are sorted.
        /// 
        /// The folder type affects the way the items
        /// are sorted:
        /// 
        ///   * **Standard folder**:
        ///   Items are always sorted by
        ///   their `type` first, with
        ///   folders listed before files,
        ///   and files listed
        ///   before web links.
        /// 
        ///   * **Root folder**:
        ///   This parameter is not supported
        ///   for marker-based pagination
        ///   on the root folder
        /// 
        ///   (the folder with an `id` of `0`).
        /// 
        ///   * **Shared folder with parent path
        ///   to the associated folder visible to
        ///   the collaborator**:
        ///   Items are always sorted by
        ///   their `type` first, with
        ///   folders listed before files,
        ///   and files listed
        ///   before web links.
        /// </summary>
        public StringEnum<GetFolderByIdQueryParamsSortField>? Sort { get; init; }

        /// <summary>
        /// The direction to sort results in. This can be either in alphabetical ascending
        /// (`ASC`) or descending (`DESC`) order.
        /// </summary>
        public StringEnum<GetFolderByIdQueryParamsDirectionField>? Direction { get; init; }

        /// <summary>
        /// The offset of the item at which to begin the response.
        /// 
        /// Queries with offset parameter value
        /// exceeding 10000 will be rejected
        /// with a 400 response.
        /// </summary>
        public long? Offset { get; init; }

        /// <summary>
        /// The maximum number of items to return per page.
        /// </summary>
        public long? Limit { get; init; }

        public GetFolderByIdQueryParams() {
            
        }
    }
}